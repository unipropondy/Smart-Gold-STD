Public Class frmOrderInventory
    Dim objCust As New clsCustomer
    Dim objOrder As New clsOrder

    Private Sub btnOrderPaymentNoSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnOrderPaymentNoSearch.Click
        Try
            'Dim frmHelpOrder As New frmHelpSearch(0, 0, "tblinventoryorderHeader", "L", 4, "OrderNo", "AdvancePaid < TotalCostEstimated ", "OrderNo", "OrderNo", 120, "OrderDate", "OrderDate", 180, "CustomerCode", "CustomerCode", 60, "SupplierCode", "SupplierCode", 80)
            Dim frmHelpOrder As New frmHelpSearch(0, 0, "vwInventoryOrder", "L", 4, "OrderNo", "(Status in ('D','P','R')) and AdvancePaid < TotalCostEstimated ", "OrderNo", "OrderNo", 100, "OrderDate", "OrderDate", 80, "Description", "Description", 180, "WeightEstimated", "WeightEstimated", 80)
            frmHelpOrder.ShowDialog()
            txtOrderNo.Text = frmHelpOrder.Var1.Trim
            If txtOrderNo.Text.Trim <> "" Then subGetOrderDetails()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnDescription_Click(sender As System.Object, e As System.EventArgs) Handles btnDescription.Click
        Try
            Dim frmHelpOrder As New frmHelpSearch(0, 0, "tblinventoryorderDetail", "L", 4, "OrderNo", "OrderNo=" & fncFormatSQLText(txtOrderNo.Text) & " and InventoryCode=''", "OrderInventoryCode", "OrderInventoryCode", 120, "Description", "Description", 180, "EstimatedCost", "EstimatedCost", 60, "WeightEstimated", "WeightEstimated", 80)
            frmHelpOrder.ShowDialog()
            txtOrderInventory.Text = frmHelpOrder.Var1.Trim
            txtOrderDescription.Text = frmHelpOrder.Var2.Trim
            txtEsimatedWeight.Text = frmHelpOrder.Var3.Trim
            If txtOrderNo.Text.Trim <> "" Then subGetOrderDetails()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnInventoryCodeSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnInventoryCodeSearch.Click
        Try
            Dim frmHelpClass As New frmHelpSearch(0, 0, "vwInventoryStock", "L", 4, "Description", " QtyonHand > 0 and Locationcode = " & fncFormatSQLText(gthisLocation), "InventoryCode", "InventoryCode", 80, "Description", "Description", 180, "SupplierCode", "SupplierCode", 60, "Weight", "Weight", 80)
            frmHelpClass.ShowDialog()
            txtInventoryCode.Text = frmHelpClass.Var1.Trim
            txtInventoryCode_LostFocus(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Public Sub txtInventoryCode_LostFocus(sender As Object, e As System.EventArgs) Handles txtInventoryCode.LostFocus
        Try
            If txtInventoryCode.Text.Trim <> "" Then
                subGetInventory()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub subGetInventory()
        Dim DsInv As DataSet
        Dim DsTrade As DataSet
        Dim DsAdditional As DataSet
        Dim sender As System.Object
        Dim e As System.EventArgs
        Dim objInv As New clsInventory
        Dim objTrade As New clsTradeIN
        Dim Ds As DataSet
        Dim objTray As New clsTray
        Try
            DsInv = objInv.GetInventoryDetail(txtInventoryCode.Text.Trim)
            If DsInv.Tables("tblInventory").Rows.Count > 0 Then
                If DsInv.Tables("tblInventory").Rows(0)("QtyOnHand") & "" > 0 Then
                    txtDescription.Text = DsInv.Tables("tblInventory").Rows(0)("Description") & ""
                    txtWeight.Text = Val(DsInv.Tables("tblInventory").Rows(0)("Weight")) - Val(DsInv.Tables("tblInventory").Rows(0)("StoneWeight"))
                End If
            Else
                MessageBox.Show("InventoryCode Not Found", "GoldPOS")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subGetOrderDetails()
        Dim dsOH As New DataSet
        Dim dsOP As New DataSet
        Dim iCnt As Integer
        Dim dPaymentAmount As Decimal
        Try
            dsOH = objOrder.GetOrderHeader(txtOrderNo.Text.Trim)
            If dsOH.Tables(0).Rows.Count > 0 Then
                txtCustomerCode.Text = Trim(dsOH.Tables(0).Rows(0)("CustomerCode") & "")
                If Not txtCustomerCode.Text.Trim = "" Then
                    subGetCustomer()
                    SubGetInventoryDetails()
                End If


            Else
                MessageBox.Show("Could not find the OrderNo.")

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsOH = Nothing
        End Try
    End Sub
    Public Function SubGetInventoryDetails()
        Dim sSql, sSql1 As String
        Dim ds1 As New DataSet
        Dim ds2 As New DataSet
        Dim dm As New DataManager
        Dim icnt As Long
        Dim sOrderDescription, sOrderInventoryCode, sInventoryCode, sDesc As String
        Dim dOrderWeight, dWeight As Double
        sSql = "Select * from tblInventoryOrderDetail where OrderNo=" & fncFormatSQLText(txtOrderNo.Text)
        ds1 = dm.getDataSetWithSqlAdapter(sSql, "tblInventoryOrderDetail", "tblInventoryOrderDetail")
        For icnt = 0 To ds1.Tables("tblInventoryOrderDetail").Rows.Count - 1
            sOrderInventoryCode = Trim(ds1.Tables("tblInventoryOrderDetail").Rows(icnt)("OrderInventoryCode"))
            sInventoryCode = Trim(ds1.Tables("tblInventoryOrderDetail").Rows(icnt)("InventoryCode"))
            If sInventoryCode <> "" Then


                sOrderDescription = Trim(ds1.Tables("tblInventoryOrderDetail").Rows(icnt)("Description"))
                dOrderWeight = Val(ds1.Tables("TblInventoryOrderDetail").Rows(icnt)("WeightEstimated"))
                sSql1 = "Select * from TblInventory where inventoryCode=" & fncFormatSQLText(sInventoryCode)
                ds2 = dm.getDataSetWithSqlAdapter(sSql1, "tblInventory", "tblInventory")
                sDesc = Trim(ds2.Tables("tblInventory").Rows(0)("Description"))
                dWeight = Val(ds2.Tables("tblInventory").Rows(0)("Weight"))

                grdOrder.Rows.Add()
                fnSetdgvText(grdOrder, icnt, 0, Trim(sOrderInventoryCode))
                fnSetdgvText(grdOrder, icnt, 1, Trim(sOrderDescription))

                fnSetdgvText(grdOrder, icnt, 4, dOrderWeight)
                fnSetdgvText(grdOrder, icnt, 2, Trim(sInventoryCode))

                fnSetdgvText(grdOrder, icnt, 3, Trim(sDesc))
                fnSetdgvText(grdOrder, icnt, 5, dWeight)
                'btnAdd.Enabled = False
                'btnDelete.Enabled = False
                'btnClear.Enabled = False
                'grdOrder.Enabled = False
                'btnSave.Enabled = False
            End If


        Next
    End Function
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

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub btnAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd.Click
        If grdOrder.RowCount > 0 Then
            If Not subCheckItemInGrid() Then
                Exit Sub
            End If
        End If
        subAddToGridOrder()
    End Sub
    Public Function subCheckItemInGrid() As Boolean
        Dim iRow As Long
        Dim sOrderInventoryCode, sOrderInventoryCodeGrid As String
        Dim sInventoryCode, sInventoryCodeGrid As String
        sOrderInventoryCode = Trim(txtOrderInventory.Text)
        sInventoryCode = Trim(txtInventoryCode.Text)
        subCheckItemInGrid = False
        For iRow = 0 To grdOrder.RowCount - 1
            sOrderInventoryCodeGrid = Trim(fnGetdgvText(grdOrder, iRow, colOrderInventory.Index))
            sInventoryCodeGrid = Trim(fnGetdgvText(grdOrder, iRow, colInventoryCode.Index))
            If sOrderInventoryCode = sOrderInventoryCodeGrid Then
                subCheckItemInGrid = False
                MsgBox("The Item is Already Added into the Grid,Please choose the another Item", MsgBoxStyle.Information)
                subCheckItemInGrid = False
                Exit Function
            End If
            If sInventoryCode = sInventoryCodeGrid Then
                subCheckItemInGrid = False
                MsgBox("The Item is Already Added into the Grid,Please choose the another Item", MsgBoxStyle.Information)
                subCheckItemInGrid = False
                Exit Function
            End If
            subCheckItemInGrid = True
        Next
    End Function
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

            ' If fncIsValidToAdd() = True Then


            If lQty = 0 Then

                iRow = grdOrder.Rows.Add


                Call fnSetdgvText(grdOrder, iRow, colOrderInventory.Index, Trim(txtOrderInventory.Text))

                Call fnSetdgvText(grdOrder, iRow, ColOrderDescription.Index, Trim(txtOrderDescription.Text))

                Call fnSetdgvText(grdOrder, iRow, colInventoryCode.Index, Trim(txtInventoryCode.Text))

                Call fnSetdgvText(grdOrder, iRow, colDescription.Index, Trim(txtDescription.Text))

                Call fnSetdgvText(grdOrder, iRow, cOrderWeight.Index, (txtEsimatedWeight.Text))

                Call fnSetdgvText(grdOrder, iRow, cInvWeight.Index, (txtWeight.Text))


            End If
            subClearControls()
            ' End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub subClearControls()
        Try
            txtDescription.Clear()
            txtInventoryCode.Clear()
            txtOrderInventory.Clear()
            txtOrderDescription.Clear()
            txtOrderDescription.Clear()
            txtEsimatedWeight.Clear()
            txtWeight.Clear()
            ' grdOrder.RowCount = 0
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subClearHeader()
        txtOrderNo.Clear()
        txtCustomerCode.Clear()
        txtCustomerName.Clear()
        txtAddress1.Clear()
        txtAddress2.Clear()
        txtAddress3.Clear()
        txtPayAmount.Clear()
        txtRemarks.Clear()
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        Dim sRateFixed As String
        Dim sTradeInType As String
        Dim sInvType As String
        Dim iCnt As Integer
        Dim objPM As New clsPayMode
        Dim frmPM As New frmPayingmode
        Dim orderedDate As String
        Try

           
            objOrder.OpenOrderConnection()
            objOrder.gpBeginOrderTransaction()
                
                
                'save order detail
            'For iCnt = 0 To grdOrder.RowCount - 1
            'If Not objOrder.fncUpdateOrderInventory(fnGetdgvText(grdOrder, iCnt, colOrderInventory.Index).Trim, fnGetdgvText(grdOrder, iCnt, colInventoryCode.Index).Trim, txtOrderNo.Text.Trim) Then
            '    objOrder.gpRollBackOrderConnection()
            '    Exit Sub
            'End If
            'Next
            'save order detail
            For iCnt = 0 To grdOrder.RowCount - 1
                If Not objOrder.fncUpdateOrderInventory(fnGetdgvText(grdOrder, iCnt, colOrderInventory.Index).Trim, fnGetdgvText(grdOrder, iCnt, colInventoryCode.Index).Trim, fnGetdgvText(grdOrder, iCnt, cInvWeight.Index).Trim, Trim(sUserID), txtOrderNo.Text.Trim) Then
                    objOrder.gpRollBackOrderConnection()
                    Exit Sub
                End If
            Next
            If Not objOrder.fncUpdateOrderInventoryHeader(txtOrderNo.Text.Trim) Then
                objOrder.gpRollBackOrderConnection()
                Exit Sub
            End If

            objOrder.gpCommitOrderTransaction()
            objOrder.CloseOrderConnection()
            subClearHeader()
            subClearControls()
            grdOrder.RowCount = 0
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(sender As System.Object, e As System.EventArgs) Handles btnNew.Click
        btnAdd.Enabled = True
        btnDelete.Enabled = True
        btnClear.Enabled = True
        grdOrder.Enabled = True
        btnSave.Enabled = True
        subClearHeader()
        subClearControls()
        grdOrder.RowCount = 0
    End Sub

    Private Sub txtInventoryCode_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtInventoryCode.TextChanged

    End Sub

    Private Sub txtOrderNo_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtOrderNo.TextChanged

    End Sub

    Private Sub grdOrder_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdOrder.CellClick
        If grdOrder.RowCount > 0 Then
            txtOrderDescription.Text = fnGetdgvText(grdOrder, grdOrder.CurrentRow.Index, ColOrderDescription.Index)
            txtOrderInventory.Text = fnGetdgvText(grdOrder, grdOrder.CurrentRow.Index, colOrderInventory.Index)

            txtEsimatedWeight.Text = fnGetdgvText(grdOrder, grdOrder.CurrentRow.Index, cOrderWeight.Index)
            txtInventoryCode.Text = fnGetdgvText(grdOrder, grdOrder.CurrentRow.Index, colInventoryCode.Index)

            txtDescription.Text = fnGetdgvText(grdOrder, grdOrder.CurrentRow.Index, colDescription.Index)
            txtWeight.Text = fnGetdgvText(grdOrder, grdOrder.CurrentRow.Index, cInvWeight.Index)

        End If
    End Sub

    Private Sub grdOrder_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdOrder.CellContentClick

    End Sub

    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        txtOrderDescription.Clear()
        txtOrderInventory.Clear()
        txtEsimatedWeight.Clear()
        txtInventoryCode.Clear()
        txtDescription.Clear()
        txtWeight.Clear()
        txtOrderDescription.Focus()
    End Sub

    Private Sub btnDelete_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete.Click
        Dim iRow
        iRow = grdOrder.CurrentRow.Index
        grdOrder.Rows.RemoveAt(iRow)
        txtOrderDescription.Focus()
    End Sub

    Private Sub frmOrderInventory_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        btnAdd.Enabled = True
        btnDelete.Enabled = True
        btnClear.Enabled = True
        grdOrder.Enabled = True
        btnSave.Enabled = True
    End Sub
End Class