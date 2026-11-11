Imports System.Text.RegularExpressions
Public Class frmOutletCustomers
    Dim objCustomer As New clsCustomer
    Private Sub frmOutletCustomers_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F6
                btnNew_Click(sender, e)
            Case Keys.F7
                btnSave_Click(sender, e)
            Case Keys.Escape
                btnClose_Click(sender, e)
            Case Keys.Enter
                SendKeys.Send("{Tab}")
            Case Keys.Up
                SendKeys.Send("+{Tab}")
            Case Keys.Down
                SendKeys.Send("{Tab}")
        End Select
    End Sub



    Private Sub frmOutletCustomers_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ClearMe()
        addCboReligion()
        cboCustomerType.SelectedIndex = 0
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        'Dim objfrm As New frmCustomerMnt
        'objfrm.sCustomercode = ""
        'objfrm.ShowDialog()
        ClearMe()
        txtCustomerCode.Focus()
    End Sub
    Private Sub ClearMe()
        'txtCustomerCode.Text = ""
        'txtCustomerName.Clear()
        txtMobile.Clear()
        txtDD.Clear()
        txtMM.Clear()
        txtYYYY.Clear()
        txtNRICNo.Clear()
        txtRemarks.Clear()
        cboCustomerType.Text = ""
        lblTotalPoints.Text = 0
        lblRedeemedPoints.Text = 0
        lblBalancePoints.Text = 0
        txtEmail.Text = ""
        txtCustomerCode.Enabled = True
        If txtCustomerCode.Enabled = True Then txtCustomerCode.Focus()
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        'Dim objfrm As New frmCustomerMnt
        'objfrm.sCustomercode = Trim(GetText(grdCustomer, grdCustomer.ActiveRow, 1))
        'objfrm.ShowDialog()
        Dim AccType As String
        Dim DOB As String
        Dim sGender As String
        Dim DOA As String
        Try
            If ValidateMe() Then
                'If optCash.Checked = True Then
                '    AccType = "C"
                'Else
                '    AccType = "G"
                'End If
                'DOB = txtDD.Text.Trim & "/" & txtMM.Text.Trim & "/" & txtYYYY.Text.Trim
                DOB = ""
                DOA = ""
                ''HAJ Add gender Colum on 28 Jan 2008
                If optMale.Checked Then
                    sGender = "M"
                Else
                    sGender = "F"
                End If

                If objCustomer.CustomerSave(txtCustomerCode.Text.Trim, txtCustomerName.Text.Trim, "", "", "", "", _
                  "", "", txtMobile.Text.Trim, "", txtEmail.Text.Trim, "", "", AccType, "", _
                  "", txtRemarks.Text.Trim, txtNRICNo.Text.Trim, "", DOB, cboCustomerType.Text.Trim, cboDateType.Text.Trim, sGender, DOA) Then
                    MessageBox.Show("Customer detail saved sucssfully.")

                    If IsCalledFrom = "POS" Then
                        Dim objPOS As New frmGPOS
                        sOutCustomerCode = txtCustomerCode.Text
                        sOutCustomerName = txtCustomerName.Text
                        Me.Close()
                    End If
                    ClearMe()
                    txtCustomerCode.Focus()
                    'GetCustomerDetails()
                    'LoadCustomerDetails(Trim(GetText(grdCustomer, grdCustomer.ActiveRow, 1)))
                Else
                    MessageBox.Show("Error in Save")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Function EmailAddressCheck(ByVal emailAddress As String) As Boolean

        Dim pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        Dim emailAddressMatch As Match = Regex.Match(emailAddress, pattern)
        If emailAddressMatch.Success Then
            EmailAddressCheck = True
        Else
            EmailAddressCheck = False
        End If
    End Function
    Private Function ValidateMe() As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            If txtCustomerCode.Text.Trim = "" Then
                MessageBox.Show("CustomerCode should not be blank.")
                txtCustomerCode.Focus()
                Exit Function
            End If
            If txtCustomerName.Text.Trim = "" Then
                MessageBox.Show("CustomerName should not be blank.")
                txtCustomerName.Focus()
                Exit Function
            End If
            'If txtNricNo.Text.Trim = "" Then
            '    MessageBox.Show("Nric Number should not be blank.")
            '    txtNricNo.Focus()
            '    Exit Function
            'End If
            If txtMobile.Text.Trim = "" Then
                MessageBox.Show("Mobile Number should not be blank.")
                txtMobile.Focus()
                Exit Function
            End If
            If Trim(txtEmail.Text) <> "" Then
                If EmailAddressCheck(txtEmail.Text) = False Then
                    MessageBox.Show("Enter Proper Email Address")
                    txtEmail.Focus()
                    Exit Function
                End If
            End If
            'If IsDate(txtDD.Text.Trim & "/" & txtMM.Text.Trim & "/" & txtYYYY.Text.Trim) = False Then
            '    MessageBox.Show("Enter valid Date of Birth")
            '    txtDD.Focus()
            '    Exit Function
            'End If
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            bStatus = False
        Finally
            ValidateMe = bStatus
        End Try

    End Function

    Private Sub btnCustomerCodeSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnCustomerCodeSearch.Click
        Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCustomer", "R", 4, "CustomerName", "", "CustomerCode", "CustomerCode", 80, "CustomerName", "CustomerName", 180, "Address1", "Address1", 60, "Address2", "Address2", 80)
        frmHelpClass.ShowDialog()
        txtCustomerCode.Text = frmHelpClass.Var1.Trim
        txtCustomerCode_LostFocus(sender, e)
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
            Dim iCnt As Long
            dsCustomer = objCustomer.GetCustomerwithCodeHQ(sCustCode)
            If Not IsNothing(dsCustomer) Then
                If dsCustomer.Tables("Customer").Rows.Count > 0 Then
                    txtCustomerCode.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("CustomerCode") & "")
                    txtCustomerName.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("CustomerName") & "")
                    cboCustomerType.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("CustomerType") & "")
                    txtMobile.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("Mobile") & "")
                    txtNRICNo.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("NricNo") & "")
                    txtDD.Text = Mid(Trim(dsCustomer.Tables("Customer").Rows(0)("DOB") & ""), 1, 2)
                    txtMM.Text = Mid(Trim(dsCustomer.Tables("Customer").Rows(0)("DOB") & ""), 4, 2)
                    txtYYYY.Text = Mid(Trim(dsCustomer.Tables("Customer").Rows(0)("DOB") & ""), 7, 4)
                    txtRemarks.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("Remarks") & "")
                    cboDateType.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("DateType") & "")
                    txtEmail.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("Email") & "")
                    If UCase(Trim(dsCustomer.Tables("Customer").Rows(0)("Gender") & "")) = "M" Then
                        optMale.Checked = True
                    Else
                        optFemale.Checked = True
                    End If
                    lblTotalPoints.Text = Val(dsCustomer.Tables("Customer").Rows(0)("TotalRewardPoints") & "")
                    lblRedeemedPoints.Text = Val(dsCustomer.Tables("Customer").Rows(0)("TotalRedeemedPoints") & "")
                    lblBalancePoints.Text = Val(dsCustomer.Tables("Customer").Rows(0)("TotalRewardPoints") & "") - Val(dsCustomer.Tables("Customer").Rows(0)("TotalRedeemedPoints") & "")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        ClearMe()
    End Sub
    Private Sub addCboReligion()
        cboDateType.Items.Add("")
        cboDateType.Items.Add("HINDU")
        cboDateType.Items.Add("MUSLIM")
        cboDateType.Items.Add("CHRISTIAN")
        cboDateType.Items.Add("CHINESE")
        cboDateType.SelectedIndex = 0
    End Sub

    Private Sub txtNRICNo_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtNRICNo.TextChanged

    End Sub

    Private Sub Label1_Click(sender As System.Object, e As System.EventArgs) Handles Label1.Click

    End Sub
End Class