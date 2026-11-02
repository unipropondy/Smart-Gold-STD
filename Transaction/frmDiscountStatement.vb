Public Class frmDiscountStatement

    Public Sub txtBillNo_LostFocus(sender As Object, e As System.EventArgs) Handles txtBillNo.LostFocus
        Dim objSalesRet As New ClsSalesReturn
        Dim Ds As DataSet
        Try
            If txtBillNo.Text.Trim <> "" Then

                subGetSalesDetail()
                txtDocId.Focus()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Public Sub subGetSalesDetail()

        Dim i As Long
        Dim j As Long
        Dim ODs As DataSet
        Dim tempInvCode As String
        Dim objSales As New ClsSalesReturn
        Dim objCustomer As New clsCustomer
        Dim Ds As DataSet
        Dim DsHdr As DataSet
        Dim DsCust As DataSet
        Dim DsMember As DataSet


        Try

            DsHdr = objSales.GetSalesHeaderByInvoiceNo(txtBillNo.Text.Trim)
            If DsHdr.Tables("Invoice").Rows.Count > 0 Then


                'txtCustomerCode.Text = Trim(DsHdr.Tables("Invoice").Rows(0)("CustomerCode") & "")
                'txtMemberCode.Text = Trim(DsHdr.Tables("Invoice").Rows(0)("MemberCode") & "")
                ''HAJ change for Date format, on 23 Oct 2007
                'txtBDD.Text = Mid(DsHdr.Tables("Invoice").Rows(0)("BDate"), 7, 2)
                'txtBMM.Text = Mid(DsHdr.Tables("Invoice").Rows(0)("BDate"), 5, 2)
                'txtBYYYY.Text = Mid(DsHdr.Tables("Invoice").Rows(0)("BDate"), 1, 4)
                txtBDD.Text = Mid(DsHdr.Tables("Invoice").Rows(0)("BDate"), 1, 2)
                txtBMM.Text = Mid(DsHdr.Tables("Invoice").Rows(0)("BDate"), 4, 2)
                txtBYYYY.Text = Mid(DsHdr.Tables("Invoice").Rows(0)("BDate"), 7, 4)
                'If Trim(DsHdr.Tables("Invoice").Rows(0)("SalesType") & "") = "INSTALLMENT" Then
                '    txtNetAmount.Text = Val(DsHdr.Tables("Invoice").Rows(0)("NetTotal")) - Val(DsHdr.Tables("Invoice").Rows(0)("TRadeInAMt"))
                'Else
                txtNetAmount.Text = Val(DsHdr.Tables("Invoice").Rows(0)("NetTotal"))
                'End If
                txtGST.Text = Format(Val(DsHdr.Tables("Invoice").Rows(0)("NetTotal")) * 0.0582, "0.00")
                txtPassportNo.Text = Trim(DsHdr.Tables("Invoice").Rows(0)("Remarks") & "")
                'txtSalesMan.Text = Trim(DsHdr.Tables("Invoice").Rows(0)("SalesmanCode") & "")
                dtDate.Text = Trim(DsHdr.Tables("Invoice").Rows(0)("BDate") & "")

                DsCust = objSales.GetSalesHeaderByPassportCount(txtPassportNo.Text.Trim)
                If DsCust.Tables("Invoice").Rows.Count > 0 Then
                    txtRemarks.Text = Trim(DsCust.Tables("Invoice").Rows(0)("Count") & "")
                Else
                    txtRemarks.Text = 0
                End If



            End If



        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub btnBillClear_Click(sender As System.Object, e As System.EventArgs) Handles btnBillClear.Click
        txtBillNo.Text = ""
        txtBDD.Text = ""
        txtBMM.Text = ""
        txtBYYYY.Text = ""
        txtSalesMan.Text = ""
        cboSalesman.Text = ""
        txtPassportNo.Text = ""
        txtGST.Text = ""
        txtNetAmount.Text = ""
        txtDocId.Text = ""
        txtRemarks.Text = ""
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        Dim AccType As String
        Dim BillDate As String
        Dim sGender As String
        Dim DOA As String
        Dim objSales As New ClsSales

        Try
            If ValidateMe() Then

                BillDate = txtBDD.Text.Trim & "/" & txtBMM.Text.Trim & "/" & txtBYYYY.Text.Trim


                'If objSales.eTRSStatementSave(txtBillNo.Text.Trim, BillDate, cboSalesman.Text.Trim, txtPassportNo.Text.Trim, Val(txtGST.Text), Val(txtNetAmount.Text), _
                '  txtDocId.Text.Trim, dtDate.Text, dtTime.Text, txtRemarks.Text.Trim) Then
                '    MessageBox.Show("eTRS Statement saved sucssfully.")
                '    btnBillClear_Click(sender, e)
                '    txtBillNo.Focus()
                'Else
                '    MessageBox.Show("Error in Save")
                'End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function ValidateMe() As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            If txtBillNo.Text.Trim = "" Then
                MessageBox.Show("BillNo should not be blank.")
                txtBillNo.Focus()
                Exit Function
            End If
            If txtPassportNo.Text.Trim = "" Then
                MessageBox.Show("PassportNo should not be blank.")
                txtPassportNo.Focus()
                Exit Function
            End If
            If txtDocId.Text.Trim = "" Then
                MessageBox.Show("DocId should not be blank.")
                txtDocId.Focus()
                Exit Function
            End If
            'If Len(txtDocId.Text.Trim) <> 19 Then
            '    MessageBox.Show("DocId should be 19 Characters")
            '    txtDocId.Focus()
            '    Exit Function
            'End If
            If cboSalesman.Text.Trim = "" Then
                MessageBox.Show("Salesman should not be blank.")
                cboSalesman.Focus()
                Exit Function
            End If
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            bStatus = False
        Finally
            ValidateMe = bStatus
        End Try

    End Function

    Private Sub frmeTRSStatement_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F6
                btnBillClear_Click(sender, e)
            Case Keys.F7
                btnSave_Click(sender, e)
            Case Keys.Escape
                btnClose_Click(sender, e)
            Case Keys.Up
                SendKeys.Send("+{Tab}")
            Case Keys.Down
                SendKeys.Send("{Tab}")
            Case Keys.Enter
                SendKeys.Send("{Tab}")
        End Select
    End Sub

    Private Sub frmeTRSStatement_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Trim(gthisLocation) = "99" Then
            cboSalesman.Items.Add("GANESH")
            cboSalesman.Items.Add("ARAVIND")
            cboSalesman.Items.Add("ARUN")
        ElseIf Trim(gthisLocation) = "85" Then
            cboSalesman.Items.Add("M.SARAVANAN")
            cboSalesman.Items.Add("SIMON")
            cboSalesman.Items.Add("A.CHELLADURAI")
            cboSalesman.Items.Add("SAMPATHKUMAR")
        ElseIf Trim(gthisLocation) = "69" Then
            cboSalesman.Items.Add("MUTHIAH")
            cboSalesman.Items.Add("MA.SARAVANAN")
            cboSalesman.Items.Add("JOY")
        End If
    End Sub
End Class