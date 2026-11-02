Imports System
Imports System.IO
Public Class frmeTRSVoidStatement
    Dim objSales As New ClsSalesReturn
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

        Dim objCustomer As New clsCustomer
        Dim Ds As DataSet
        Dim DsHdr As DataSet
        Dim DsCust As DataSet
        Dim DsMember As DataSet
        Dim objSR As New ClsSalesReturn


        Try



            DsCust = objSR.GetCustomerImage(Trim(txtBillNo.Text))
            If Not IsNothing(DsCust) Then
                If DsCust.Tables("Inventory").Rows.Count > 0 Then
                    Byte2Image(picImage.Image, DsCust.Tables("Inventory").Rows(0)("CustomerPhoto"))
                End If
            End If

            DsHdr = objSales.GeteTRSStatementInvoiceNo(txtBillNo.Text.Trim)
            If DsHdr.Tables("Invoice").Rows.Count > 0 Then


                'txtCustomerCode.Text = Trim(DsHdr.Tables("Invoice").Rows(0)("CustomerCode") & "")
                'txtMemberCode.Text = Trim(DsHdr.Tables("Invoice").Rows(0)("MemberCode") & "")
                ''HAJ change for Date format, on 23 Oct 2007
                'txtBDD.Text = Mid(DsHdr.Tables("Invoice").Rows(0)("BDate"), 7, 2)
                'txtBMM.Text = Mid(DsHdr.Tables("Invoice").Rows(0)("BDate"), 5, 2)
                'txtBYYYY.Text = Mid(DsHdr.Tables("Invoice").Rows(0)("BDate"), 1, 4)
                txtBDD.Text = Mid(DsHdr.Tables("Invoice").Rows(0)("BillDate"), 1, 2)
                txtBMM.Text = Mid(DsHdr.Tables("Invoice").Rows(0)("BillDate"), 4, 2)
                txtBYYYY.Text = Mid(DsHdr.Tables("Invoice").Rows(0)("BillDate"), 7, 4)
                'If Trim(DsHdr.Tables("Invoice").Rows(0)("SalesType") & "") = "INSTALLMENT" Then
                '    txtNetAmount.Text = Val(DsHdr.Tables("Invoice").Rows(0)("NetTotal")) - Val(DsHdr.Tables("Invoice").Rows(0)("TRadeInAMt"))
                'Else
                txtNetAmount.Text = Val(DsHdr.Tables("Invoice").Rows(0)("NetAmount"))
                'End If
                txtGST.Text = Val(DsHdr.Tables("Invoice").Rows(0)("Gst"))
                txtPassportNo.Text = Trim(DsHdr.Tables("Invoice").Rows(0)("PassPortNo") & "")
                'txtSalesMan.Text = Trim(DsHdr.Tables("Invoice").Rows(0)("SalesmanCode") & "")
                dtDate.Text = Trim(DsHdr.Tables("Invoice").Rows(0)("eTRSIssueDate") & "")
                dtDate.Text = Trim(DsHdr.Tables("Invoice").Rows(0)("eTRSIssueTime") & "")
                cboSalesman.Text = Trim(DsHdr.Tables("Invoice").Rows(0)("SalesManCode") & "")
                txtDocId.Text = Trim(DsHdr.Tables("Invoice").Rows(0)("DocId") & "")
                txtSealedBag.Text = Trim(DsHdr.Tables("Invoice").Rows(0)("SealedBagNo") & "")
                If Trim(DsHdr.Tables("Invoice").Rows(0)("IsSalesAbove700") & "") = "Y" Then
                    optSales7000Yes.Checked = True
                Else
                    optSales7000No.Checked = True
                End If

                If Trim(DsHdr.Tables("Invoice").Rows(0)("IsSealBag") & "") = "Y" Then
                    optSealedBagYes.Checked = True
                Else
                    optSealedBagNo.Checked = True
                End If



            End If



        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Public Sub Byte2Image(ByRef NewImage As Image, ByVal ByteArr() As Byte)
        Dim ImageStream As System.IO.MemoryStream
        Try
            If ByteArr.GetUpperBound(0) > 0 Then
                ImageStream = New MemoryStream(ByteArr)
                NewImage = System.Drawing.Image.FromStream(ImageStream)
            Else
                NewImage = Nothing
            End If
        Catch ex As Exception
            NewImage = Nothing
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
        chkAbove7000.Checked = False
        chkSealedBag.Checked = False
        txtSealedBag.Text = ""
        txtStaffSign.Text = ""
        picImage.Name = ""
        picImage.Image = Nothing
        optSales7000No.Checked = True
        optSealedBagNo.Checked = True
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub frmeTRSVoidStatement_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F6
                btnBillClear_Click(sender, e)
                'Case Keys.F7
                '    btnSave_Click(sender, e)
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

    Private Sub frmeTRSVoidStatement_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Trim(gthisLocation) = "99" Then
            cboSalesman.Items.Add("GANESH")
            cboSalesman.Items.Add("ARAVIND")
            cboSalesman.Items.Add("ARUN")
        ElseIf Trim(gthisLocation) = "85" Then
            'cboSalesman.Items.Add("M.SARAVANAN")
            'cboSalesman.Items.Add("SIMON")
            'cboSalesman.Items.Add("A.CHELLADURAI")
            'cboSalesman.Items.Add("SAMPATHKUMAR")
            'cboSalesman.Items.Add("MUTHIAH")
            'cboSalesman.Items.Add("MA.SARAVANAN")
            'cboSalesman.Items.Add("JOY")
            cboSalesman.Items.Add("PANG WEI KOON")
            cboSalesman.Items.Add("CHELLAMUTHU GANESAN")
            cboSalesman.Items.Add("RAJAGOPAL MANGALESWARAN")
        ElseIf Trim(gthisLocation) = "69" Then
            cboSalesman.Items.Add("PANG WEI KOON")
            cboSalesman.Items.Add("CHELLAMUTHU GANESAN")
            cboSalesman.Items.Add("RAJAGOPAL MANGALESWARAN")
        End If


        txtBDD.Enabled = False
        txtBMM.Enabled = False
        txtBYYYY.Enabled = False
        txtSalesMan.Enabled = False
        cboSalesman.Enabled = False
        txtPassportNo.Enabled = False
        txtGST.Enabled = False
        txtNetAmount.Enabled = False
        txtDocId.Enabled = False
        txtRemarks.Enabled = True
        chkAbove7000.Enabled = False
        chkSealedBag.Enabled = False
        txtSealedBag.Enabled = False
        txtStaffSign.Enabled = False
        btnBrowseImage.Enabled = False
        picImage.Image = Nothing
        optSales7000No.Enabled = False
        optSealedBagNo.Enabled = False
        optSales7000Yes.Enabled = False
        optSealedBagYes.Enabled = False
        dtDate.Enabled = False
        dtTime.Enabled = False
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click

        If txtRemarks.Text.Trim = "" Then
            MessageBox.Show("Remarks should not be blank.")
            txtRemarks.Focus()
            Exit Sub
        End If
        If Trim(txtBillNo.Text) <> "" Then
            iRet = objSales.eTRSStatementDelete(Trim(txtBillNo.Text), Trim(txtRemarks.Text))
            If iRet = False Then
                MessageBox.Show("eTRS Ticket not exists.")
            Else
                MessageBox.Show("eTRS Ticket Voided Successfully")
                btnBillClear_Click(sender, e)
            End If
        End If
    End Sub
End Class