Public Class frmCustomerTypeChange

    Inherits System.Windows.Forms.Form
    Dim objCust As New clsCustomer
    Dim dm As DataManager
    Const CustomerCode = 1
    Const CustomerName = 2
    Const Phone = 3
    Const Fax = 4

    Private Sub ClearMe()
        cboCustomerType.SelectedIndex = -1
        cboNewCustomerType.SelectedIndex = -1
        cboPoints.SelectedIndex = -1
        txtCustomerCode.Clear()
        txtFromPoints.Clear()
        txtToPoints.Clear()
        fncClearGrid()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ClearMe()
        cboCustomerType.Focus()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sQry As String
        Dim iRows As Long
        Dim sCustomercode As String
        Dim sCustomername As String
        Dim sTotalPoints As String
        Dim sOldCustomerType As String
        Dim sNewCustomerType As String
        Dim dFPoints As Integer
        Dim dTPoints As Integer
        Dim SaveStatus, bStatus As Boolean
        Try
            If validateMe() Then
                If cboNewCustomerType.Text.Trim = "" Then
                    MessageBox.Show("New Customer Type Should Not Be Blank.")
                    Exit Sub
                End If
                bStatus = False
                If cboCustomerType.Text.Trim = cboNewCustomerType.Text.Trim Then
                    MessageBox.Show("Customer Type Should Not Be Same.")
                    Exit Sub
                End If

                For iRows = 0 To grdCustomerType.RowCount - 1
                    If Trim(fnGetdgvText(grdCustomerType, iRows, 0)) = True Then
                        bStatus = True
                        sCustomercode = fnGetdgvText(grdCustomerType, iRows, 1).Trim
                        sCustomername = fnGetdgvText(grdCustomerType, iRows, 2).Trim
                        sTotalPoints = fnGetdgvText(grdCustomerType, iRows, 3).Trim
                        sOldCustomerType = Trim(cboCustomerType.Text)
                        sNewCustomerType = Trim(cboNewCustomerType.Text)
                        dFPoints = Val(cboPoints.Text)
                        dTPoints = Val(Microsoft.VisualBasic.Right(cboPoints.Text, 1))
                        objCust.CustomerTypeChange(Trim(sCustomercode), sCustomername.Trim, sTotalPoints.Trim, sOldCustomerType, sNewCustomerType, Val(dFPoints), Val(dTPoints), sUserID.Trim)

                    End If
                Next
                If bStatus = False Then
                    MessageBox.Show("Select Type Should Not Be Blank.")
                    Exit Sub
                End If
            End If
            MessageBox.Show("Customer detail saved successfully.")
            cboCustomerType.SelectedIndex = -1
            cboNewCustomerType.SelectedIndex = -1
            ClearMe()
            cboCustomerType.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub fncClearGrid()
        grdCustomerType.RowCount = 0
    End Sub

    Private Sub cmdFetch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFetch.Click
        Dim sQry As String
        Try
            If validateMe() Then
                SubLoadSalesDetail()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function validateMe() As Boolean
        Dim bStatus As Boolean
        Try
            If cboCustomerType.Text.Trim = "" Then
                MessageBox.Show("Customer Type Should Not Be Blank.")
                Exit Function
            End If

            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            bStatus = False
        Finally
            validateMe = bStatus
        End Try
    End Function

    Private Sub SubLoadSalesDetail()
        Try
            Dim dsCustomers As New DataSet
            Dim iCnt As Long
            Dim sCondition As String
            Dim sQry As String
            'Dim InvoiceNo As Str
            grdCustomerType.RowCount = 0
            'cCustomerType = (fnGetdgvText(cboCustomerType, grdCustomerType.CurrentRow.Index == 1))

            If cboCustomerType.Text <> "" Then
                dsCustomers = objCust.GetCustomerWithCustomerType(Trim(cboCustomerType.Text), Trim(txtCustomerCode.Text), Val(txtFromPoints.Text), Val(txtToPoints.Text))
                If Not IsNothing(dsCustomers) Then
                    If dsCustomers.Tables("Customer").Rows.Count > 0 Then

                        For iCnt = 0 To dsCustomers.Tables("Customer").Rows.Count - 1
                            grdCustomerType.Rows.Add()
                            fnSetdgvText(grdCustomerType, iCnt, 0, False)
                            fnSetdgvText(grdCustomerType, iCnt, 1, Trim(dsCustomers.Tables("Customer").Rows(iCnt)("CustomerCode") & ""))
                            fnSetdgvText(grdCustomerType, iCnt, 2, Trim(dsCustomers.Tables("Customer").Rows(iCnt)("CustomerName") & ""))
                            fnSetdgvText(grdCustomerType, iCnt, 3, Trim(dsCustomers.Tables("Customer").Rows(iCnt)("TotalRewardPoints") & ""))
                            'fnSetdgvText(grdCustomerType, iCnt, 3, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("PCS") & ""))
                            'fnSetdgvText(grdCustomerType, iCnt, 4, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("NetAmount") & ""))

                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'Private Function fnGetdgvText(ByVal p1 As Integer, ByVal p2 As Integer) As String
    '    Throw New NotImplementedException
    'End Function

    Private Sub grdCustomerType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCustomerType.Click
        If Trim(fnGetdgvText(grdCustomerType, grdCustomerType.CurrentRow.Index, 0)) = False Then
            Call fnSetdgvText(grdCustomerType, grdCustomerType.CurrentRow.Index, 0, True)
        Else
            Call fnSetdgvText(grdCustomerType, grdCustomerType.CurrentRow.Index, 0, False)
        End If
    End Sub

    Private Sub frmCustomerTypeChange_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F6
                btnSave_Click(sender, e)
            Case Keys.F7
                btnClear_Click(sender, e)
            Case Keys.Escape
                btnClose_Click(sender, e)
            Case Keys.Enter
                SendKeys.Send("(Tab)")
            Case Keys.Up
                SendKeys.Send("+{Tab}")
            Case Keys.Down
                SendKeys.Send("{Tab}")

        End Select
    End Sub

    Private Sub cboNewCustomerType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNewCustomerType.SelectedIndexChanged
        Try
            If cboCustomerType.Text.Trim = "RUBY" Then
                If cboNewCustomerType.Text = "EMERALD" Then
                    cboPoints.Text = "5/7"
                    'cboPoints.SelectedIndex = 0
                ElseIf cboNewCustomerType.Text = "DIAMOND" Then
                    cboPoints.Text = "5/9"
                    'cboPoints.SelectedIndex = 1
                End If
            ElseIf cboCustomerType.Text.Trim = "EMERALD" Then
                If cboNewCustomerType.Text = "RUBY" Then
                    cboPoints.Text = "7/5"
                ElseIf cboNewCustomerType.Text = "DIAMOND" Then
                    cboPoints.Text = "7/9"
                End If
            ElseIf cboCustomerType.Text.Trim = "DIAMOND" Then
                If cboNewCustomerType.Text = "RUBY" Then
                    cboPoints.Text = "9/5"
                ElseIf cboNewCustomerType.Text = "EMERALD" Then
                    cboPoints.Text = "9/7"
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    
    Private Sub btnCustomerCodeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomerCodeSearch.Click
        If Trim(sCompanyShortCode) = "RJ" Then
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCustomer", "R", 8, "CustomerName", "", "CustomerCode", "CustomerCode", 80, "CustomerName", "CustomerName", 180)
            frmHelpClass.ShowDialog()
            txtCustomerCode.Text = frmHelpClass.Var1.Trim
            txtCustomerCode_LostFocus(sender, e)
            ' SendKeys.Send(("{TAB}"))
        Else
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCustomer", "R", 4, "CustomerName", "Customertype =" & fncFormatSQLText(cboCustomerType.Text.Trim), "CustomerCode", "CustomerCode", 80, "CustomerName", "CustomerName", 180, "Address1", "Address1", 180, "Address2", "Address2", 180)
            frmHelpClass.ShowDialog()
            txtCustomerCode.Text = frmHelpClass.Var1.Trim
            txtCustomerCode_LostFocus(sender, e)
            'SendKeys.Send(("{TAB}"))
        End If
    End Sub

    Private Sub txtCustomerCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCustomerCode.KeyDown
        If e.KeyCode = Keys.F1 Then
            If Trim(sCompanyShortCode) = "RJ" Then
                Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCustomer", "L", 8, "CustomerName", "", "CustomerCode", "CustomerCode", 80, "CustomerName", "CustomerName", 180)
                frmHelpClass.ShowDialog()
                txtCustomerCode.Text = frmHelpClass.Var1.Trim
            Else
                Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCustomer", "L", 4, "CustomerName", "", "CustomerCode", "CustomerCode", 80, "CustomerName", "CustomerName", 180)
                frmHelpClass.ShowDialog()
                txtCustomerCode.Text = frmHelpClass.Var1.Trim
                'grdSaleDetail.Show()
            End If
        End If
    End Sub

    Private Sub txtCustomerCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustomerCode.LostFocus
        If txtCustomerCode.Text.Trim <> "" Then
            txtCustomerCode.Enabled = True
            LoadCustomerDetails(txtCustomerCode.Text.Trim)
        End If
    End Sub
    Public Sub LoadCustomerDetails(ByVal sCustCode As String)
        Try
            Dim dsCustomer As New DataSet
            Dim objCustomer As New clsCustomer
            Dim iCnt As Long
            dsCustomer = objCustomer.GetCustomerwithTypeHQ(sCustCode, Trim(cboCustomerType.Text))
            If Not IsNothing(dsCustomer) Then
                If dsCustomer.Tables("Customer").Rows.Count > 0 Then
                    txtCustomerCode.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("CustomerCode") & "")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
End Class
