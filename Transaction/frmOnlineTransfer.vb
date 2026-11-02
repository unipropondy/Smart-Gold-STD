Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmOnlineTransfer
    Dim objInv As New clsInventory
    Dim objClass As New ClsClass
    Dim sClassCode, sCategoryCode, sCatalogueCode As String
    Dim dQty As Double
    Dim dm As New DataManager
    Dim sTransferDate As String
    Dim sTransferNo As String
    Dim sFromLocation, sToLocation As String
    Dim sRemarks As String
    Dim sTransferReferenceNumber As String
    Dim msClsSalesType As String
    Private Sub txtInventoryCode_LostFocus(sender As Object, e As System.EventArgs) Handles txtInventoryCode.LostFocus
        If Trim(txtInventoryCode.Text) <> "" Then
            If fncGetInventory(txtInventoryCode.Text) Then
                If msClsSalesType <> "P" Then
                    btnAdd_Click(sender, e)
                End If
            End If
        End If
    End Sub

    Private Function fncGetInventory(ByVal sinventorycode As String) As Boolean
        Dim lSalesBillNo&
        Dim dsInv As New DataSet
        Try
            dsInv = objInv.GetInventoryDetailTransfer(sinventorycode)
            fncGetInventory = True
            If dsInv.Tables(0).Rows.Count > 0 Then
                ''lSalesBillNo = Val(dsInv.Tables(0).Rows(0)("SalesBillNo") & "")
                ' ''If Val(adoRS!SalesAmount & "") > 0 Then
                ''If UCase(sCompanyShortCode = "GMT") Then
                ''    If (Val(dsInv.Tables(0).Rows(0)("QtyOnHand") & "") = 0) And (Trim(dsInv.Tables(0).Rows(0)("CategoryCode")) <> "22KASR") Then
                ''        MsgBox("Item Already Sold for Invoice " & Trim(dsInv.Tables(0).Rows(0)("SalesBillNo") & "") & " on " & Format(dsInv.Tables(0).Rows(0)("SalesDAte"), "dd/MM/yyyy"))
                ''        fncGetInventory = False
                ''        txtInventoryCode.Focus()
                ''        Exit Function
                ''    End If
                If (Val(dsInv.Tables(0).Rows(0)("QtyOnHand") & "") < 1) Then
                    MsgBox("No Stock for this Inventory!")
                    fncGetInventory = False
                    txtInventoryCode.Focus()
                    Exit Function
                End If
                ''Else
                ''    If ((Val(dsInv.Tables(0).Rows(0)("QtyOnHand") & "") = 0) And (Val(dsInv.Tables(0).Rows(0)("QtySold") & "") = 0)) Then
                ''        MsgBox("No Stock for this Inventory!")
                ''        fncGetInventory = False
                ''        txtInventoryCode.Focus()
                ''        Exit Function
                ''    End If

                ''    If (Val(dsInv.Tables(0).Rows(0)("QtyOnHand") & "") = 0) Then
                ''        MsgBox("Item Already Sold for Invoice " & Trim(dsInv.Tables(0).Rows(0)("SalesBillNo") & "") & " on " & Format(dsInv.Tables(0).Rows(0)("SalesDAte"), "dd/MM/yyyy"))
                ''        fncGetInventory = False
                ''        txtInventoryCode.Focus()
                ''        Exit Function
                ''    End If
                ''    If (Val(dsInv.Tables(0).Rows(0)("QtyOnHand") & "") < 1) Then
                ''        MsgBox("No Stock for this Inventory!")
                ''        fncGetInventory = False
                ''        txtInventoryCode.Focus()
                ''        Exit Function
                ''    End If

                ''End If

                ''If Trim$(dsInv.Tables(0).Rows(0)("LocationCode") & "") <> gthisLocation Then
                ''    MsgBox("The selected Inventory belongs to the Location - " & Trim$(dsInv.Tables(0).Rows(0)("LocationCode") & "") & "!")
                ''    fncGetInventory = False
                ''    txtInventoryCode.Focus()
                ''    Exit Function
                ''End If
                'added by sarathiy to check the inventory is ordered for some other customer
                'dAdvancePaid = 0.0

                sClassCode = Trim(dsInv.Tables(0).Rows(0)("ClassCode") & "")
                sCategoryCode = Trim(dsInv.Tables(0).Rows(0)("CategoryCode") & "")
                sCatalogueCode = Trim(dsInv.Tables(0).Rows(0)("CatalogueCode") & "")
                subGetClass()
                'load class details to get the rate and the salestype.
                txtDescription.Text = Trim(dsInv.Tables(0).Rows(0)("Description") & "")


                txtWeight.Text = Format(Val(dsInv.Tables(0).Rows(0)("Weight") & ""), "0.00")
                txtSize.Text = Format(Val(dsInv.Tables(0).Rows(0)("sIZEcODE") & ""), "0.00")
                txtCostPerGram.Text = Format(Val(dsInv.Tables(0).Rows(0)("CostPerGram") & ""), "0.00")
                dQty = Format(Val(dsInv.Tables(0).Rows(0)("QtyonHand") & ""), "0.00")

                txtSMC.Text = Format(Val(dsInv.Tables(0).Rows(0)("sMc") & ""), "0.00")
                txtPMC.Text = Val(dsInv.Tables(0).Rows(0)("PMC") & "")
                txtPCS.Text = Format(Val(dsInv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                If msClsSalesType = "P" Then
                    If txtPCS.Enabled Then txtPCS.Focus()
                    'Exit Function
                End If
                txtTotalCost.Text = Val(dsInv.Tables(0).Rows(0)("TotalCost") & "")
                ' ''If msClsSalesType <> "P" Then
                ' ''    txtTotalCost.Text = Format((Val(txtWeight.Text)) * Val(txtCostPerGram.Text) + Val(txtSMC.Text), "0.00")
                ' ''    ''HAJ AS PCS Sales of MUKUTTHI 
                ' ''ElseIf msClsSalesType = "P" And UCase(sCompanyShortCode) = "AS" Then
                ' ''    txtTotalCost.Text = Format((Val(txtWeight.Text)) * Val(txtCostPerGram.Text) + Val(txtSMC.Text), "0.00")
                ' ''Else
                ' ''    txtTotalCost.Text = Format(Val(txtSMC.Text), "0.00")
                ' ''End If

            Else
                MsgBox("Inventory Not Found ...")
                fncGetInventory = False
                txtInventoryCode.Text = ""
                If txtInventoryCode.CanFocus Then txtInventoryCode.Focus()
                Exit Function
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Sub btnAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd.Click
        Dim nCurFrimQty As Long
        Dim nCurConsigQty As Long
        Dim i As Long
        Try
            subAddGrid()
            fncGetTotal()
            subClearTransfer()
            txtInventoryCode.Focus()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub subModifyGrid()
        Call fnSetText(dgvTransfer, dgvTransfer.CurrentRow.Index, colInventoryCode.Index, Trim(txtInventoryCode.Text))
        Call fnSetText(dgvTransfer, dgvTransfer.CurrentRow.Index, colDescription.Index, Trim(txtDescription.Text))

    End Sub
    Private Sub subClearTransfer()
        txtInventoryCode.Text = ""
        txtDescription.Text = ""
        txtWeight.Text = 0
        txtCostPerGram.Text = 0
        txtSize.Text = 0
        txtPMC.Text = 0
        txtSMC.Text = 0
        txtTotalCost.Text = 0
        txtPCS.Text = 0
        If txtInventoryCode.CanFocus Then txtInventoryCode.Focus()
    End Sub
    Private Sub subClear()

        txtTotalWeight.Text = 0
        txtTotalPMC.Text = 0
        txtTotalSMC.Text = 0
        txtTotalTotalCost.Text = 0
        txtTotalPCS.Text = 0
        If txtInventoryCode.CanFocus Then txtInventoryCode.Focus()
    End Sub
    Private Sub subGetClass()
        Dim dsCls As New DataSet
        Try
            dsCls = objClass.GetClassDetail(sClassCode)
            If dsCls.Tables(0).Rows.Count > 0 Then
                txtCostPerGram.Text = Val(dsCls.Tables(0).Rows(0)("Rate") & "")
                msClsSalesType = Trim$(dsCls.Tables(0).Rows(0)("SalesType") & "")
            Else
                MessageBox.Show("Class Not defined for this item .... ")
                Exit Sub
            End If
            dsCls = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subAddGrid()
        Dim lQty As Long
        Dim lRow As Long
        '    Dim objTransfer As New clsTransfer
        Dim iRow As Long
        Try
            lQty = 0
            lRow = 0
            If fncInventoryCodeExist(txtInventoryCode.Text.Trim, lRow, dQty) Then
                MsgBox(" Item found in tranfer list ")
                subClearTransfer()
                If txtInventoryCode.CanFocus Then txtInventoryCode.Focus()
                Exit Sub
            End If

            If lQty = 0 Then

                iRow = dgvTransfer.Rows.Add

                Call fnSetdgvText(dgvTransfer, iRow, colInventoryCode.Index, Trim(txtInventoryCode.Text & ""))
                Call fnSetdgvText(dgvTransfer, iRow, colDescription.Index, Trim(txtDescription.Text & ""))
                Call fnSetdgvText(dgvTransfer, iRow, colClassCode.Index, Trim(sClassCode))
                Call fnSetdgvText(dgvTransfer, iRow, colCategoryCode.Index, Trim(sCategoryCode))
                Call fnSetdgvText(dgvTransfer, iRow, colCatalogueCode.Index, Trim(sCatalogueCode))
                Call fnSetdgvText(dgvTransfer, iRow, colWeight.Index, Val(txtWeight.Text & ""))
                Call fnSetdgvText(dgvTransfer, iRow, colSize.Index, Val(txtSize.Text & ""))
                Call fnSetdgvText(dgvTransfer, iRow, colQty.Index, Val(txtPCS.Text & ""))
                'Call fnSetText(dgvTransfer, iRow, colQtyRequest.Index, Val(ToTransfer & ""))
                Call fnSetdgvText(dgvTransfer, iRow, colCostPerGram.Index, Val(txtCostPerGram.Text & ""))

                Call fnSetdgvText(dgvTransfer, iRow, colPMC.Index, Val(txtPMC.Text & ""))
                Call fnSetdgvText(dgvTransfer, iRow, colSMC.Index, Val(txtSMC.Text & ""))
                Call fnSetdgvText(dgvTransfer, iRow, colTotalCost.Index, Val(txtTotalCost.Text & ""))
                ''subTotal()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub subAddGridScrapInventory()
        Dim lQty As Long
        Dim lRow As Long
        '    Dim objTransfer As New clsTransfer
        Dim iRow As Long
        Dim i As Long
        Dim Ds As DataSet
        Dim drPurchase() As DataRow
        Dim dm As New DataManager
        Dim objTran As New clsTransferOut
        Try
            lQty = 0
            lRow = 0

            Ds = objTran.GetScrapInventory()


            dgvTransfer.RowCount = 0
            For iRow = 0 To Ds.Tables("tblInventory").Rows.Count - 1
                dgvTransfer.Rows.Add()



                Call fnSetdgvText(dgvTransfer, iRow, colInventoryCode.Index, Trim(Ds.Tables("tblInventory").Rows(iRow)("Inventorycode") & ""))
                Call fnSetdgvText(dgvTransfer, iRow, colDescription.Index, Trim(Ds.Tables("tblInventory").Rows(iRow)("Description") & ""))
                Call fnSetdgvText(dgvTransfer, iRow, colClassCode.Index, Trim(Ds.Tables("tblInventory").Rows(iRow)("ClassCode") & ""))
                Call fnSetdgvText(dgvTransfer, iRow, colCategoryCode.Index, Trim$(Ds.Tables("tblInventory").Rows(iRow)("CategoryCode") & ""))
                Call fnSetdgvText(dgvTransfer, iRow, colCatalogueCode.Index, Trim$(Ds.Tables("tblInventory").Rows(iRow)("CatalogueCode") & ""))
                Call fnSetdgvText(dgvTransfer, iRow, colWeight.Index, Format(Val(Ds.Tables("tblInventory").Rows(iRow)("Weight") & ""), "#0.00"))
                Call fnSetdgvText(dgvTransfer, iRow, colSize.Index, Trim(Ds.Tables("tblInventory").Rows(iRow)("SizeCode") & ""))
                Call fnSetdgvText(dgvTransfer, iRow, colQty.Index, Format(Val(Ds.Tables("tblInventory").Rows(iRow)("Pcs") & ""), "#0"))
                'Call fnSetText(dgvTransfer, iRow, colQtyRequest.Index, Val(ToTransfer & ""))
                Call fnSetdgvText(dgvTransfer, iRow, colCostPerGram.Index, Format(Val(Ds.Tables("tblInventory").Rows(iRow)("CostperGram") & ""), "#0.00"))

                Call fnSetdgvText(dgvTransfer, iRow, colPMC.Index, Format(Val(Ds.Tables("tblInventory").Rows(iRow)("PMC")), "#0.00"))
                Call fnSetdgvText(dgvTransfer, iRow, colSMC.Index, Format(Val(Ds.Tables("tblInventory").Rows(iRow)("sMc")), "#0.00"))
                Call fnSetdgvText(dgvTransfer, iRow, colTotalCost.Index, Format(Val(Ds.Tables("tblInventory").Rows(iRow)("TotalCost") & ""), "#0.00"))
                ''subTotal()
            Next iRow

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Function fncInventoryCodeExist(ByVal sInventoryCode As String, ByRef lRow As Long, ByRef lQty As Long) As Boolean
        Dim i As Long
        Try
            fncInventoryCodeExist = False
            For i = 0 To dgvTransfer.Rows.Count - 1
                If Trim(fnGetdgvText(dgvTransfer, i, colInventoryCode.Index)).ToUpper = Trim(sInventoryCode).ToUpper Then
                    lQty = Val(fnGetdgvText(dgvTransfer, i, colQty.Index))
                    lRow = i
                    fncInventoryCodeExist = True
                    Exit Function
                End If
            Next i
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Function


    Public Sub AddItemstoComboL()
        dm.SetMasterDataSetLoc("Select * from tblLocation Order by SortCode", "tbllocation")
        dm.FillDatasToComboListWithFilters(cboToLocationCode, "tbllocation", "LocationCode", "LocationName")
    End Sub

    Private Sub frmOnlineTransfer_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            Select Case e.KeyCode
                'Case Keys.F6
                '    If btnBillClear.Enabled Then btnBillClear_Click(sender, e)
                'Case Keys.F7
                '    If btnBillClear.Enabled Then btnBillClear_Click(sender, e)
                Case Keys.Escape
                    If btnClose.Enabled Then btnClose_Click(sender, e)
                    'Case Keys.F9
                    '    If btnReport.Enabled Then btnReport_Click(sender, e)
                    'Case Keys.F10
                    '    subAutoManual()
                Case Keys.Up
                    SendKeys.Send("+{Tab}")
                Case Keys.Down
                    SendKeys.Send("{Tab}")
                Case Keys.Enter
                    SendKeys.Send("{Tab}")
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmOnlineTransfer_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim sDate As String
        sDate = Format(ServerDate, "dd/MM/yyyy")
        txtTransferDate.Text = Mid(Trim(sDate), 1, 2)
        txtTransferMonth.Text = Mid(Trim(sDate), 4, 2)
        txtTransferYear.Text = Mid(Trim(sDate), 7, 4)
        If sTransferReferenceNumber Is Nothing Then
            sTransferReferenceNumber = ""
        Else
            If sTransferReferenceNumber <> "" Then
                txtTransferNo.Text = sTransferReferenceNumber
                txtTransferNo_LostFocus(sender, e)
            End If
        End If
        ' AddItemstoComboL()
        txtFromLocationCode.Text = gthisLocation
        txtFromLocationCode.Enabled = False
        'If cboToLocationCode.CanFocus Then cboToLocationCode.Focus()
    End Sub

    Private Sub btnTransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransfer.Click
        Dim i As Long
        Dim objTran As New clsTransferOut
        Try
            If dgvTransfer.Rows.Count < 0 Then
                MsgBox("No Item To Transfer", vbInformation)
                Exit Sub
            End If
            If MsgBox("Do want to transfer all  these items ?", vbYesNo + MsgBoxStyle.Information) = MsgBoxResult.No Then
                Exit Sub
            End If
            If fncUpdateTransfer("I") Then
                For i = 0 To dgvTransfer.Rows.Count - 1

                Next
                ''' subDeleteTemp()
                dgvTransfer.Rows.Clear()
                ''  subTotal()
                ' ''If txtRefNo.Text <> "" And gsLocationCode <> "HQ" Then
                ' ''    fncUpdateTransferToHQ(txtRefNo.Text)
                ' ''End If
                ''subClear()
                subClearTransfer()
                subClear()
            End If
            cboToLocationCode.Focus()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        Finally
            objTran = Nothing
        End Try
    End Sub
    Private Function fncUpdateTransfer(ByVal pMode) As Boolean
        Dim sTranNo As String
        Dim dsHeader As New DataSet
        Dim dsDetail As New DataSet
        Dim dr As DataRow
        Dim drDetail As DataRow
        Dim iRow As Long
        Dim sUpdate As String
        Dim objTran As New clsOnlineTransfer
        Dim sTransferDate As String
        Dim objInventory As clsInventory

        fncUpdateTransfer = False

        sTransferDate = Trim(txtTransferDate.Text) & "/" & Trim(txtTransferMonth.Text) & "/" & Trim(txtTransferYear.Text)

        sTransferNo = objTran.fncGetOnlineTransferNo()
        If sTransferNo.Trim = "" Then
            MessageBox.Show("Error in generating Transfer No!")
            Exit Function
        End If
        txtTransferNo.Text = sTransferNo
        'sPoType = "0"

        dsHeader = objTran.fncGetOnlineTransferHeader(gsCon, "Hdr", "")
        dr = dsHeader.Tables("Hdr").NewRow()
        dr.Item("TransferNo") = sTransferNo
        dr.Item("TransferDate") = sTransferDate
        dr.Item("FromLocation") = Trim(gthisLocation)
        dr.Item("ToLocation") = Trim(cboToLocationCode.Text)
        dr.Item("TOtalWeight") = Val(txtTotalWeight.Text)
        dr.Item("TotalPcs") = Val(txtTotalPCS.Text)
        dr.Item("TotalCost") = Val(txtTotalTotalCost.Text)
        dr.Item("TotalPMC") = Val(txtTotalPMC.Text)
        dr.Item("TotalSMC") = Val(txtTotalSMC.Text)
        dr.Item("FromLocStatus") = "2"
        dr.Item("ToLocStatus") = "0"
        dr.Item("CreateUser") = Objuser.UserId
        dr.Item("ModifyUser") = Objuser.UserId
        dsHeader.Tables("Hdr").Rows.Add(dr)


        dsDetail = objTran.fncGetOnlineTransferDetail(gsCon, "Det", "")
        'If bPending Then
        '    If objTran.fncDeleteTransferPending(Trim(gsLocationCode), Trim(cboToLocation.Text)) Then
        '        MsgBox("Cannot delete previous pending records")
        '        Exit Function
        '    End If
        'End If

        For iRow = 0 To dgvTransfer.Rows.Count - 1
            If Val(fnGetdgvText(dgvTransfer, iRow, colQty.Index)) > 0 Then
                drDetail = dsDetail.Tables("Det").NewRow()
                drDetail.Item("TransferNo") = sTransferNo
                drDetail.Item("InventoryCode") = Trim(fnGetdgvText(dgvTransfer, iRow, colInventoryCode.Index))
                drDetail.Item("ClassCode") = Trim(fnGetdgvText(dgvTransfer, iRow, colDescription.Index))
                drDetail.Item("CategoryCode") = Trim(fnGetdgvText(dgvTransfer, iRow, colDescription.Index))
                drDetail.Item("CatalogueCode") = Trim(fnGetdgvText(dgvTransfer, iRow, colDescription.Index))
                drDetail.Item("Description") = Trim(fnGetdgvText(dgvTransfer, iRow, colDescription.Index))
                drDetail.Item("QtyonHand") = Val(fnGetdgvText(dgvTransfer, iRow, colQty.Index))
                drDetail.Item("Weight") = Val(fnGetdgvText(dgvTransfer, iRow, colWeight.Index))
                drDetail.Item("SizeCode") = Val(fnGetdgvText(dgvTransfer, iRow, colSize.Index))
                drDetail.Item("CostPerGram") = Val(fnGetdgvText(dgvTransfer, iRow, colCostPerGram.Index))
                drDetail.Item("PMC") = Val(fnGetdgvText(dgvTransfer, iRow, colPMC.Index))
                drDetail.Item("SMC") = Val(fnGetdgvText(dgvTransfer, iRow, colSMC.Index))
                drDetail.Item("TotalCost") = Val(fnGetdgvText(dgvTransfer, iRow, colTotalCost.Index))
                drDetail.Item("TransferTag") = False
                drDetail.Item("CreateUser") = Objuser.UserId
                drDetail.Item("ModifyUser") = Objuser.UserId
                dsDetail.Tables("Det").Rows.Add(drDetail)
                'fncUpdatePending(iRow)
            End If
        Next iRow

        sTranNo = objTran.UpdateOnlineTransfer(dsHeader, dsDetail, pMode)
        If sTranNo = "" Then
            Exit Function
        End If

        objTran.subOpenTransaferConnection()
        objTran.subBeginTransaferTransaction()
        If Not objTran.fncUpdateNextOnlineTransferNo() Then
            MessageBox.Show("Error in updating Transfer No")
            objTran.subRollBackTransaferTransaction()
            Exit Function
        End If

        For iRow = 0 To dgvTransfer.Rows.Count - 1
            If Not fncUpdateInventoryOnOnlineTransfer(Trim(fnGetdgvText(dgvTransfer, iRow, colInventoryCode.Index)), Trim(fnGetdgvText(dgvTransfer, iRow, colQty.Index))) Then
                MessageBox.Show("Error in updating inventory information")
                objTran.subRollBackTransaferTransaction()
                Exit Function
            End If


            If objTran.fncInventoryMovementSave(Trim(gthisLocation), Trim(fnGetdgvText(dgvTransfer, iRow, colInventoryCode.Index)), Trim(fnGetdgvText(dgvTransfer, iRow, colDescription.Index)), "TO", Trim(sTransferNo), sTransferDate, _
                    Val(fnGetdgvText(dgvTransfer, iRow, colQty.Index)), Val(fnGetdgvText(dgvTransfer, iRow, colWeight.Index)), Val(fnGetdgvText(dgvTransfer, iRow, colSize.Index)), Val(fnGetdgvText(dgvTransfer, iRow, colPMC.Index)), _
                    Val(fnGetdgvText(dgvTransfer, iRow, colSMC.Index)), Val(fnGetdgvText(dgvTransfer, iRow, colCostPerGram.Index)), Val(fnGetdgvText(dgvTransfer, iRow, colTotalCost.Index)), _
                     "", sUserID) Then
            Else
                MessageBox.Show("Error in updating inventory Movement information")
                objTran.subRollBackTransaferTransaction()
                Exit Function
            End If

        Next iRow
        objTran.subCommitTransaferTransaction()
        objTran.subCloseTransaferConnection()
        MsgBox("Please take note reference no. " + Trim(sTranNo))
        txtTransferNo.Text = sTranNo



        ' ''If dgvTransfer.Rows.Count > 0 Then
        ' ''    For i = 0 To dgvTransfer.Rows.Count - 1
        ' ''        sUpdate = ""
        ' ''        If sPoType = gObjPM.gpTypeSellthrough Then
        ' ''            If sType = gObjPM.gpTypeSellthrough Then
        ' ''                sUpdate = " Update tblInventoryStock Set ToOrder = " & Val(fnGetText(dgvTransfer, i, colQtyOrder.Index))
        ' ''                sUpdate = sUpdate & " , ToTransfer = 0"
        ' ''                sUpdate = sUpdate & " Where InventoryCode = " & fncFormatSQLText(fnGetText(dgvTransfer, i, colInventoryCode.Index))
        ' ''                sUpdate = sUpdate & " And LocationCode = " & fncFormatSQLText(cboToLocation.Text)
        ' ''            ElseIf sType = gObjPM.gpTypeConsignment Then
        ' ''                sUpdate = " Update tblInventoryStock Set ConsignmentToOrder = " & Val(fnGetText(dgvTransfer, i, colQtyOrder.Index))
        ' ''                sUpdate = sUpdate & " , ConsignmentToTransfer = 0"
        ' ''                sUpdate = sUpdate & " Where InventoryCode = " & fncFormatSQLText(fnGetText(dgvTransfer, i, colInventoryCode.Index))
        ' ''                sUpdate = sUpdate & " And LocationCode = " & fncFormatSQLText(cboToLocation.Text)
        ' ''            End If
        ' ''        ElseIf sPoType = gObjPM.gpTypeRental Then
        ' ''            sUpdate = " Update tblInventoryStock Set RentalToOrder = " & Val(fnGetText(dgvTransfer, i, colQtyOrder.Index))
        ' ''            sUpdate = sUpdate & " , RentalToTransfer = 0"
        ' ''            sUpdate = sUpdate & " Where InventoryCode = " & fncFormatSQLText(fnGetText(dgvTransfer, i, colInventoryCode.Index))
        ' ''            sUpdate = sUpdate & " And LocationCode = " & fncFormatSQLText(cboToLocation.Text)
        ' ''        ElseIf sPoType = gObjPM.gpTypeConsignment Then
        ' ''            sUpdate = " Update tblInventoryStock Set ConsignmentToOrder = " & Val(fnGetText(dgvTransfer, i, colQtyOrder.Index))
        ' ''            sUpdate = sUpdate & " , ConsignmentToTransfer = 0"
        ' ''            sUpdate = sUpdate & " Where InventoryCode = " & fncFormatSQLText(fnGetText(dgvTransfer, i, colInventoryCode.Index))
        ' ''            sUpdate = sUpdate & " And LocationCode = " & fncFormatSQLText(cboToLocation.Text)
        ' ''        End If
        ' ''        fncExecuteQuery(sUpdate)
        ' ''    Next i
        ' ''End If
        If MsgBox("Do you want to print ?", vbYesNo + MsgBoxStyle.Information) = MsgBoxResult.Yes Then
            subPrintTransferOut(Trim(sTranNo))
        End If
        fncUpdateTransfer = True
        dsHeader = Nothing
        dsDetail = Nothing
        objTran = Nothing
        Exit Function
subUpdateError:
        fncUpdateTransfer = False
        MsgBox(Err.Description)
        dsHeader = Nothing
        dsDetail = Nothing
        objTran = Nothing
    End Function
    Private Sub subPrintTransferOut(ByVal sTransferNo As String)
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
            sPath = Application.StartupPath & "\Reports\rptOnlineTransferOutDetail.rpt"
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
    Private Function fncUpdateInventoryOnOnlineTransfer(ByVal sInventoryCode As String, ByVal dQtyonHand As Decimal) As Boolean
        Dim objTransferOut As New clsOnlineTransfer
        Dim bStatus As Boolean
        Try
            bStatus = False
            bStatus = objTransferOut.fncupdateInventoryOnOnlineTransfer(sInventoryCode, dQtyonHand, sUserID)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            objTransferOut = Nothing
            fncUpdateInventoryOnOnlineTransfer = bStatus
        End Try
    End Function
    Private Function fncGetTotal()
        Dim i As Long
        Dim sTotalWeight As Double
        Dim sTotalCost As Double
        Dim sTotalPMC As Double
        Dim sTotalSMC As Double
        Dim sTotalPCS As Integer
        Try
            fncGetTotal = False
            For i = 0 To dgvTransfer.Rows.Count - 1
                sTotalWeight = Val(sTotalWeight) + Val(fnGetdgvText(dgvTransfer, i, colWeight.Index))
                sTotalCost = Val(sTotalCost) + Val(fnGetdgvText(dgvTransfer, i, colTotalCost.Index))
                sTotalPMC = Val(sTotalPMC) + Val(fnGetdgvText(dgvTransfer, i, colPMC.Index))
                sTotalSMC = Val(sTotalSMC) + Val(fnGetdgvText(dgvTransfer, i, colSMC.Index))
                sTotalPCS = Val(sTotalPCS) + Val(fnGetdgvText(dgvTransfer, i, colQty.Index))
                fncGetTotal = True
            Next i
            txtTotalWeight.Text = sTotalWeight
            txtTotalPCS.Text = sTotalPCS
            txtTotalTotalCost.Text = sTotalCost
            txtTotalPMC.Text = sTotalPMC
            txtTotalSMC.Text = sTotalSMC


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Function

    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        subClearTransfer()
    End Sub

    Private Sub txtTransferNo_LostFocus(sender As Object, e As System.EventArgs) Handles txtTransferNo.LostFocus
        Try
            If Not Len(Trim(txtTransferNo.Text)) = 0 Then
                If fncheckTansferNoExists(Trim(txtTransferNo.Text)) Then
                    If dgvTransfer.RowCount = 0 Then
                        FetchTransferDetails(Trim(txtTransferNo.Text))
                    End If
                    subLockAllFields()
                    '''MessageBox.Show("Transfered from " & txtFromLocation.Text.Trim & " cannot modify")
                    If txtTransferDate.Enabled Then txtTransferDate.Focus()
                Else
                    MessageBox.Show("Transfer number does not exists.")
                    txtTransferNo.Text = ""
                    If txtTransferNo.Enabled Then txtTransferNo.Focus()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Function fncheckTansferNoExists(ByVal sTransferNo) As Boolean
        Try
            Dim sSql As String
            Dim DR As DataRow()
            sSql = ""
            sSql = "Select  TransferNo from tblInventoryOnlineTransferHeader where TransferNo=" & fncFormatSQLText(sTransferNo)

            DR = dm.RowReturnProjExecNonQryWithDataRow(sSql)
            If Not DR.Length > 0 Then
                fncheckTansferNoExists = False
            Else
                fncheckTansferNoExists = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function
    Public Function FetchTransferDetails(ByVal sTransferNo As String)
        Dim sSql As String
        Dim iCnt As Integer
        Dim dr As DataRow()
        Try
            sSql = "select TotalCost,TotalPCS,TotalWeight,ToLOcation from tblInventoryOnlineTransferHeader where transferNo=" & fncFormatSQLText(sTransferNo)
            dr = dm.RowReturnProjExecNonQryWithDataRow(sSql)
            If dr.Length > 0 Then
                txtTotalCost.Text = Val(dr(0)("TotalCost") & "")
                txtTotalWeight.Text = Val(dr(0)("TotalWeight") & "")
                'txtTotalItem.Text = Trim(dr(0)("TotalItem") & "")
                txtTotalPCS.Text = Val(dr(0)("TotalPCS") & "")
                cboToLocationCode.Text = Trim(dr(0)("ToLOcation") & "")
            End If
            dr = Nothing
            sSql = Nothing
            Dim dr1 As DataRow()
            'sSql = "select A.InventoryCode,A.Description,A.QtyOnHand,A.weight,A.CostPerGram,A.PMC,A.SMC,A.PCS,A.TotalCost,B.ClassCode,B.CategoryCode from tblInventoryTransferDetail A,tblInventory B where A.inventoryCode=B.inventoryCode and  A.TransferNo=" & fncFormatSQLText(sTransferNo)
            sSql = "Select * from tblInventoryOnlineTransferDetail Where TransferNo = " & fncFormatSQLText(sTransferNo)
            dr1 = dm.RowReturnProjExecNonQryWithDataRow(sSql)
            If dr1.Length > 0 Then
                For iCnt = 0 To dr1.Length - 1
                    dgvTransfer.Rows.Add()

                    fnSetdgvText(dgvTransfer, iCnt, 0, Trim(dr1(iCnt)("InventoryCode") & ""))
                    fnSetdgvText(dgvTransfer, iCnt, 1, Trim(dr1(iCnt)("Description") & ""))
                    fnSetdgvText(dgvTransfer, iCnt, 2, Trim(dr1(iCnt)("ClassCode") & ""))
                    fnSetdgvText(dgvTransfer, iCnt, 3, Trim(dr1(iCnt)("CategoryCode") & ""))
                    fnSetdgvText(dgvTransfer, iCnt, 4, Trim(dr1(iCnt)("CatalogueCode") & ""))
                    fnSetdgvText(dgvTransfer, iCnt, 5, (dr1(iCnt)("Weight")))
                    fnSetdgvText(dgvTransfer, iCnt, 6, (dr1(iCnt)("SizeCode")))
                    fnSetdgvText(dgvTransfer, iCnt, 7, (dr1(iCnt)("CostPerGram")))
                    fnSetdgvText(dgvTransfer, iCnt, 8, (dr1(iCnt)("QtyonHand") & ""))
                    fnSetdgvText(dgvTransfer, iCnt, 9, (dr1(iCnt)("PMC") & ""))
                    fnSetdgvText(dgvTransfer, iCnt, 10, (dr1(iCnt)("SMC") & ""))
                    fnSetdgvText(dgvTransfer, iCnt, 11, (dr1(iCnt)("TotalCost")))

                    '  dr1 = Nothing
                Next iCnt
                fncGetTotal()
            Else
                MessageBox.Show("No data found in detail.")
                Exit Function
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function

    Public Sub subLockAllFields()
        txtTransferDate.ReadOnly = True
        txtTransferMonth.ReadOnly = True
        txtTransferYear.ReadOnly = True
        txtTotalCost.ReadOnly = True
        txtTotalPCS.ReadOnly = True
        txtTotalWeight.ReadOnly = True

        txtDescription.ReadOnly = True
        txtWeight.ReadOnly = True
        txtCostPerGram.ReadOnly = True
        txtPMC.ReadOnly = True
        txtSMC.ReadOnly = True
        btnAdd.Enabled = False
        btnTransfer.Enabled = False
        'btnPrint.Enabled = False
        btnDelete.Enabled = False
    End Sub

    Private Sub btnTransferClear_Click(sender As System.Object, e As System.EventArgs) Handles btnTransferClear.Click
        Try
            subClearDetail()
            If txtInventoryCode.Enabled = True Then txtInventoryCode.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subClearDetail()
        Dim sDate As String
        Try
            txtTransferNo.Text = ""
            sDate = Format(Now, "dd/MM/yyyy")
            txtTransferDate.Text = Mid(Trim(sDate), 1, 2)
            txtTransferMonth.Text = Mid(Trim(sDate), 4, 2)
            txtTransferYear.Text = Mid(Trim(sDate), 7, 4)
            txtInventoryCode.Text = ""
            txtDescription.Text = ""
            txtRemarks.Text = ""
            txtWeight.Text = ""
            txtCostPerGram.Text = " "
            txtPMC.Text = " "
            txtSMC.Text = " "
            txtTotalCost.Text = ""
            txtTotalPCS.Text = ""
            txtTotalWeight.Text = ""
            dgvTransfer.RowCount = 0
            subClearTransfer()
            txtInventoryCode.Focus()
            Exit Sub
        Catch ex As Exception
        End Try

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

    Private Sub btnClose_Click(sender As Object, e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub btnDelete_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete.Click
        Dim objTransfer As New clsTransferOut
        Dim sICode As String
        Dim iRow As Long
        Try
            If fncCheckGridRowSelect(dgvTransfer) Then
                If Trim(fnGetdgvText(dgvTransfer, dgvTransfer.CurrentRow.Index, colInventoryCode.Index)) <> "" Then
                    If MsgBox("Do you want to delete this item " & Trim(fnGetdgvText(dgvTransfer, dgvTransfer.CurrentRow.Index, colInventoryCode.Index)), vbYesNo + MsgBoxStyle.Information) Then
                        iRow = dgvTransfer.CurrentRow.Index
                        dgvTransfer.Rows.RemoveAt(iRow)
                        If dgvTransfer.RowCount = 0 Then
                            cboToLocationCode.Enabled = True
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub


    Private Sub btnPrint_Click(sender As System.Object, e As System.EventArgs) Handles btnPrint.Click
        If txtTransferNo.Text.Trim <> "" Then
            If MsgBox("Do you want to print ?", vbYesNo + MsgBoxStyle.Information) = MsgBoxResult.Yes Then
                subPrintTransferOut(Trim(txtTransferNo.Text))
            End If
        End If
    End Sub
End Class