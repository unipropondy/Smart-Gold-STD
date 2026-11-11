Public Class frmSize
    Dim DSSize As New DataSet
    Dim DM As New DataManager
    Dim objClsCatalogue As New ClsCatalogue
    Dim objClsCategory As New ClsCategory
    Private Sub subLoadDataIntoGrid()
        'Dim DS As DataSet
        Dim iCount, iRows
        Dim sCategoryCode, sSizeCode, sSizeType, sRemarks As String
        Dim sSql As String

        Try
            subClearMe()
            dgvHeader.Rows.Clear()
            '''DSSize = objClsCatalogue.fncGetSize(usCon, "Size")
            ''sSql = "Select * from tblSize"
            ''DSSize = DM.getDataSetWithSqlAdapter(sSql, "Size", "Size")



            DSSize = objClsCatalogue.GetSize

            iCount = DSSize.Tables("getSize").Rows.Count
            If iCount = 0 Then Exit Sub

            With DSSize.Tables("getSize")
                For iRows = 0 To iCount - 1
                    dgvHeader.Rows.Add()

                    sCategoryCode = .Rows(iRows)("CategoryCode").Trim & ""
                    sSizeCode = .Rows(iRows)("SizeCode")
                    sSizeType = .Rows(iRows)("SizeType").Trim & ""
                    sRemarks = .Rows(iRows)("Remarks").Trim & ""

                    fnSetdgvText(dgvHeader, iRows, CategoryCode.Index, sCategoryCode)
                    fnSetdgvText(dgvHeader, iRows, SizeCode.Index, sSizeCode)
                    fnSetdgvText(dgvHeader, iRows, SizeType.Index, sSizeType)
                    fnSetdgvText(dgvHeader, iRows, Remarks.Index, sRemarks)
                Next
            End With


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try

    End Sub

    Private Sub subClearMe()
        txtCategoryCode.Clear()
        txtCategoryName.Clear()
        txtSizeCode.Clear()
        txtSizeType.Clear()
        cboSizeType.Text = ""
        txtRemarks.Clear()
        btnDelete.Enabled = False
        txtCategoryCode.ReadOnly = False
        txtCategoryCode.Focus()
    End Sub

    Private Sub btnNew_Click(sender As System.Object, e As System.EventArgs) Handles btnNew.Click
        subClearMe()
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnCatagorySearch_Click(sender As System.Object, e As System.EventArgs) Handles btnCatagorySearch.Click
        Dim frmHelpCategory As New frmHelpSearch(0, 0, "tblCategory", "L", 4, "CategoryCode", "SizeFactor=1", "CategoryCode", "CategoryCode", 80, "CategoryName", "CategoryName", 180, "ClassCode", "ClassCode", 60, "Remarks", "Remarks", 80)
        frmHelpCategory.ShowDialog()
        txtCategoryCode.Text = frmHelpCategory.Var1.Trim
        txtCategoryName.Text = frmHelpCategory.Var2.Trim
        If txtSizeCode.Enabled Then txtSizeCode.Focus()
    End Sub

    Private Sub btnDelete_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete.Click
        subDelete()
    End Sub
    Private Sub subDelete()
        Try
            If dgvHeader.RowCount > 0 And dgvHeader.CurrentRow.Index > 0 Then
                Dim sSizeCode As String
                Dim sCategoryCode As String
                sSizeCode = fnGetdgvText(dgvHeader, dgvHeader.CurrentRow.Index, SizeCode.Index).Trim
                sCategoryCode = fnGetdgvText(dgvHeader, dgvHeader.CurrentRow.Index, CategoryCode.Index).Trim
                If fncGetCatalogueTran(sSizeCode) = True Then
                    MsgBox("Can Not Delete Catalogue , Beacuse This Catalogue Is Having Some Inventory", MsgBoxStyle.Information)
                    Exit Sub
                Else
                    If MessageBox.Show("Do you want to delete " & fnGetdgvText(dgvHeader, dgvHeader.CurrentRow.Index, SizeCode.Index).Trim & " Size Code?", "GoldPOS", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        If objClsCatalogue.SizeDelete(sSizeCode, sCategoryCode) Then
                            MessageBox.Show("Catalogue detail deleted.")
                            subClearMe()
                            subLoadDataIntoGrid()
                            dgvHeader.Focus()
                            subShowSizeDetail(dgvHeader.CurrentRow.Index)
                        Else
                            MessageBox.Show("Error in deleting Catalogue detail.")
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function fncGetCatalogueTran(ByVal sCode As String) As Boolean
        Dim DvCustomer() As DataRow
        Dim DM As New DataManager
        fncGetCatalogueTran = False
        DvCustomer = DM.RowReturnProjExecNonQryWithDataRow("Select * from tblInventory where CatalogueCode=" & fncFormatSQLText(sCode))
        If DvCustomer.Length > 0 Then
            fncGetCatalogueTran = True
        Else
            fncGetCatalogueTran = False
        End If
    End Function
    Private Sub subShowSizeDetail(ByVal iRow As Integer)
        Try
            btnDelete.Enabled = True
            If dgvHeader.RowCount = 0 Then Exit Sub

            txtCategoryCode.Text = fnGetdgvText(dgvHeader, dgvHeader.CurrentRow.Index, CategoryCode.Index).Trim & ""
            '  txtCategoryName.Text = fnGetText(dgvHeader, dgvHeader.CurrentRow.Index, CategoryName.Index).Trim & ""
            txtSizeCode.Text = fnGetdgvText(dgvHeader, dgvHeader.CurrentRow.Index, SizeCode.Index).Trim & ""
            cboSizeType.Text = fnGetdgvText(dgvHeader, dgvHeader.CurrentRow.Index, SizeType.Index).Trim & ""
            '  txtSizeType.Text = fnGetdgvText(dgvHeader, dgvHeader.CurrentRow.Index, SizeType.Index).Trim & ""
            txtRemarks.Text = fnGetdgvText(dgvHeader, dgvHeader.CurrentRow.Index, Remarks.Index).Trim & ""

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        subSave()
    End Sub
    Private Sub subSave()
        Try
            If subValidateMe() Then
                If objClsCatalogue.SizeSave(txtCategoryCode.Text.Trim, txtSizeCode.Text.Trim, cboSizeType.Text.Trim, txtRemarks.Text.Trim) Then
                    MessageBox.Show("Size detail saved sucssfully.")
                    subClearMe()
                    subLoadDataIntoGrid()
                    dgvHeader.Focus()
                    subShowSizeDetail(dgvHeader.CurrentRow.Index)
                Else
                    MessageBox.Show("Error in Save")
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
            If txtSizeCode.Text.Trim = "" Then
                MessageBox.Show("SizeCode should not be blank.")
                txtSizeCode.Focus()
                Exit Function
            End If
            If cboSizeType.Text.Trim = "" Then
                MessageBox.Show("SizeType should not be blank.")
                cboSizeType.Focus()
                Exit Function
            End If
            If txtCategoryCode.Text.Trim = "" Then
                MessageBox.Show("CategoryCode should not be blank.")
                txtCategoryCode.Focus()
                Exit Function
            End If
            'check valid category code
            If IsNothing(objClsCategory.GetCategoryName(txtCategoryCode.Text.Trim)) Then
                MessageBox.Show("Pleae enter valid CatagoryCode.")
                txtCategoryCode.Clear()
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

    Private Sub frmSize_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F6
                btnNew_Click(sender, e)
            Case Keys.F7
                btnSave_Click(sender, e)
            Case Keys.F8
                If btnDelete.Enabled Then btnDelete_Click(sender, e)
            Case Keys.Escape
                btnClose_Click(sender, e)
            Case Keys.Return
                SendKeys.Send("{Tab}")
                'Case Keys.Up
                'SendKeys.Send("+{Tab}")
        End Select
    End Sub

    Private Sub frmSize_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        subLoadDataIntoGrid()
        subShowSizeDetail(dgvHeader.CurrentRow.Index)
        If Trim(sCompanyShortCode) = "AJ" Then
            Label6.Visible = False
            txtRemarks.Visible = False
        End If
    End Sub

  

    Private Sub dgvHeader_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles dgvHeader.KeyUp
        Try
            If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then subShowSizeDetail(dgvHeader.CurrentRow.Index)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub dgvHeader_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles dgvHeader.MouseClick
        Try
            subShowSizeDetail(dgvHeader.CurrentRow.Index)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

   
    Private Sub cboSizeType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboSizeType.SelectedIndexChanged

    End Sub
End Class