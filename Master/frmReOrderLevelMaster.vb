Public Class frmReOrderLevelMaster
    Dim sSizeType As String
    Dim sToSizeType As String
    Dim objClsCatalogue As New ClsCatalogue
    Dim dm As New DataManager
    Private Sub subClearMe()
        Try
            txtCatalogueCode.ReadOnly = False
            txtCatalogueCode.Clear()
            txtCategoryCode.Clear()
            txtRemarks.Clear()
            txtSizeCode.Clear()
            sSizeType = ""
            txtFromWeight.Clear()
            txtToWeight.Clear()
            txtReOrderLevel.Clear()
            txtToSizeCode.Clear()
            sToSizeType = ""
            btnDelete.Enabled = False
            cboLocationCode.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
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
    Private Sub btnNew_Click(sender As System.Object, e As System.EventArgs) Handles btnNew.Click
        subClearMe()
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub btnCatagorySearch_Click(sender As System.Object, e As System.EventArgs) Handles btnCatagorySearch.Click
        Try
            Dim frmHelpCategory As New frmHelpSearch(0, 0, "tblCategory", "L", 4, "CategoryCode", "", "CategoryCode", "CategoryCode", 80, "CategoryName", "CategoryName", 180, "ClassCode", "ClassCode", 60, "Remarks", "Remarks", 80)
            frmHelpCategory.ShowDialog()
            txtCategoryCode.Text = frmHelpCategory.Var1.Trim
        Catch ex As Exception
            MessageBox.Show(Err.Description)
        End Try
    End Sub

    Private Sub btnCatalogueSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnCatalogueSearch.Click
        Try
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCatalogue", "L", 4, "CatalogueName", "CategoryCode=" & fncFormatSQLText(txtCategoryCode.Text), "CatalogueCode", "CatalogueCode", 80, "CatalogueName", "CatalogueName", 180, "CategoryCode", "CategoryCode", 60, "GoldLoss", "GoldLoss", 80)
            frmHelpClass.ShowDialog()
            txtCatalogueCode.Text = frmHelpClass.Var1.Trim
        Catch ex As Exception
            MessageBox.Show(Err.Description)
        End Try
    End Sub

    Private Sub btnSizeCodeSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSizeCodeSearch.Click
        Try
            Dim e1 As New System.EventArgs
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSize", "L", 4, "SizeCode", "categorycode=" & fncFormatSQLText(txtCategoryCode.Text), "SizeCode", "SizeCode", 80, "SizeType", "SizeType", 100, "CategoryCode", "CategoryCode", 100, "Remarks", "Remarks", 100)
            frmHelpClass.ShowDialog()
            txtSizeCode.Text = frmHelpClass.Var1.Trim
            sSizeType = frmHelpClass.Var2.Trim
            txtSizeCode_LostFocus(sender, e1)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtSizeCode_LostFocus(sender As Object, e As System.EventArgs) Handles txtSizeCode.LostFocus
        Dim Ds As DataSet
        Dim objSize As New ClsCatalogue
        Try
            If txtSizeCode.Text.Trim <> "" Then
                Ds = objSize.GetSizewithCode(txtSizeCode.Text.Trim, txtCategoryCode.Text.Trim)
                If Ds.Tables("Size").Rows.Count <= 0 Then
                    MsgBox("Invalid Size Code!", MsgBoxStyle.Critical)
                    If txtSizeCode.Enabled Then txtSizeCode.Focus()
                    Exit Sub
                Else
                    txtSizeCode.Text = Trim(Ds.Tables("Size").Rows(0)("SizeCode") & "")
                    sSizeType = Trim(Ds.Tables("Size").Rows(0)("SizeType") & "")
                    If txtToSizeCode.Enabled Then txtToSizeCode.Focus()
                End If
            End If
            Ds = Nothing
            objSize = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Private Sub txtToSizeCode_LostFocus(sender As Object, e As System.EventArgs) Handles txtToSizeCode.LostFocus
        Dim Ds As DataSet
        Dim objSize As New ClsCatalogue
        Try
            If txtToSizeCode.Text.Trim <> "" Then
                Ds = objSize.GetSizewithCode(txtToSizeCode.Text.Trim, txtCategoryCode.Text.Trim)
                If Ds.Tables("Size").Rows.Count <= 0 Then
                    MsgBox("Invalid Size Code!", MsgBoxStyle.Critical)
                    If txtToSizeCode.Enabled Then txtToSizeCode.Focus()
                    Exit Sub
                Else
                    txtToSizeCode.Text = Trim(Ds.Tables("Size").Rows(0)("SizeCode") & "")
                    sToSizeType = Trim(Ds.Tables("Size").Rows(0)("SizeType") & "")
                    If txtReOrderLevel.Enabled Then txtReOrderLevel.Focus()
                End If
            End If
            Ds = Nothing
            objSize = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        subSave()
    End Sub
    Private Sub subSave()
        Dim NewImage As Image
        Dim ByteArr() As Byte
        Try
            If subValidateMe() Then
               

                gsConBatchPosting.ConnectionString = gsCon
                gsConBatchPosting.Open()
                gsTrans = gsConBatchPosting.BeginTransaction


                If objClsCatalogue.ReOrderLevelMaster(txtCategoryCode.Text.Trim, txtCatalogueCode.Text.Trim, Val(txtFromWeight.Text), Val(txtToWeight.Text), Val(txtSizeCode.Text), Trim(sSizeType), Val(txtReOrderLevel.Text), txtRemarks.Text.Trim, cboLocationCode.Text, Val(txtToSizeCode.Text), Trim(sToSizeType)) Then
                    MessageBox.Show("ReOrder detail saved sucssfully.")
                    gsTrans.Commit()
                    gsConBatchPosting.Close()
                    subClearMe()
                    subGetReOrderDetails()
                    grdCatalogue.Focus()
                Else
                    MessageBox.Show("Error in Save")
                    gsTrans.Rollback()
                    gsConBatchPosting.Close()
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subDelete()
        Try
            If grdCatalogue.RowCount > 0 And grdCatalogue.CurrentRow.Index >= 0 Then
                Dim sCatalogueCode As String
                Dim sCategoryCode As String
                Dim dFromWeight As Decimal
                Dim dToWeight As Decimal
                Dim dSizeCode As Decimal
                Dim sSizeType As String
                Dim dToSizeCode As Decimal
                Dim sToSizeType As String
                Dim sLocationCode As String
                sLocationCode = fnGetdgvText(grdCatalogue, grdCatalogue.CurrentRow.Index, 0)
                sCatalogueCode = fnGetdgvText(grdCatalogue, grdCatalogue.CurrentRow.Index, 2)
                sCategoryCode = fnGetdgvText(grdCatalogue, grdCatalogue.CurrentRow.Index, 1)
                dFromWeight = fnGetdgvText(grdCatalogue, grdCatalogue.CurrentRow.Index, 3)
                dToWeight = fnGetdgvText(grdCatalogue, grdCatalogue.CurrentRow.Index, 4)
                dSizeCode = fnGetdgvText(grdCatalogue, grdCatalogue.CurrentRow.Index, 5)
                sSizeType = fnGetdgvText(grdCatalogue, grdCatalogue.CurrentRow.Index, 6)
                dToSizeCode = fnGetdgvText(grdCatalogue, grdCatalogue.CurrentRow.Index, 7)
                sToSizeType = fnGetdgvText(grdCatalogue, grdCatalogue.CurrentRow.Index, 8)
                'If fncGetCatalogueTran(sCatalogueCode) = True Then
                '    MsgBox("Can Not Delete Catalogue , Beacuse This Catalogue Is Having Some Inventory", MsgBoxStyle.Information)
                '    Exit Sub
                'Else
                If MessageBox.Show("Do you want to delete " & fnGetdgvText(grdCatalogue, grdCatalogue.CurrentRow.Index, 1) & " Catalogue?", "GoldPOS", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    If objClsCatalogue.ReOrderDelete(sCatalogueCode, sCategoryCode, dFromWeight, dToWeight, dSizeCode, sSizeType, sLocationCode, dToSizeCode, sToSizeType) Then
                        MessageBox.Show("Catalogue detail deleted.")
                        subClearMe()
                        subGetReOrderDetails()
                        grdCatalogue.Focus()
                        'subShowCatalogueDetail(grdCatalogue.ActiveRow)
                    Else
                        MessageBox.Show("Error in deleting Catalogue detail.")
                    End If
                End If
                '            End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subGetReOrderDetails()
        Try
            Dim dsCatalogue As New DataSet
            Dim iCnt As Long
            dsCatalogue = objClsCatalogue.GetReOrderMaster()
            If Not IsNothing(dsCatalogue) Then
                If dsCatalogue.Tables("Catalogue").Rows.Count > 0 Then
                    grdCatalogue.RowCount = 0
                    For iCnt = 0 To dsCatalogue.Tables("Catalogue").Rows.Count - 1
                        grdCatalogue.Rows.Add()
                        fnSetdgvText(grdCatalogue, iCnt, 0, Trim(dsCatalogue.Tables("Catalogue").Rows(iCnt)("LocationCode") & ""))
                        fnSetdgvText(grdCatalogue, iCnt, 1, Trim(dsCatalogue.Tables("Catalogue").Rows(iCnt)("CategoryCode") & ""))
                        fnSetdgvText(grdCatalogue, iCnt, 2, Trim(dsCatalogue.Tables("Catalogue").Rows(iCnt)("CatalogueCode") & ""))

                        fnSetdgvText(grdCatalogue, iCnt, 3, Trim(dsCatalogue.Tables("Catalogue").Rows(iCnt)("Weight") & ""))
                        fnSetdgvText(grdCatalogue, iCnt, 4, Trim(dsCatalogue.Tables("Catalogue").Rows(iCnt)("ToWeight") & ""))
                        fnSetdgvText(grdCatalogue, iCnt, 5, Trim(dsCatalogue.Tables("Catalogue").Rows(iCnt)("SizeCode") & ""))
                        fnSetdgvText(grdCatalogue, iCnt, 6, Trim(dsCatalogue.Tables("Catalogue").Rows(iCnt)("SizeType") & ""))
                        fnSetdgvText(grdCatalogue, iCnt, 7, Trim(dsCatalogue.Tables("Catalogue").Rows(iCnt)("ToSizeCode") & ""))
                        fnSetdgvText(grdCatalogue, iCnt, 8, Trim(dsCatalogue.Tables("Catalogue").Rows(iCnt)("ToSizeType") & ""))
                        fnSetdgvText(grdCatalogue, iCnt, 9, Trim(dsCatalogue.Tables("Catalogue").Rows(iCnt)("ReOrderLevel") & ""))
                        fnSetdgvText(grdCatalogue, iCnt, 10, Trim(dsCatalogue.Tables("Catalogue").Rows(iCnt)("Remarks") & ""))
                    Next
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function subValidateMe() As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            If txtCatalogueCode.Text.Trim = "" Then
                MessageBox.Show("CatalogueCode should not be blank.")
                txtCatalogueCode.Focus()
                Exit Function
            End If
            If txtFromWeight.Text.Trim = "" Then
                MessageBox.Show("Weight should not be blank.")
                txtFromWeight.Focus()
                Exit Function
            End If
            If txtToWeight.Text.Trim = "" Then
                MessageBox.Show("Weight should not be blank.")
                txtToWeight.Focus()
                Exit Function
            End If
            If txtCategoryCode.Text.Trim = "" Then
                MessageBox.Show("CategoryCode should not be blank.")
                txtCategoryCode.Focus()
                Exit Function
            End If

            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            bStatus = False
        Finally
            subValidateMe = bStatus
        End Try
    End Function

    Private Sub frmReOrderLevelMaster_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                SendKeys.Send("{Tab}")
            Case Keys.Up
                SendKeys.Send("+{Tab}")
            Case Keys.Down
                SendKeys.Send("{Tab}")
        End Select
    End Sub

    Private Sub frmReOrderLevelMaster_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        AddItemstoComboL()
        LoadLocationcode()
        subGetReOrderDetails()
    End Sub
    Public Function GetDetailsFromGrid()
        Dim sGoldLoss As String
        Dim dsCatalogue As New DataSet
        Dim iCnt As Long
        btnDelete.Enabled = True
        cboLocationCode.Text = fnGetdgvText(grdCatalogue, grdCatalogue.CurrentRow.Index, cLocationCode.Index)
        txtCategoryCode.Text = fnGetdgvText(grdCatalogue, grdCatalogue.CurrentRow.Index, cCategoryCode.Index)
        txtCatalogueCode.Text = fnGetdgvText(grdCatalogue, grdCatalogue.CurrentRow.Index, cCatalogueCode.Index)
        txtFromWeight.Text = fnGetdgvText(grdCatalogue, grdCatalogue.CurrentRow.Index, cFromWeight.Index)
        txtToWeight.Text = fnGetdgvText(grdCatalogue, grdCatalogue.CurrentRow.Index, cToWeight.Index)
        txtSizeCode.Text = fnGetdgvText(grdCatalogue, grdCatalogue.CurrentRow.Index, cSizeCode.Index)
        sSizeType = fnGetdgvText(grdCatalogue, grdCatalogue.CurrentRow.Index, cSizeType.Index)
        txtToSizeCode.Text = fnGetdgvText(grdCatalogue, grdCatalogue.CurrentRow.Index, cToSizeCode.Index)
        sToSizeType = fnGetdgvText(grdCatalogue, grdCatalogue.CurrentRow.Index, cToSizeType.Index)
        txtRemarks.Text = fnGetdgvText(grdCatalogue, grdCatalogue.CurrentRow.Index, cRemarks.Index)
        txtReOrderLevel.Text = fnGetdgvText(grdCatalogue, grdCatalogue.CurrentRow.Index, cReOrderLevel.Index)


    End Function

    Private Sub grdCatalogue_Click(sender As Object, e As System.EventArgs) Handles grdCatalogue.Click
        GetDetailsFromGrid()
    End Sub

    Private Sub btnDelete_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete.Click
        subDelete()
    End Sub

    Private Sub txtSizeCode_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtSizeCode.TextChanged

    End Sub

    Private Sub Label2_Click(sender As System.Object, e As System.EventArgs) Handles Label2.Click

    End Sub

    Private Sub btnToSizeCodeSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnToSizeCodeSearch.Click
        Try
            Dim e1 As New System.EventArgs
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSize", "L", 4, "SizeCode", "categorycode=" & fncFormatSQLText(txtCategoryCode.Text), "SizeCode", "SizeCode", 80, "SizeType", "SizeType", 100, "CategoryCode", "CategoryCode", 100, "Remarks", "Remarks", 100)
            frmHelpClass.ShowDialog()
            txtToSizeCode.Text = frmHelpClass.Var1.Trim
            sSizeType = frmHelpClass.Var2.Trim
            txtToSizeCode_LostFocus(sender, e1)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class