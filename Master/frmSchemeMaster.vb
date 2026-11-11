Public Class frmSchemeMaster

    Dim objScheme As New clsScheme
    Dim DSSize As New DataSet
    Dim DM As New DataManager
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        subClearMe()
    End Sub

    Private Sub subClearMe()
        txtCode.Clear()
        txtDescription.Clear()
        txtRemarks.Clear()
        btnDelete.Enabled = False
        txtCode.ReadOnly = False
        txtCode.Focus()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        subDelete()
    End Sub
    Private Sub subDelete()
        Try
            If dgvHeader.RowCount > 0 And dgvHeader.CurrentRow.Index > 0 Then
                Dim sCode As String
                Dim sDescription As String
                sCode = fnGetdgvText(dgvHeader, dgvHeader.CurrentRow.Index, Code.Index).Trim
                sDescription = fnGetdgvText(dgvHeader, dgvHeader.CurrentRow.Index, Description.Index).Trim
                If fncGetSchemeDetail(sCode) = True Then
                    MsgBox("Can Not Delete Scheme Master , Beacuse This Scheme Is Having Some Details", MsgBoxStyle.Information)
                    Exit Sub
                Else
                    If MessageBox.Show("Do you want to delete " & fnGetdgvText(dgvHeader, dgvHeader.CurrentRow.Index, Code.Index).Trim & " Scheme?", "GoldPOS", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        If objScheme.SchemeMasterDelete(sCode, sDescription) Then
                            MessageBox.Show("Scheme detail deleted.")
                            subClearMe()
                            subLoadDataIntoGrid()
                            dgvHeader.Focus()
                            subShowSchemeDetail(dgvHeader.CurrentRow.Index)
                        Else
                            MessageBox.Show("Error in deleting Scheme detail.")
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function fncGetSchemeDetail(ByVal sCode As String) As Boolean
        Dim DvCustomer() As DataRow
        Dim DM As New DataManager
        fncGetSchemeDetail = False
        DvCustomer = DataManager.RowReturnProjExecNonQryWithDataRow("Select * from tblSchemeDetail where Code=" & fncFormatSQLText(sCode))
        If DvCustomer.Length > 0 Then
            fncGetSchemeDetail = True
        Else
            fncGetSchemeDetail = False
        End If
    End Function
    Private Sub frmSchemeMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub frmSchemeMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        subLoadDataIntoGrid()
        subShowSchemeDetail(dgvHeader.CurrentRow.Index)
    End Sub
    Private Sub subLoadDataIntoGrid()
        'Dim DS As DataSet
        Dim iCount, iRows
        Dim sCode, sDescription, sRemarks As String
        Dim sSql As String

        Try
            subClearMe()
            dgvHeader.Rows.Clear()


            DSSize = objScheme.GetScheme

            iCount = DSSize.Tables("getScheme").Rows.Count
            If iCount = 0 Then Exit Sub

            With DSSize.Tables("getScheme")
                For iRows = 0 To iCount - 1
                    dgvHeader.Rows.Add()

                    sCode = .Rows(iRows)("Code").Trim & ""
                    sDescription = .Rows(iRows)("Description")
                    sRemarks = .Rows(iRows)("Remarks").Trim & ""

                    fnSetdgvText(dgvHeader, iRows, Code.Index, sCode)
                    fnSetdgvText(dgvHeader, iRows, Description.Index, sDescription)
                    fnSetdgvText(dgvHeader, iRows, Remarks.Index, sRemarks)
                Next
            End With


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try

    End Sub
    Private Sub subShowSchemeDetail(ByVal iRow As Integer)
        Try
            btnDelete.Enabled = True
            If dgvHeader.RowCount = 0 Then Exit Sub

            txtCode.Text = fnGetdgvText(dgvHeader, dgvHeader.CurrentRow.Index, Code.Index).Trim & ""
            txtDescription.Text = fnGetdgvText(dgvHeader, dgvHeader.CurrentRow.Index, Description.Index).Trim & ""
            txtRemarks.Text = fnGetdgvText(dgvHeader, dgvHeader.CurrentRow.Index, Remarks.Index).Trim & ""

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        subSave()
    End Sub
    Private Sub subSave()
        Try
            If subValidateMe() Then
                If objScheme.SchemeMasterSave(txtCode.Text.Trim, txtDescription.Text.Trim, txtRemarks.Text.Trim, "I") Then
                    MessageBox.Show("Size detail saved sucssfully.")
                    subClearMe()
                    subLoadDataIntoGrid()
                    dgvHeader.Focus()
                    subShowSchemeDetail(dgvHeader.CurrentRow.Index)
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
            If txtCode.Text.Trim = "" Then
                MessageBox.Show("SchemeCode should not be blank.")
                txtCode.Focus()
                Exit Function
            End If
            If txtDescription.Text.Trim = "" Then
                MessageBox.Show("Scheme Description should not be blank.")
                txtDescription.Focus()
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
    Private Sub dgvHeader_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvHeader.KeyUp
        Try
            If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then subShowSchemeDetail(dgvHeader.CurrentRow.Index)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub dgvHeader_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvHeader.MouseClick
        Try
            subShowSchemeDetail(dgvHeader.CurrentRow.Index)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

End Class