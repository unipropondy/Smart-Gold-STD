Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmSalesOrder
    Dim dm As New DataManager
    Dim sSalesOrderNo As String
    Public Sub LoadLocationcode()
        If gthisLocationType = "HQ" Then
            If cboLocationCode.Items.Count > 0 Then cboLocationCode.SelectedIndex = -1
            cboLocationCode.Text = gthisLocation
        Else
            If cboLocationCode.Items.Count > 0 Then cboLocationCode.Text = gthisLocation
            cboLocationCode.Enabled = False
        End If
    End Sub
    Public Sub AddItemstoComboL()
        dm.SetMasterDataSetLoc("Select * from tblLocation Order by SortCode", "tbllocation")
        dm.FillDatasToComboListWithFilters(cboLocationCode, "tbllocation", "LocationCode", "LocationName")
    End Sub

    Private Sub frmSalesOrder_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F6
                btnNew_Click(sender, e)
            Case Keys.F7
                btnSave_Click(sender, e)
            Case Keys.Escape
                btnClose_Click(sender, e)
            Case Keys.Return
                SendKeys.Send("{Tab}")
                'Case Keys.Up
                'SendKeys.Send("+{Tab}")
        End Select
    End Sub

    Private Sub frmSalesOrder_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        AddItemstoComboL()
        LoadLocationcode()
    End Sub

    Private Sub btnCatagorySearch_Click(sender As System.Object, e As System.EventArgs) Handles btnCatagorySearch.Click
        Try
            Dim frmHelpCategory As New frmHelpSearch(0, 0, "tblCategory", "L", 4, "CategoryCode", "", "CategoryCode", "CategoryCode", 80, "CategoryName", "CategoryName", 180, "ClassCode", "ClassCode", 60, "Remarks", "Remarks", 80)
            frmHelpCategory.ShowDialog()
            txtCategory.Text = frmHelpCategory.Var1.Trim
        Catch ex As Exception
            MessageBox.Show(Err.Description)
        End Try
    End Sub

    Private Sub btnCatalogueSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnCatalogueSearch.Click
        Try
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCatalogue", "L", 4, "CatalogueName", "CategoryCode=" & fncFormatSQLText(txtCategory.Text), "CatalogueCode", "CatalogueCode", 80, "CatalogueName", "CatalogueName", 180, "CategoryCode", "CategoryCode", 60, "GoldLoss", "GoldLoss", 80)
            frmHelpClass.ShowDialog()
            txtCatalogue.Text = frmHelpClass.Var1.Trim
        Catch ex As Exception
            MessageBox.Show(Err.Description)
        End Try
    End Sub
    Private Sub subReOrderLevel()
        Dim sSql, sFromLocStatus As String
        Dim iRows, iCount As Integer
        Dim DM As New DataManager
        Dim drH As DataRow()

        'sSql &= "Select * from tblInventoryTransferHeader Where FromLocation = " & fncFormatSQLText(sLocation)
        ''sSql &= " And TransferDate = " & fncFormatSQLDate(sTransferDate)
        'sSql = "Select c.Cataloguecode,c.CategoryCode,c.ReOrderLevel,sum(Qtyonhand)Qty from tblInventory a,"
        'sSql = sSql & " tblInventoryStock b,tblCatalogue c where a.Cataloguecode = c.Cataloguecode And a.InventoryCode = b.InventoryCode"
        'sSql = sSql & " and isnull(c.Reorderlevel,0)>Qtyonhand Group by c.Cataloguecode,c.CategoryCode,ReOrderLevel"
        'sSql = "Select c.CategoryCode,c.Cataloguecode,c.ReOrderLevel,sum(Qtyonhand)QTy from tblInventory a,"
        'sSql = sSql & " tblInventoryStock b,tblCatalogue c where a.Cataloguecode = c.Cataloguecode And a.InventoryCode = b.InventoryCode"
        'sSql = sSql & " and b.locationcode<>'HQ'"
        'sSql = sSql & " Group by  b.Locationcode,c.Cataloguecode,c.CategoryCode,ReOrderLevel"
        'sSql = sSql & " having(isnull(c.Reorderlevel, 0) > sum(Qtyonhand))"



        'sSql = "Select c.LocationCode,a.CategoryCode,a.CatalogueCode,a.Weight,a.SizeCode,a.SizeType,"
        'sSql = sSql & " a.ReOrderLevel,sum(Qtyonhand)QTy from tblReOrderlevelMaster a, tblInventory b,"
        'sSql = sSql & " tblInventoryStock c where a.CategoryCode=b.Categorycode and "
        'sSql = sSql & " a.Cataloguecode = b.Cataloguecode And a.Weight = b.Weight And"
        'sSql = sSql & " a.SizeCode = b.SizeCode And a.SizeType = b.SizeType and b.inventorycode=C.inventorycode"
        'sSql = sSql & " and C.locationcode=" & fncFormatSQLText(cboLocationCode.Text)
        'If Trim(txtCategory.Text) <> "" Then
        '    sSql = sSql & " and b.CategoryCode=" & fncFormatSQLText(txtCategory.Text)
        'End If
        'If Trim(txtCatalogue.Text) <> "" Then
        '    sSql = sSql & " and b.CatalogueCode=" & fncFormatSQLText(txtCatalogue.Text)
        'End If
        'sSql = sSql & " Group by  c.LocationCode,a.CategoryCode,a.CatalogueCode,a.Weight,a.SizeCode,a.SizeType,a.ReOrderLevel"
        'sSql = sSql & " having(isnull(a.Reorderlevel, 0) > sum(Qtyonhand))"


        sSql = "Select c.LocationCode,b.CategoryCode,b.CatalogueCode,a.weight,a.toweight,a.SizeCode,a.SizeType,a.ToSizeCode,a.ToSizeType,"
        sSql = sSql & " a.ReOrderLevel,sum(Qtyonhand)QTy from tblReOrderlevelMaster a with(nolock), tblInventory b with(nolock),"
        sSql = sSql & " tblInventoryStock c with(nolock) where a.CategoryCode=b.Categorycode and "
        sSql = sSql & " a.Cataloguecode = b.Cataloguecode and"
        sSql = sSql & " b.inventorycode=C.inventorycode"
        sSql = sSql & " and C.locationcode=" & fncFormatSQLText(cboLocationCode.Text)
        sSql = sSql & " and A.locationcode=" & fncFormatSQLText(cboLocationCode.Text)
        If Trim(txtCategory.Text) <> "" Then
            sSql = sSql & " and b.CategoryCode=" & fncFormatSQLText(txtCategory.Text)
        End If
        If Trim(txtCatalogue.Text) <> "" Then
            sSql = sSql & " and b.CatalogueCode=" & fncFormatSQLText(txtCatalogue.Text)
        End If
        sSql = sSql & " and b.weight between a.weight and a.toweight"
        sSql = sSql & " and b.sizecode between a.SizeCode and a.ToSizeCode"
        sSql = sSql & " Group by  c.LocationCode,b.CategoryCode,b.CatalogueCode,a.SizeCode,a.SizeType,a.ToSizeCode,a.ToSizeType,a.ReOrderLevel,a.weight,a.toweight"
        sSql = sSql & " having(isnull(a.Reorderlevel, 0) > sum(Qtyonhand))"


        drH = DM.RowReturnProjExecNonQryWithDataRow(sSql)
        grdCatalogue.RowCount = 0
        If drH.Length > 0 Then
            '    grdCatalogue.Visible = True
            For iRows = 0 To drH.Length - 1
                grdCatalogue.Rows.Add()
                fnSetdgvText(grdCatalogue, iRows, 1, Trim(drH(iRows)("CategoryCode") & ""))
                fnSetdgvText(grdCatalogue, iRows, 2, Trim(drH(iRows)("Cataloguecode") & ""))
                fnSetdgvText(grdCatalogue, iRows, 3, Trim(drH(iRows)("Weight") & ""))
                fnSetdgvText(grdCatalogue, iRows, 4, Trim(drH(iRows)("TOWeight") & ""))
                fnSetdgvText(grdCatalogue, iRows, 5, Trim(drH(iRows)("SizeCode") & ""))
                fnSetdgvText(grdCatalogue, iRows, 6, Trim(drH(iRows)("SizeType") & ""))
                fnSetdgvText(grdCatalogue, iRows, 7, Trim(drH(iRows)("ToSizeCode") & ""))
                fnSetdgvText(grdCatalogue, iRows, 8, Trim(drH(iRows)("ToSizeType") & ""))
                fnSetdgvText(grdCatalogue, iRows, 9, Trim(drH(iRows)("Qty") & ""))
                fnSetdgvText(grdCatalogue, iRows, 10, Trim(drH(iRows)("ReOrderLevel") & ""))
                fnSetdgvText(grdCatalogue, iRows, 11, Trim(drH(iRows)("ReOrderLevel") & "") - Trim(drH(iRows)("Qty") & ""))
            Next
        End If
        drH = Nothing
        sSql = Nothing
    End Sub

    Private Sub btnFetch_Click(sender As System.Object, e As System.EventArgs) Handles btnFetch.Click
        subReOrderLevel()
    End Sub

    Private Sub grdCatalogue_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCatalogue.CellClick
        If e.ColumnIndex = 8 Then

        End If
    End Sub

    Private Sub grdCatalogue_Click(sender As Object, e As System.EventArgs) Handles grdCatalogue.Click

    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub btnNew_Click(sender As System.Object, e As System.EventArgs) Handles btnNew.Click
        AddItemstoComboL()
        LoadLocationcode()
        txtCategory.Text = ""
        txtCatalogue.Text = ""
        txtSupplierCode.Text = ""
        txtSONo.Text = ""
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        Dim i As Long
        Dim objTran As New clsTransferOut
        Try
            If grdCatalogue.Rows.Count < 0 Then
                MsgBox("No Item To Transfer", vbInformation)
                Exit Sub
            End If
            If cboLocationCode.Text.Trim = "" Then
                MsgBox("LocationCode Should not be Empty", vbInformation)
                cboLocationCode.Focus()
                Exit Sub
            End If
            If txtSupplierCode.Text.Trim = "" Then
                MsgBox("SupplierCode Should not be Empty", vbInformation)
                txtSupplierCode.Focus()
                Exit Sub
            End If
            If MsgBox("Do want to Create SalesOrder for all  these items ?", vbYesNo + MsgBoxStyle.Information) = MsgBoxResult.No Then
                Exit Sub
            End If
            If fncUpdateSalesOrder("I") Then
                For i = 0 To grdCatalogue.Rows.Count - 1

                Next
                ''' subDeleteTemp()
                grdCatalogue.Rows.Clear()
                ''  subTotal()
                ' ''If txtRefNo.Text <> "" And gsLocationCode <> "HQ" Then
                ' ''    fncUpdateTransferToHQ(txtRefNo.Text)
                ' ''End If
                ''subClear()
                btnNew_Click(sender, e)
            End If
            cboLocationCode.Focus()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        Finally
            objTran = Nothing
        End Try
    End Sub
    Private Function fncUpdateSalesOrder(ByVal pMode) As Boolean
        Dim sTranNo As String
        Dim dsHeader As New DataSet
        Dim dsDetail As New DataSet
        Dim dr As DataRow
        Dim drDetail As DataRow
        Dim iRow As Long
        Dim sUpdate As String
        Dim objTran As New clsSalesOrder
        Dim sSalesOrderDate As String
        Dim objInventory As clsInventory
        Dim sDueDate As String

        fncUpdateSalesOrder = False

        sSalesOrderDate = Trim(ServerDate)

        sSalesOrderNo = objTran.fncSalesOrderNo()
        If sSalesOrderNo.Trim = "" Then
            MessageBox.Show("Error in generating SalesOrderNo")
            Exit Function
        End If
        txtSONo.Text = sSalesOrderNo
        'sPoType = "0"
        sDueDate = Trim(txtIDD.Text) & "/" & Trim(txtIMM.Text) & "/" & Trim(txtIYYYY.Text)

        'sDueDate = Trim(txtIYYYY.Text) & Trim(txtIMM.Text) & Trim(txtIDD.Text)

        dsDetail = objTran.fncGetSalesOrderDetail(gsCon, "Det", "")

        For iRow = 0 To grdCatalogue.Rows.Count - 1
            
            If (fnGetdgvText(grdCatalogue, iRow, 0)) <> "" Then
                drDetail = dsDetail.Tables("Det").NewRow()
                drDetail.Item("SalesOrderNo") = sSalesOrderNo
                drDetail.Item("SalesOrderDate") = sSalesOrderDate
                drDetail.Item("DueDate") = sDueDate
                drDetail.Item("LocationCode") = Trim(cboLocationCode.Text)
                drDetail.Item("SupplierCode") = Trim(txtSupplierCode.Text)
                drDetail.Item("CategoryCode") = Trim(fnGetdgvText(grdCatalogue, iRow, cCategoryCode.Index))
                drDetail.Item("CatalogueCode") = Trim(fnGetdgvText(grdCatalogue, iRow, cCatalogueCode.Index))
                drDetail.Item("Weight") = Val(fnGetdgvText(grdCatalogue, iRow, cWeight.Index))
                drDetail.Item("ToWeight") = Val(fnGetdgvText(grdCatalogue, iRow, cToWeight.Index))
                drDetail.Item("SizeCode") = Val(fnGetdgvText(grdCatalogue, iRow, cSizeCode.Index))
                drDetail.Item("SizeType") = Trim(fnGetdgvText(grdCatalogue, iRow, cSizeType.Index))
                drDetail.Item("ReOrderLevel") = Val(fnGetdgvText(grdCatalogue, iRow, cReOrderLevel.Index))
                drDetail.Item("SalesOrderQty") = Val(fnGetdgvText(grdCatalogue, iRow, cSOQty.Index))
                drDetail.Item("Remarks") = Trim(txtRemarks.Text)
                drDetail.Item("CreateUser") = Objuser.UserId
                drDetail.Item("ModifyUser") = Objuser.UserId
                dsDetail.Tables("Det").Rows.Add(drDetail)
                'fncUpdatePending(iRow)
            End If
        Next iRow

        sTranNo = objTran.UpdateSalesOrder(dsDetail, pMode)
        'If sTranNo = "" Then
        '    Exit Function
        'End If

        objTran.subOpenTransaferConnection()
        objTran.subBeginTransaferTransaction()
        If Not objTran.fncUpdateNextSalesOrderNo() Then
            MessageBox.Show("Error in updating SalesOrder No")
            objTran.subRollBackTransaferTransaction()
            Exit Function
        End If
        If Not objTran.SalesOrderHeaderInsert(txtSONo.Text.Trim) Then
            MessageBox.Show("Error in updating SalesOrder Header")
            objTran.subRollBackTransaferTransaction()
        End If

        objTran.subCommitTransaferTransaction()
        objTran.subCloseTransaferConnection()
        MsgBox("Please take note reference no. " + Trim(txtSONo.Text))




        If MsgBox("Do you want to print ?", vbYesNo + MsgBoxStyle.Information) = MsgBoxResult.Yes Then
            subPrintSalesOrder(Trim(txtSONo.Text))
        End If
        fncUpdateSalesOrder = True
        dsHeader = Nothing
        dsDetail = Nothing
        objTran = Nothing
        Exit Function
subUpdateError:
        fncUpdateSalesOrder = False
        MsgBox(Err.Description)
        dsHeader = Nothing
        dsDetail = Nothing
        objTran = Nothing
    End Function
    Private Sub subPrintSalesOrder(ByVal sTransferNo As String)
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
            sPath = Application.StartupPath & "\Reports\rptSalesOrderDetail.rpt"
            'End If
            sReportTitle = "Transfer Note"
            sSql = " {tblSalesOrderDetail.SalesOrderNo} = " & fncFormatSQLText(sTransferNo)
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

    Private Sub btnSupplierCodeSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSupplierCodeSearch.Click
        Try
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSupplier", "L", 4, "SupplierName", "", "SupplierCode", "SupplierCode", 80, "SupplierName", "SupplierName", 180, "Address1", "Address1", 60, "Address2", "Address2", 80)
            frmHelpClass.ShowDialog()
            txtSupplierCode.Text = frmHelpClass.Var1.Trim
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnRePrint_Click(sender As System.Object, e As System.EventArgs) Handles btnRePrint.Click
        If Trim(txtSONo.Text) <> "" Then
            subPrintSalesOrder(Trim(txtSONo.Text))
        End If
    End Sub

    Private Sub btnDelete_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete.Click
        If Trim(txtSONo.Text) <> "" Then
            subDelete(Trim(txtSONo.Text))
        Else
            MsgBox("SalesOrder Number Should not be Empty", vbInformation)
            txtSONo.Focus()
            Exit Sub
        End If
    End Sub
    Private Function fncGetSalesOrderTran(ByVal sSalesOrderNo As String) As Boolean
        Dim DvCustomer() As DataRow
        Dim DM As New DataManager
        fncGetSalesOrderTran = False
        DvCustomer = DM.RowReturnProjExecNonQryWithDataRow("Select * from tblJobSheetHeader where SalesOrderNo=" & fncFormatSQLText(sSalesOrderNo))
        If DvCustomer.Length > 0 Then
            fncGetSalesOrderTran = True
        Else
            fncGetSalesOrderTran = False
        End If
    End Function

    Private Sub subDelete(ByVal sInvoiceNo As String)
        Dim objTran As New clsSalesOrder
        Try

            If fncGetSalesOrderTran(txtSONo.Text.Trim) = True Then
                MsgBox("Can Not Delete SalesOrder , Beacuse This SalesOrder Exists in JobSheet", MsgBoxStyle.Information)
                Exit Sub
            Else
                If MessageBox.Show("Do you want to delete SalesOrder No " & Trim(txtSONo.Text), "GoldPOS", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    objTran.subOpenTransaferConnection()
                    objTran.subBeginTransaferTransaction()
                    If Not objTran.SalesOrderDelete(txtSONo.Text.Trim) Then
                        MessageBox.Show("Error in Delete.")
                        objTran.subRollBackTransaferTransaction()
                        objTran.subCloseTransaferConnection()
                    Else
                        MessageBox.Show("Sales Order Deleted")
                        objTran.subCommitTransaferTransaction()
                        objTran.subCloseTransaferConnection()
                    End If
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class