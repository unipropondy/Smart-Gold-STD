Public Class frmCustomerDeposit
    Dim objCustomer As New clsCustomer
    Dim objCls As New ClsClass
    Dim sSalesReturnNo As String
    Dim dDepositAmount As Double
    Private Sub txtCustomerName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtCustomerName.TextChanged

    End Sub
    Private Sub lblLocationName_Click(sender As System.Object, e As System.EventArgs) Handles lblLocationName.Click

    End Sub

    Private Sub btnNew_Click(sender As System.Object, e As System.EventArgs) Handles btnNew.Click
        ClearAll()
    End Sub
    Private Sub ClearAll()
        txtCustomerCode.Text = ""
        txtCustomerName.Text = ""
        cboType.Text = ""
        txtAmount.Text = ""
        txtRemarks.Text = ""
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnCustomerCodeSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnCustomerCodeSearch.Click
        Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCustomer", "R", 4, "CustomerName", "", "CustomerCode", "CustomerCode", 80, "CustomerName", "CustomerName", 180, "Address1", "Address1", 180, "Address2", "Address2", 180)
            frmHelpClass.ShowDialog()
            txtCustomerCode.Text = frmHelpClass.Var1.Trim
            txtCustomerCode_LostFocus(sender, e)
            'SendKeys.Send(("{TAB}"))
    End Sub

    Private Sub txtCustomerCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtCustomerCode.KeyDown
        If e.KeyCode = Keys.F1 Then
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCustomer", "L", 4, "CustomerName", "", "CustomerCode", "CustomerCode", 80, "CustomerName", "CustomerName", 180, "Address1", "Address1", 180, "Address2", "Address2", 180)
                frmHelpClass.ShowDialog()
                txtCustomerCode.Text = frmHelpClass.Var1.Trim
        End If
    End Sub

    Private Sub txtCustomerCode_LostFocus(sender As Object, e As System.EventArgs) Handles txtCustomerCode.LostFocus
        If txtCustomerCode.Text.Trim <> "" Then
            txtCustomerCode.Enabled = False
            LoadCustomerDetails(txtCustomerCode.Text.Trim)
        End If
    End Sub
    Public Sub LoadCustomerDetails(ByVal sCustCode As String)
        Try
            Dim dsCustomer As New DataSet
            dsCustomer = objCustomer.GetCustomerwithCodeHQ(sCustCode)
            If Not IsNothing(dsCustomer) Then
                If dsCustomer.Tables("Customer").Rows.Count > 0 Then
                    txtCustomerCode.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("CustomerCode") & "")
                    txtCustomerName.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("CustomerName") & "")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtCustomerCode_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtCustomerCode.TextChanged

    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        Dim sDepositType As String
        Dim dsG As New DataSet
        Try
            If txtCustomerCode.Text = "" Then
                MsgBox("Please Select the Customer Code", MsgBoxStyle.Information)
                txtCustomerCode.Focus()
                Exit Sub
            ElseIf Val(txtAmount.Text) = 0 Then
                MsgBox("Deposti Amount Cannot be Zero", MsgBoxStyle.Information)
                txtAmount.Text = ""
                txtAmount.Focus()
                Exit Sub
            ElseIf txtRemarks.Text = "" Then
                MsgBox("Please Enter the Remarks", MsgBoxStyle.Information)
                txtRemarks.Focus()
                Exit Sub
            End If
            If IsNumeric(txtAmount.Text) Then

            Else
                MsgBox("Amount can be Numbers only ", MsgBoxStyle.Information)
                txtAmount.Text = ""
                txtAmount.Focus()
                Exit Sub
            End If
            If cboType.Text = "Flexi" Then
                sDepositType = "FLEXI"
            Else
                sDepositType = "TRADE-IN"
            End If

            If objCls.DepositSave(txtCustomerCode.Text.Trim, txtCustomerName.Text.Trim, sDepositType, txtAmount.Text, txtRemarks.Text.Trim) Then
                MessageBox.Show("Customer deposit saved sucssfully.")
                btnNew_Click(Nothing, Nothing)
            Else
                MessageBox.Show("Error in Save")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmCustomerDeposit_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
                    btnNew_Click(Nothing, Nothing)
                Case Keys.F7
                    btnSave_Click(Nothing, Nothing)
            End Select
        Catch ex As Exception
        End Try
    End Sub


    Private Sub SubLoadDeposit()
        Try
            Dim dsDeposit As New DataSet
            Dim objSalesReturn As New ClsSalesReturn
            Dim sDate As String
            Dim iCnt As Long
            sDate = Format(ServerDate, "dd/MM/yyyy")
            dsDeposit = objSalesReturn.GetDepositDetails(txtCustomerCode.Text)
            If Not IsNothing(dsDeposit) Then
                If dsDeposit.Tables("Deposit").Rows.Count > 0 Then
                    sSalesReturnNo = Trim(dsDeposit.Tables("Deposit").Rows(iCnt)("SalesReturnNo") & "")
                    dDepositAmount = Val(dsDeposit.Tables("Deposit").Rows(iCnt)("Amount") & "")
                    sDepositNo = Trim(dsDeposit.Tables("Deposit").Rows(iCnt)("SalesReturnNo") & "")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class