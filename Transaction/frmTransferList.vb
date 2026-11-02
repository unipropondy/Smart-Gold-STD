Public Class frmTransferList
    Dim dm As New DataManager
    Dim bPending As Boolean
    Dim sLocation, sTransferDate As String
    Dim sToLocStatus As String
    Private Sub frmTransferList_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            subLoadDetails()
            cboLocation.Focus()
            cboLocation.Text = Trim(gthisLocation)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subLoadDetails()
        Try
            Dim sDate As String
            sDate = Format(ServerDate, "dd/MM/yyyy")
            txtTransferDate.Text = Mid(Trim(sDate), 1, 2)
            txtTransferMonth.Text = Mid(Trim(sDate), 4, 2)
            txtTransferYear.Text = Mid(Trim(sDate), 7, 4)
            If gthisLocation = "HQ" Then
                AddItemstoComboL()
                cboLocation.SelectedIndex = 0
            Else
                cboLocation.Items.Add(gthisLocation)
                cboLocation.SelectedIndex = 0
            End If
            cboLocation.Text = (gthisLocation)
            bPending = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Sub AddItemstoComboL()
        dm.SetMasterDataSetLoc("Select rtrim(LocationCode)LocationCode,rtrim(LocationName)LocationName from tblLocation Order by SortCode", "tbllocation")
        dm.FillDatasToComboListWithFilters(cboLocation, "tbllocation", "LocationCode", "LocationName")
    End Sub

    Private Sub cmdFetch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFetch.Click
        Cursor.Current = Cursors.WaitCursor
        Try
            sLocation = Trim(cboLocation.Text)
            sTransferDate = Trim(txtTransferDate.Text) & "/" & Trim(txtTransferMonth.Text) & "/" & Trim(txtTransferYear.Text)
            If fncCheckData(sLocation, sTransferDate) = True Then
                If rbtnTransferout.Checked = True Then
                    subTransferOut(sLocation, sTransferDate)
                ElseIf rbtnTransferIn.Checked = True Then
                    subTransferIn(sLocation, sTransferDate)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Function fncCheckData(ByVal sLocationCode As String, ByVal sTransferDate As String) As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            If sLocationCode = "" Then
                MessageBox.Show("Pease select location.")
                If cboLocation.Enabled Then cboLocation.Focus()
                Exit Function
            End If
            If rbtnTransferIn.Checked = False And rbtnTransferout.Checked = False Then
                MessageBox.Show("Please select TranferIn / TransferOut")
                If rbtnTransferIn.Enabled Then rbtnTransferIn.Focus()
                Exit Function
            End If
            If (txtTransferDate.Text = "" Or txtTransferMonth.Text = "" Or txtTransferYear.Text = "") And bPending = False Then
                MessageBox.Show("Please enter date.")
                If txtTransferDate.Enabled Then txtTransferDate.Focus()
                Exit Function
            End If
            If IsDate(sTransferDate) = False Then
                MessageBox.Show("Please enter valid date.")
                If txtTransferDate.Enabled Then txtTransferDate.Focus()
                Exit Function
            End If
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncCheckData = bStatus
        End Try
    End Function
    Private Sub subTransferOut(ByVal sLocation As String, ByVal sTransferDate As String)
        Dim sSql, sFromLocStatus As String
        Dim iRows, iCount As Integer
        Dim drH As DataRow()

        'sSql &= "Select * from tblInventoryTransferHeader Where FromLocation = " & fncFormatSQLText(sLocation)
        'sSql &= " And TransferDate = " & fncFormatSQLDate(sTransferDate)

        sSql &= "Select * from tblInventoryTransferHeader Where FromLocation = " & fncFormatSQLText(sLocation)
        sSql &= " And TransferDate = " & fncFormatSQLDate(sTransferDate)

        drH = DM.RowReturnProjExecNonQryWithDataRow(sSql)
        dgvTransfer.RowCount = 0
        If drH.Length > 0 Then
            For iRows = 0 To drH.Length - 1
                dgvTransfer.Rows.Add()


                fnSetdgvText(dgvTransfer, iRows, 0, Trim(drH(iRows)("TransferNo") & ""))
                fnSetdgvText(dgvTransfer, iRows, 1, Trim(drH(iRows)("TransferDate") & ""))

                If Trim(drH(iRows)("FromLocStatus") & "") = 2 Then
                    sFromLocStatus = " Transferred"
                End If
                fnSetdgvText(dgvTransfer, iRows, 2, sFromLocStatus)
                If Trim(drH(iRows)("ToLocStatus") & "") = 0 Then
                    sToLocStatus = " Pending..."
                Else
                    sToLocStatus = " Accepted"
                End If
                fnSetdgvText(dgvTransfer, iRows, 3, sToLocStatus)
                fnSetdgvText(dgvTransfer, iRows, 4, Trim(drH(iRows)("ToLocation") & ""))
                fnSetdgvText(dgvTransfer, iRows, 5, Trim(drH(iRows)("TotalPCS") & ""))
                fnSetdgvText(dgvTransfer, iRows, 6, Trim(drH(iRows)("TotalPCS") & ""))
            Next
        Else
            MessageBox.Show("No transfer from " & sLocation)
            If cboLocation.Enabled Then cboLocation.Focus()
        End If
        drH = Nothing
        sSql = Nothing
    End Sub

    Private Sub subTransferIn(ByVal sLocation As String, ByVal sTransferDate As String)
        Dim sSql, sFromLocStatus, sToLocStatus As String
        Dim iRows, iCount As Integer
        Dim iCnt As Integer
        Dim drH As DataRow()

        'sSql &= "Select * From tblInventoryTransferHeader Where ToLocation = " & fncFormatSQLText(sLocation)
        'sSql &= " And TransferDate = " & fncFormatSQLDate(sTransferDate)


        sSql &= "Select TransferNo,TransferDate,FromLocStatus,ToLocStatus,TotalPCS,FromLocation,ToLocation from tblInventoryTransferHeader Where ToLocation = " & fncFormatSQLText(sLocation)
        sSql &= " And TransferDate = " & fncFormatSQLDate(sTransferDate)


        drH = dm.RowReturnProjExecNonQryWithDataRowAcc(sSql)
        dgvTransfer.RowCount = 0
        If drH.Length > 0 Then
            For iRows = 0 To drH.Length - 1
                dgvTransfer.Rows.Add()


                fnSetdgvText(dgvTransfer, iRows, 0, Trim(drH(iRows)("TransferNo") & ""))
                fnSetdgvText(dgvTransfer, iRows, 1, Trim(drH(iRows)("TransferDate") & ""))
                'fnSetdgvText(dgvTransfer, iRows, 2, Trim(drH(iRows)("FromLocStatus") & ""))
                'fnSetdgvText(dgvTransfer, iRows, 3, Trim(drH(iRows)("ToLocStatus") & ""))
                If Trim(drH(iRows)("FromLocStatus") & "") = 2 Then
                    sFromLocStatus = " Transferred"
                End If
                fnSetdgvText(dgvTransfer, iRows, 2, sFromLocStatus)
                If Trim(drH(iRows)("ToLocStatus") & "") = 0 Then
                    sToLocStatus = " Pending..."
                Else
                    sToLocStatus = " Accepted"
                End If
                fnSetdgvText(dgvTransfer, iRows, 3, sToLocStatus)
                fnSetdgvText(dgvTransfer, iRows, 4, "")
                fnSetdgvText(dgvTransfer, iRows, 5, Trim(drH(iRows)("TotalPCS") & ""))
                fnSetdgvText(dgvTransfer, iRows, 6, Trim(drH(iRows)("TotalPCS") & ""))
            Next
        Else
        MessageBox.Show("No transfer from " & sLocation)
        If cboLocation.Enabled Then cboLocation.Focus()
        End If
        drH = Nothing
        sSql = Nothing
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub cmdClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransferClear.Click
        Try
            txtTransferDate.Text = ""
            txtTransferMonth.Text = ""
            txtTransferYear.Text = ""
            dgvTransfer.RowCount = 0
            bPending = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

 
    Private Sub btnPending_Click(sender As System.Object, e As System.EventArgs) Handles btnPending.Click
        Cursor.Current = Cursors.WaitCursor
        Try
            bPending = True
            sLocation = Trim(cboLocation.Text)
            'sTransferDate = Trim(txtDay.Text) & "/" & Trim(txtMonth.Text) & "/" & Trim(txtYear.Text)
            sTransferDate = "01/01/1900"
            If fncCheckData(sLocation, sTransferDate) = True Then
                If rbtnTransferout.Checked = True Then
                    subPendingTransferOut(sLocation, sTransferDate)
                ElseIf rbtnTransferIn.Checked = True Then
                    subPendingTransferIn(sLocation, sTransferDate)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub
    Private Sub subPendingTransferIn(ByVal sLocation As String, ByVal sTransferDate As String)
        Dim sSql, sFromLocStatus, sToLocStatus As String
        Dim iRows As Integer
        Dim drH As DataRow()

        'sSql &= "Select * From tblInventoryTransferHeader Where ToLocation = " & fncFormatSQLText(sLocation)
        'sSql &= " And TransferDate = " & fncFormatSQLDate(sTransferDate)


        sSql &= "Select TransferNo,TransferDate,FromLocStatus,ToLocStatus,TotalPCS,FromLocation from tblInventoryTransferHeader Where ToLocation = " & fncFormatSQLText(sLocation)
        sSql &= " and ToLocStatus=0 "


        drH = dm.RowReturnProjExecNonQryWithDataRowAcc(sSql)
        dgvTransfer.RowCount = 0
        If drH.Length > 0 Then
            For iRows = 0 To drH.Length - 1
                dgvTransfer.Rows.Add()


                fnSetdgvText(dgvTransfer, iRows, 0, Trim(drH(iRows)("TransferNo") & ""))
                fnSetdgvText(dgvTransfer, iRows, 1, Trim(drH(iRows)("TransferDate") & ""))
                'fnSetdgvText(dgvTransfer, iRows, 2, Trim(drH(iRows)("FromLocStatus") & ""))
                'fnSetdgvText(dgvTransfer, iRows, 3, Trim(drH(iRows)("ToLocStatus") & ""))
                If Trim(drH(iRows)("FromLocStatus") & "") = 2 Then
                    sFromLocStatus = " Transferred"
                End If
                fnSetdgvText(dgvTransfer, iRows, 2, sFromLocStatus)
                If Trim(drH(iRows)("ToLocStatus") & "") = 0 Then
                    sToLocStatus = " Pending..."
                Else
                    sToLocStatus = " Accepted"
                End If
                fnSetdgvText(dgvTransfer, iRows, 3, sToLocStatus)
                fnSetdgvText(dgvTransfer, iRows, 4, Trim(drH(iRows)("FromLocation") & ""))
                fnSetdgvText(dgvTransfer, iRows, 5, Trim(drH(iRows)("TotalPCS") & ""))
                fnSetdgvText(dgvTransfer, iRows, 6, Trim(drH(iRows)("TotalPCS") & ""))
            Next
        Else
            MessageBox.Show("No transfer from " & sLocation)
            If cboLocation.Enabled Then cboLocation.Focus()
        End If
        drH = Nothing
        sSql = Nothing
    End Sub
    Private Sub subPendingTransferOut(ByVal sLocation As String, ByVal sTransferDate As String)
        Dim sSql, sFromLocStatus As String
        Dim iRows As Integer
        Dim drH As DataRow()

        'sSql &= "Select * from tblInventoryTransferHeader Where FromLocation = " & fncFormatSQLText(sLocation)
        'sSql &= " And TransferDate = " & fncFormatSQLDate(sTransferDate)

        sSql &= "Select * from tblInventoryTransferHeader Where FromLocation = " & fncFormatSQLText(sLocation)
        sSql &= " And TransferDate >= " & fncFormatSQLDate(sTransferDate) & " and ToLocStatus=0 "

        drH = DM.RowReturnProjExecNonQryWithDataRow(sSql)
       dgvTransfer.RowCount = 0
        If drH.Length > 0 Then
            For iRows = 0 To drH.Length - 1
                dgvTransfer.Rows.Add()


                fnSetdgvText(dgvTransfer, iRows, 0, Trim(drH(iRows)("TransferNo") & ""))
                fnSetdgvText(dgvTransfer, iRows, 1, Trim(drH(iRows)("TransferDate") & ""))
                'fnSetdgvText(dgvTransfer, iRows, 2, Trim(drH(iRows)("FromLocStatus") & ""))
                'fnSetdgvText(dgvTransfer, iRows, 3, Trim(drH(iRows)("ToLocStatus") & ""))
                If Trim(drH(iRows)("FromLocStatus") & "") = 2 Then
                    sFromLocStatus = " Transferred"
                End If
                fnSetdgvText(dgvTransfer, iRows, 2, sFromLocStatus)
                If Trim(drH(iRows)("ToLocStatus") & "") = 0 Then
                    sToLocStatus = " Pending..."
                Else
                    sToLocStatus = " Accepted"
                End If
                fnSetdgvText(dgvTransfer, iRows, 4, Trim(drH(iRows)("ToLocation") & ""))
                fnSetdgvText(dgvTransfer, iRows, 5, Trim(drH(iRows)("TotalPCS") & ""))
                fnSetdgvText(dgvTransfer, iRows, 6, Trim(drH(iRows)("TotalPCS") & ""))
            Next
        Else
            MessageBox.Show("No transfer from " & sLocation)
            If cboLocation.Enabled Then cboLocation.Focus()
        End If
        drH = Nothing
        sSql = Nothing
    End Sub

    Private Sub btnView_Click(sender As System.Object, e As System.EventArgs) Handles btnView.Click
        subTransferDetails()
        btnPending_Click(sender, e)
    End Sub
    Private Sub subTransferDetails()
        Dim sTransferNo As String
        Try
            If dgvTransfer.RowCount > 0 Then
                '''gTransferNo = Trim(fnGetText(grdTransferList, grdTransferList.ActiveRow, CTRANSFERNO))

                sTransferNo = fnGetdgvText(dgvTransfer, dgvTransfer.CurrentRow.Index, 0).Trim & ""
                If sTransferNo <> "" Then
                    '''gSelectedLocation = cboLocation.Text
                    If rbtnTransferIn.Checked Then
                        '''gSourceForm = "frmTransferList"
                        '''gSelectedLocation = Trim(cboLocation.Text)
                        '''gTransferType = "TransferIn"
                        Dim objTransferInForm As New frmTransferIn(Trim(sTransferNo))
                        objTransferInForm.ShowDialog()
                    ElseIf rbtnTransferout.Checked Then
                        '''gSourceForm = "frmTransferList"
                        '''gSelectedLocation = Trim(cboLocation.Text)
                        '''gTransferType = "TransferOut"
                        Dim objTranferOutForm As New frmTransferOut(sTransferNo)
                        objTranferOutForm.ShowDialog()
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            '''gTransferNo = ""
            '''gSourceForm = ""
            '''gSelectedLocation = ""
            '''gTransferType = ""
        End Try
    End Sub

    Private Sub dgvTransfer_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTransfer.CellContentClick

    End Sub

    Private Sub dgvTransfer_DoubleClick(sender As Object, e As System.EventArgs) Handles dgvTransfer.DoubleClick
        btnView_Click(sender, e)
    End Sub
End Class