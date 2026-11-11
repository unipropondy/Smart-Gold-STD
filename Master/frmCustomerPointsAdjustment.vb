Public Class frmCustomerPointsAdjustment
    Inherits System.Windows.Forms.Form
    Dim objCustomer As New clsCustomer
    Const CustomerCode = 1
    Const CustomerName = 2
    Const Phone = 3
    Const Fax = 4


    Dim dm As New DataManager
    Friend WithEvents grdSalesDetail As System.Windows.Forms.DataGridView
    Friend WithEvents grdCustomerPoints As System.Windows.Forms.DataGridView
    Friend WithEvents grdCustomersAdjustment As New System.Windows.Forms.DataGridView

    Private Sub btnCustomerCodeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomerCodeSearch.Click
        If Trim(sCompanyShortCode) = "RJ" Then
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCustomer", "R", 8, "CustomerName", "", "CustomerCode", "CustomerCode", 80, "CustomerName", "CustomerName", 180)
            frmHelpClass.ShowDialog()
            txtCustomerCode.Text = frmHelpClass.Var1.Trim
            txtCustomerCode_LostFocus(sender, e)
            ' SendKeys.Send(("{TAB}"))
        Else
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCustomer", "R", 4, "CustomerName", "", "CustomerCode", "CustomerCode", 80, "CustomerName", "CustomerName", 180, "Address1", "Address1", 180, "Address2", "Address2", 180)
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
    Private Sub frmCustomer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub txtCustomerCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustomerCode.LostFocus
        If txtCustomerCode.Text.Trim <> "" Then
            txtCustomerCode.Enabled = True
            LoadCustomerDetails(txtCustomerCode.Text.Trim)
            SubLoadSales()
        End If
    End Sub
    Private Sub clearMe()
        txtCustomerCode.Text = ""
        txtCustomerCode.Focus()
        'cboCustomerType.Items.Clear()
        txtCustomerType.Clear()
        txtCustomerName.Clear()
        txtPreviousPoints.Text = ""
        txtAdjustmentPoints.Text = ""
        'fnGridClearText()

    End Sub
    Private Sub fncClearGrid()
        grdCustomersAdjustment.RowCount = 0
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        clearMe()
        txtCustomerCode.Focus()
        txtInvoiceNo.Clear()
        txtTotalValue.Clear()
        fncClearGrid()

    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sQry As String

        'Dim SaveStatus As Boolean
        Try
            If ValidateMe() Then

                If objCustomer.CustomerPointAdjustmentSave(txtCustomerCode.Text.Trim, txtCustomerType.Text.Trim, txtCustomerName.Text.Trim, txtPreviousPoints.Text.Trim, txtAdjustmentPoints.Text.Trim, txtInvoiceNo.Text.Trim) Then


                    'SaveStatus = dm.ValReturnProjExecNonQryAcc(sQry)


                    'dm.ValReturnProjExecNonQryAcc(sQry)
                    'dm.ProjExecNonQryAcc(sQry)
                    MessageBox.Show("Customer detail saved successfully.")
                    clearMe()
                    txtInvoiceNo.Clear()
                    txtTotalValue.Clear()
                    fncClearGrid()
                    txtCustomerCode.Focus()
                    btnSave.Enabled = True
                End If


                ''If objCustomer.CustomerSave(txtCustomerCode.Text.Trim, txtCustomerType.Text.Trim, txtCustomerName.Text.Trim, txtPreviousPoints.Text.Trim, txtAdjustmentPoints.Text.Trim) Then


                ''    'SaveStatus = dm.ValReturnProjExecNonQryAcc(sQry)


                ''    'dm.ValReturnProjExecNonQryAcc(sQry)
                ''    'dm.ProjExecNonQryAcc(sQry)
                ''    MessageBox.Show("Customer detail saved sucssfully.")
                ''    clearMe()
                ''    txtInvoiceNo.Clear()
                ''    txtTotalValue.Clear()
                ''    fncClearGrid()
                ''    txtCustomerCode.Focus()
                ''    btnSave.Enabled = True

            End If
            ''If objCustomer.CustomerSave(txtCustomerCode.Text.Trim, txtCustomerName.Text.Trim) Then 
            ''MessageBox.Show("Customer detail saved sucessfully")
            ''clearMe()
            ''txtCustomerCode.Focus()
            '' Else
            ''    MessageBox.Show("Error in Save")

            ''End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            ' 'SaveStatus = False

        End Try

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
                    txtCustomerType.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("CustomerType") & "")
                    txtPreviousPoints.Text = Val(dsCustomer.Tables("Customer").Rows(0)("TotalRewardPoints") & "")
                    txtInvoiceNo.Focus()
                    SubLoadSales()
                    '' SubLoadCustomerPoints()
                    ''txtAdjustmentPoints.Focus()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Function ValidateMe() As Boolean
        Dim bStatus As Boolean

        Try
            If txtInvoiceNo.Text.Trim = "" Then
                MessageBox.Show("Invoice No should not be blank.")
                txtAdjustmentPoints.Focus()
                Exit Function
            End If
            If txtTotalValue.Text.Trim = "" Then
                MessageBox.Show("Total Value should not be blank")
                txtAdjustmentPoints.Focus()
                Exit Function
            End If
            If txtAdjustmentPoints.Text.Trim = "" Then
                MessageBox.Show("Adjustment Points should not be blank.")
                txtAdjustmentPoints.Focus()
                Exit Function
            End If
            If objCustomer.fncCheckInvoiceNo(txtInvoiceNo.Text.Trim) Then
                ' MsgBox("Invoice No Already Exists!", vbCritical)
                MsgBox("Invoice Number Already Entered For another customer. Please Check.")
                txtInvoiceNo.Focus()
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

    Private Sub SubLoadSalesDetail()
        Try
            Dim dsCustomer As New DataSet
            Dim iCnt As Long
            Dim InvoiceNo As String
            grdCustomersAdjustment.RowCount = 0
            InvoiceNo = Trim(fnGetdgvText(grdSalesDetail, grdCustomersAdjustment.CurrentRow.Index, 0))
            If InvoiceNo.Trim <> "" Then
                dsCustomer = objCustomer.GetSalesDetailByInvoiceNo(InvoiceNo.Trim)
                If Not IsNothing(dsCustomer) Then
                    If dsCustomer.Tables("Customer").Rows.Count > 0 Then

                        For iCnt = 0 To dsCustomer.Tables("Customer").Rows.Count - 1
                            grdSalesDetail.Rows.Add()
                            fnSetdgvText(grdSalesDetail, iCnt, 0, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("InventoryCode") & ""))
                            fnSetdgvText(grdSalesDetail, iCnt, 1, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("Description") & ""))
                            fnSetdgvText(grdSalesDetail, iCnt, 2, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("Weight") & ""))
                            fnSetdgvText(grdSalesDetail, iCnt, 3, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("PCS") & ""))
                            fnSetdgvText(grdSalesDetail, iCnt, 4, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("NetAmount") & ""))

                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub GrdSalesSummary_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_ClickEvent)
        SubLoadSales()
    End Sub

    Private Sub SubLoadSales()
        Try
            Dim dsCustomer As New DataSet
            Dim iCnt As Long
            grdCustomersAdjustment.RowCount = 0
            '  grdCustomersAdjustment.RowCount = 0
            dsCustomer = objCustomer.GetSalesByCustomer(txtCustomerCode.Text.Trim)
            If Not IsNothing(dsCustomer) Then
                If dsCustomer.Tables("Customer").Rows.Count > 0 Then
                    For iCnt = 0 To dsCustomer.Tables("Customer").Rows.Count - 1
                        grdCustomersAdjustment.Rows.Add()
                        fnSetdgvText(grdCustomersAdjustment, iCnt, 0, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("InvoiceNo") & ""))
                        fnSetdgvText(grdCustomersAdjustment, iCnt, 1, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("InvoiceDate") & ""))
                        fnSetdgvText(grdCustomersAdjustment, iCnt, 2, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("TotalWeight") & ""))
                        fnSetdgvText(grdCustomersAdjustment, iCnt, 3, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("TotalPCS") & ""))
                        fnSetdgvText(grdCustomersAdjustment, iCnt, 4, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("NetTotal") & ""))

                    Next
                    'SubLoadSalesDetail()
                End If
                'btnDelete.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub SubLoadCustomerPoints()
        Try
            Dim dsCustomer As New DataSet
            Dim iCnt As Long
            grdCustomerPoints.RowCount = 0
            dsCustomer = objCustomer.GetRewardPointsByCustomer(txtCustomerCode.Text.Trim)
            If Not IsNothing(dsCustomer) Then
                If dsCustomer.Tables("Customer").Rows.Count > 0 Then
                    For iCnt = 0 To dsCustomer.Tables("Customer").Rows.Count - 1
                        grdCustomerPoints.Rows.Add()
                        fnSetdgvText(grdCustomerPoints, iCnt, 0, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("InvoiceNo") & ""))
                        fnSetdgvText(grdCustomerPoints, iCnt, 1, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("InvoiceDate") & ""))
                        fnSetdgvText(grdCustomerPoints, iCnt, 2, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("InvoiceAmount") & ""))
                        fnSetdgvText(grdCustomerPoints, iCnt, 3, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("RewardPoints") & ""))
                        fnSetdgvText(grdCustomerPoints, iCnt, 4, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("RedeemPoints") & ""))

                    Next
                End If
                btnDelete.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function fncGetCustomerTran(ByVal sCode As String) As Boolean
        Dim DvCustomer() As DataRow
        Dim DM As New DataManager
        fncGetCustomerTran = False
        DvCustomer = DM.RowReturnProjExecNonQryWithDataRow("Select * from tblSalesHeader where CustomerCode=" & fncFormatSQLText(sCode))
        If DvCustomer.Length > 0 Then
            fncGetCustomerTran = True
        Else
            fncGetCustomerTran = False
        End If
    End Function

    Private Sub frmCustomerPointsAdjustment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'subGridSetting(grdsaleSummary)
        'subGridSetting(grdSalesDetail)
    End Sub

    Private Sub grdCustomersAdjustment_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCustomersAdjustment.CellContentClick
        'txtInvoiceNo.Text = Trim(fnGetdgvText(grdCustomersAdjustment, grdCustomersAdjustment.CurrentRow.Index, 0))
        'txtTotalValue.Text = Trim(fnGetdgvText(grdCustomersAdjustment, grdCustomersAdjustment.CurrentRow.Index, 4))
        'txtAdjustmentPoints.Focus()
    End Sub
    
    Private Sub grdCustomersAdjustment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCustomersAdjustment.Click
        txtInvoiceNo.Text = Trim(fnGetdgvText(grdCustomersAdjustment, grdCustomersAdjustment.CurrentRow.Index, 0))
        txtTotalValue.Text = Trim(fnGetdgvText(grdCustomersAdjustment, grdCustomersAdjustment.CurrentRow.Index, 4))
        txtAdjustmentPoints.Focus()
    End Sub

    Private Sub txtCustomerCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCustomerCode.TextChanged

    End Sub
End Class

