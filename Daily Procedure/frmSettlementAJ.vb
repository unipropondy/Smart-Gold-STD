Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Web.Mail
Imports System.Web.Mail.SmtpMail
Imports System.Web.Mail.MailAttachment
Imports System.Web.Mail.MailMessage
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mime

Public Class frmSettlementAJ
    Dim sTerminalName As String
    Dim sCashierID As String
    Dim nRowTotal As Integer
    Dim nRowCash As Integer
    Dim nFloatAmount As Decimal
    Dim dm As New DataManager
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Dispose()
    End Sub
    Private Sub frmSettlement_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub
    Private Sub frmSettlement_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                SendKeys.Send("{TAB}")
        End Select
    End Sub
    Private Sub frmSettlement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        dgvSettlement.Visible = False
        dtDate.Text = Date.Now()
        'subMoveTempSales()
    End Sub
    
    Private Sub subGetPassword()
        Dim dsUser As New DataSet
        Dim objUser As New clsUser
        Try
            'dsUser = objUser.fncGetUserByPassword(gsConn, "User", txtUserPassword.Text.Trim)
            dsUser = objUser.fncGetUserByIdAndPassword(gsCon, "User", txtUserID.Text.Trim, txtUserPassword.Text.Trim)
            If dsUser.Tables("User").Rows.Count > 0 Then
                txtUserName.Text = dsUser.Tables("User").Rows(0).Item("UserName") & ""
                sCashierID = dsUser.Tables("User").Rows(0).Item("UserCode") & ""
                subOpenCashDrawer()
                If SubGetTerminal() Then
                    pnlCashier.Visible = False
                    dgvSettlement.Visible = True
                    dgvSettlement.Focus()
                    dgvSettlement.CurrentCell = dgvSettlement(col2.Index, 1)
                Else
                    txtUserPassword.Text = ""
                    If txtUserPassword.CanFocus Then txtUserPassword.Focus()
                End If
            Else
                MsgBox("Not Found Please Check Your Password.....", MsgBoxStyle.Information)
                txtUserPassword.Text = ""
                txtUserPassword.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        Finally
            dsUser = Nothing
            objUser = Nothing
        End Try
    End Sub
    Private Sub subOpenCashDrawer()
        'Dim objPrint As New clsPrint
        'Try
        '    objPrint.subOpenCashDrawer(gsReceiptPrinterName)
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
    End Sub
    Public Function SubGetTerminal() As Boolean
        Dim objSales As New ClsSales
        Dim dsDinamination As New DataSet
        Dim dsTerminal As New DataSet
        Dim dsTerminalSales As New DataSet
        Dim dsPaidStatus As New DataSet
        Dim dsPaymode As New DataSet
        Dim dsExpense As New DataSet
        Dim dsDeposit As New DataSet
        Dim nDepositReturnAmount As Decimal
        Dim nExpenseAmount As Decimal
        Dim iRow As Integer
        Dim i As Integer
        Dim nComputerTotal As Decimal
        Dim Style As New DataGridViewCellStyle
        Try
            Style.Font = New Font(dgvSettlement.Font, FontStyle.Bold)
            Style.BackColor = Color.Yellow

            ''Dinamination
            dgvSettlement.Rows.Clear()
            iRow = dgvSettlement.Rows.Add
            fnSetdgvText(dgvSettlement, iRow, col1.Index, "$")
            fnSetdgvText(dgvSettlement, iRow, col2.Index, "No Of $")
            fnSetdgvText(dgvSettlement, iRow, col3.Index, "Amount")
            fnSetdgvText(dgvSettlement, iRow, col4.Index, "")
            dgvSettlement(col1.Index, iRow).ReadOnly = True
            dgvSettlement(col2.Index, iRow).ReadOnly = True
            dgvSettlement(col3.Index, iRow).ReadOnly = True
            dgvSettlement.Rows(iRow).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            'dgvSettlement.Rows(iRow).DefaultCellStyle.BackColor = Color.Yellow
            dgvSettlement.Rows(iRow).DefaultCellStyle = Style

            ''Check Is Date is Very Old For Settlement
            If objSales.fncCheckSettlementonOldDate(gsCon, gTerminalCode, dtDate.Text) Then
                MsgBox("Cannot do settlement for old date.", MsgBoxStyle.Critical)
                Exit Function
            End If

            ''Check Existing Record
            dsDinamination = objSales.fncGetTerminalSalesList(gsCon, "Den", gTerminalCode, dtDate.Text)
            If dsDinamination.Tables("Den").Rows.Count > 0 Then
                MsgBox("Settlement Already Done.", MsgBoxStyle.Information)
                For i = 0 To dsDinamination.Tables("Den").Rows.Count - 1
                    iRow = dgvSettlement.Rows.Add()
                    fnSetdgvText(dgvSettlement, iRow, col1.Index, dsDinamination.Tables("Den").Rows(i).Item("Qty") & "")
                    fnSetdgvText(dgvSettlement, iRow, col2.Index, dsDinamination.Tables("Den").Rows(i).Item("Amount") & "")
                    dgvSettlement(col1.Index, iRow).ReadOnly = True
                    dgvSettlement(col3.Index, iRow).ReadOnly = True
                    fnSetdgvText(dgvSettlement, iRow, col4.Index, "D")
                Next i

                ''Paymode
                iRow = dgvSettlement.Rows.Add
                fnSetdgvText(dgvSettlement, iRow, col1.Index, "Pay Mode")
                fnSetdgvText(dgvSettlement, iRow, col2.Index, "Computer Total")
                fnSetdgvText(dgvSettlement, iRow, col3.Index, "Manual Total")
                fnSetdgvText(dgvSettlement, iRow, col4.Index, "")
                dgvSettlement(col1.Index, iRow).ReadOnly = True
                dgvSettlement(col2.Index, iRow).ReadOnly = True
                dgvSettlement(col3.Index, iRow).ReadOnly = True
                dgvSettlement.Rows(iRow).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvSettlement.Rows(iRow).DefaultCellStyle = Style

                dsTerminalSales = objSales.fncGetExistingPayModeList(gsCon, "Sales", gTerminalCode, dtDate.Text)
                For i = 0 To dsTerminalSales.Tables("Sales").Rows.Count - 1
                    iRow = dgvSettlement.Rows.Add
                    fnSetdgvText(dgvSettlement, iRow, col1.Index, dsTerminalSales.Tables("Sales").Rows(i).Item("PayMode") & "")
                    fnSetNumeric(dgvSettlement, iRow, col2.Index, dsTerminalSales.Tables("Sales").Rows(i).Item("ComputerAmount") & "")
                    fnSetNumeric(dgvSettlement, iRow, col3.Index, dsTerminalSales.Tables("Sales").Rows(i).Item("ManualAmount") & "")
                    fnSetdgvText(dgvSettlement, iRow, col4.Index, "P")
                    If dsTerminalSales.Tables("Sales").Rows(i).Item("PayMode").Trim & "".ToUpper = "CASH" Then
                        nRowCash = iRow
                        dgvSettlement(col3.Index, iRow).ReadOnly = True
                    End If
                    dgvSettlement(col1.Index, iRow).ReadOnly = True
                    dgvSettlement(col2.Index, iRow).ReadOnly = True
                Next

                dsTerminal = objSales.fncGetTerminal(gsCon, "Term")
                If dsTerminal.Tables("Term").Rows.Count > 0 Then
                    sTerminalName = dsTerminal.Tables("Term").Rows(0).Item("TerminalName") & ""
                    nFloatAmount = Val(dsTerminal.Tables("Term").Rows(0).Item("TillAmount") & "")
                End If


                ''Add Sales Reurn Later




                ''Till Amount
                If nFloatAmount <> 0 Then
                    iRow = dgvSettlement.Rows.Add
                    fnSetdgvText(dgvSettlement, iRow, col1.Index, "Till Amount")
                    fnSetNumeric(dgvSettlement, iRow, col2.Index, Val(nFloatAmount))
                    fnSetNumeric(dgvSettlement, iRow, col3.Index, Val(nFloatAmount))
                    fnSetdgvText(dgvSettlement, iRow, col4.Index, "F")
                    dgvSettlement(col1.Index, iRow).ReadOnly = True
                    dgvSettlement(col2.Index, iRow).ReadOnly = True
                    dgvSettlement(col3.Index, iRow).ReadOnly = True
                End If

            Else
                ''No Existing Record Found
                dsDinamination = Nothing
                dsDinamination = objSales.fncGetCurrencyDinamination(gsCon, "Cur")
                If dsDinamination.Tables("Cur").Rows.Count > 0 Then
                    For i = 0 To dsDinamination.Tables("Cur").Rows.Count - 1
                        iRow = dgvSettlement.Rows.Add()
                        fnSetdgvText(dgvSettlement, iRow, col1.Index, dsDinamination.Tables("Cur").Rows(i).Item("CurrencyValue") & "")
                        dgvSettlement(col1.Index, iRow).ReadOnly = True
                        dgvSettlement(col3.Index, iRow).ReadOnly = True
                        fnSetdgvText(dgvSettlement, iRow, col4.Index, "D")
                    Next i
                End If

                ''Paymode
                iRow = dgvSettlement.Rows.Add
                fnSetdgvText(dgvSettlement, iRow, col1.Index, "Pay Mode")
                fnSetdgvText(dgvSettlement, iRow, col2.Index, "Computer Total")
                fnSetdgvText(dgvSettlement, iRow, col3.Index, "Manual Total")
                fnSetdgvText(dgvSettlement, iRow, col4.Index, "")
                dgvSettlement(col1.Index, iRow).ReadOnly = True
                dgvSettlement(col2.Index, iRow).ReadOnly = True
                dgvSettlement(col3.Index, iRow).ReadOnly = True
                dgvSettlement.Rows(iRow).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvSettlement.Rows(iRow).DefaultCellStyle = Style

                dsTerminal = objSales.fncGetTerminal(gsCon, "Term")
                If dsTerminal.Tables("Term").Rows.Count > 0 Then
                    sTerminalName = dsTerminal.Tables("Term").Rows(0).Item("TerminalName") & ""
                    nFloatAmount = Val(dsTerminal.Tables("Term").Rows(0).Item("TillAmount") & "")
                End If
                'Shabeer 20150127
                If Trim(sCompanyShortCode) = "AJ" Then
                    dsPaidStatus = objSales.fncGetPaidStatus(gsCon, "Sales", gTerminalCode, dtDate.Text)
                    If dsPaidStatus.Tables("Sales").Rows.Count > 0 Then
                        MsgBox("Payment Entry Pending", MsgBoxStyle.Information)
                        Me.Close()
                        Dim objpay As New frmPaymentEntry
                        objpay.ShowDialog()
                        Exit Function
                    End If

                    dsPaidStatus = objSales.fncGetFlexiDetails(gsConFlexi, "Flexi", gTerminalCode)
                    If dsPaidStatus.Tables("Flexi").Rows.Count > 0 Then
                        MsgBox("Flexi Entry Pending", MsgBoxStyle.Information)
                        Me.Close()
                        Dim objpay As New frmPaymentEntry
                        objpay.ShowDialog()
                        Exit Function
                    End If

                    dsPaidStatus = objSales.fncGeteTRS(gsCon, "eTRS", gTerminalCode)
                    If dsPaidStatus.Tables("eTRS").Rows.Count > 0 Then
                        MsgBox("eTRS Entry Pending", MsgBoxStyle.Information)
                        Me.Close()
                        Dim objpay As New frmPaymentEntry
                        objpay.ShowDialog()
                        Exit Function
                    End If
                    'Shabeer 20150127
                End If
                dsTerminalSales = objSales.fncGetTerminalSales(gsCon, "Sales", gTerminalCode, dtDate.Text)
                If dsTerminalSales.Tables("Sales").Rows.Count = 0 Then
                    'dsExpense = objSales.fncGetExpense(gsConFlexi, "Flexi", sTerminalCode, dtDate.Text)
                    'If dsExpense.Tables("Flexi").Rows.Count = 0 Then
                    MsgBox("No Sales Detail For Today", MsgBoxStyle.Information)
                    Exit Function
                    'End If
            End If




            For i = 0 To dsTerminalSales.Tables("Sales").Rows.Count - 1
                iRow = dgvSettlement.Rows.Add
                fnSetdgvText(dgvSettlement, iRow, col1.Index, dsTerminalSales.Tables("Sales").Rows(i).Item("PayMode") & "")
                fnSetNumeric(dgvSettlement, iRow, col2.Index, dsTerminalSales.Tables("Sales").Rows(i).Item("Amount") & "")
                fnSetdgvText(dgvSettlement, iRow, col4.Index, "P")
                If Trim(dsTerminalSales.Tables("Sales").Rows(i).Item("PayMode") & "") = "CASH" Then
                    nRowCash = iRow
                    fnSetNumeric(dgvSettlement, iRow, col2.Index, fnGetNumeric(dgvSettlement, iRow, col2.Index) - (nDepositReturnAmount + nExpenseAmount))
                    dgvSettlement(col3.Index, iRow).ReadOnly = True
                End If
                dgvSettlement(col1.Index, iRow).ReadOnly = True
                dgvSettlement(col2.Index, iRow).ReadOnly = True
            Next

            dsTerminalSales = objSales.fncGetTerminalSalesPayOut(gsCon, "Sales", gTerminalCode, dtDate.Text)

            For i = 0 To dsTerminalSales.Tables("Sales").Rows.Count - 1
                iRow = dgvSettlement.Rows.Add
                fnSetdgvText(dgvSettlement, iRow, col1.Index, dsTerminalSales.Tables("Sales").Rows(i).Item("PayMode") & "")
                fnSetNumeric(dgvSettlement, iRow, col2.Index, dsTerminalSales.Tables("Sales").Rows(i).Item("Amount") & "")
                fnSetNumeric(dgvSettlement, iRow, col3.Index, dsTerminalSales.Tables("Sales").Rows(i).Item("Amount") & "")
                fnSetdgvText(dgvSettlement, iRow, col4.Index, "P")
                dgvSettlement(col1.Index, iRow).ReadOnly = True
                dgvSettlement(col2.Index, iRow).ReadOnly = True
                dgvSettlement(col3.Index, iRow).ReadOnly = True
            Next
            ''Add Sales Reurn Later

            'Expense
            'dsExpense = objSales.fncGetExpense(gsConFlexi, "Flexi", sTerminalCode, dtDate.Text)
            'If dsExpense.Tables("Flexi").Rows.Count > 0 Then
            '    iRow = dgvSettlement.Rows.Add
            '    fnSetdgvText(dgvSettlement, iRow, col1.Index, "Flexi")
            '    fnSetNumeric(dgvSettlement, iRow, col2.Index, Val(dsExpense.Tables("Flexi").Rows(0).Item("Amount") & ""))
            '    fnSetNumeric(dgvSettlement, iRow, col3.Index, Val(dsExpense.Tables("Flexi").Rows(0).Item("Amount") & ""))
            '    fnSetdgvText(dgvSettlement, iRow, col4.Index, "P")
            '    dgvSettlement(col1.Index, iRow).ReadOnly = True
            '    dgvSettlement(col2.Index, iRow).ReadOnly = True
            '    dgvSettlement(col3.Index, iRow).ReadOnly = True
            'End If
                If Trim(sCompanyShortCode) = "AJ" Then
                    dsExpense = objSales.fncGetExpense(gsConFlexi, "Flexi", sTerminalCode, dtDate.Text)

                    If dsExpense.Tables("Flexi").Rows.Count > 0 Then
                        For i = 0 To dsExpense.Tables("Flexi").Rows.Count - 1
                            iRow = dgvSettlement.Rows.Add
                            fnSetdgvText(dgvSettlement, iRow, col1.Index, "Flexi" & " " & Trim(dsExpense.Tables("Flexi").Rows(i).Item("PayMode") & ""))
                            fnSetNumeric(dgvSettlement, iRow, col2.Index, Val(dsExpense.Tables("Flexi").Rows(i).Item("Amount") & ""))
                            fnSetNumeric(dgvSettlement, iRow, col3.Index, 0)
                            fnSetdgvText(dgvSettlement, iRow, col4.Index, "P")
                            dgvSettlement(col1.Index, iRow).ReadOnly = True
                            dgvSettlement(col2.Index, iRow).ReadOnly = True
                            dgvSettlement(col3.Index, iRow).ReadOnly = True

                        Next
                    End If
                End If
                ''Deposit Return
                ''dsDeposit = objSales.fncGetDepositReturn(gsConn, "Dep", gsServerDate)
                ''If Trim(gsCompanyShortCode) = "VZ" Then
                ''    If dsDeposit.Tables("Dep").Rows.Count > 0 Then
                ''        iRow = dgvSettlement.Rows.Add
                ''        fnSetdgvText(dgvSettlement, iRow, col1.Index, "Deposit Return")
                ''        fnSetNumeric(dgvSettlement, iRow, col2.Index, Val(dsDeposit.Tables("Dep").Rows(0).Item("Amount") & ""))
                ''        fnSetNumeric(dgvSettlement, iRow, col3.Index, Val(dsDeposit.Tables("Dep").Rows(0).Item("Amount") & ""))
                ''        fnSetdgvText(dgvSettlement, iRow, col4.Index, "R")
                ''        dgvSettlement(col1.Index, iRow).ReadOnly = True
                ''        dgvSettlement(col2.Index, iRow).ReadOnly = True
                ''        dgvSettlement(col3.Index, iRow).ReadOnly = True
                ''    End If
                ''End If

                ''Till Amount
                If nFloatAmount <> 0 Then
                    iRow = dgvSettlement.Rows.Add
                    fnSetdgvText(dgvSettlement, iRow, col1.Index, "Till Amount")
                    fnSetNumeric(dgvSettlement, iRow, col2.Index, Val(nFloatAmount))
                    fnSetNumeric(dgvSettlement, iRow, col3.Index, Val(nFloatAmount))
                    fnSetdgvText(dgvSettlement, iRow, col4.Index, "F")
                    dgvSettlement(col1.Index, iRow).ReadOnly = True
                    dgvSettlement(col2.Index, iRow).ReadOnly = True
                    dgvSettlement(col3.Index, iRow).ReadOnly = True
                End If
                End If



                ''Total
                nComputerTotal = 0
                For i = 0 To dgvSettlement.RowCount - 1
                    If fnGetdgvText(dgvSettlement, i, col4.Index) = "P" Then
                        nComputerTotal = nComputerTotal + fnGetdgvText(dgvSettlement, i, col2.Index)
                        ''ElseIf fnGetdgvText(dgvSettlement, i, col4.Index) = "E" Or fnGetdgvText(dgvSettlement, i, col4.Index) = "R" Then
                        ''    nComputerTotal = nComputerTotal - fnGetdgvText(dgvSettlement, i, col2.Index)
                    End If
                Next i

                iRow = dgvSettlement.Rows.Add
                nRowTotal = iRow
                fnSetdgvText(dgvSettlement, iRow, col1.Index, "Total")
                fnSetNumeric(dgvSettlement, iRow, col2.Index, nComputerTotal + nFloatAmount)
                fnSetdgvText(dgvSettlement, iRow, col4.Index, "T")
                dgvSettlement(col1.Index, iRow).ReadOnly = True
                dgvSettlement(col2.Index, iRow).ReadOnly = True
                dgvSettlement(col3.Index, iRow).ReadOnly = True
                dgvSettlement.Rows(iRow).DefaultCellStyle = Style

                dgvSettlement.Height = dgvSettlement.Rows.Count * 23.2
                Me.cmdSave.Location = New System.Drawing.Point(94, dgvSettlement.Height + 70)
                Me.cmdExit.Location = New System.Drawing.Point(183, dgvSettlement.Height + 70)
                Me.Height = dgvSettlement.Height + 130
                Me.CenterToScreen()
                subCalcTotal()
                SubGetTerminal = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            SubGetTerminal = False
        Finally
            objSales = Nothing
            dsDinamination = Nothing
            dsTerminal = Nothing
            dsTerminalSales = Nothing
            dsPaymode = Nothing
            dsExpense = Nothing
            dsDeposit = Nothing
        End Try
    End Function
    Private Sub subCalcTotal()
        Dim nManualTotal As Decimal
        Dim nCashTotal As Decimal
        Dim nDenomination As Decimal
        Dim nNoOfDolar As Decimal
        Dim iRow As Integer
        Try
            For iRow = 0 To dgvSettlement.RowCount - 1
                If fnGetdgvText(dgvSettlement, iRow, col4.Index) = "D" Then
                    fnSetNumeric(dgvSettlement, iRow, col2.Index, Val(fnGetdgvText(dgvSettlement, iRow, col2.Index)))
                    nDenomination = Val(fnGetdgvText(dgvSettlement, iRow, col1.Index))
                    nNoOfDolar = Val(fnGetdgvText(dgvSettlement, iRow, col2.Index))
                    fnSetNumeric(dgvSettlement, iRow, col3.Index, nDenomination * nNoOfDolar)
                    nCashTotal = nCashTotal + fnGetdgvText(dgvSettlement, iRow, col3.Index)
                    fnSetNumeric(dgvSettlement, nRowCash, col3.Index, nCashTotal)
                ElseIf fnGetdgvText(dgvSettlement, iRow, col4.Index) = "P" Or fnGetdgvText(dgvSettlement, iRow, col4.Index) = "F" Then
                    If iRow <> nRowCash Then
                        fnSetNumeric(dgvSettlement, iRow, col3.Index, fnGetdgvText(dgvSettlement, iRow, col3.Index))
                    End If
                    nManualTotal = nManualTotal + fnGetdgvText(dgvSettlement, iRow, col3.Index)
                    fnSetNumeric(dgvSettlement, nRowTotal, col3.Index, nManualTotal)
                    'ElseIf fnGetdgvText(dgvSettlement, iRow, col4.Index) = "R" Or fnGetdgvText(dgvSettlement, iRow, col4.Index) = "E" Then
                    '    If iRow <> nRowCash Then
                    '        fnSetNumeric(dgvSettlement, iRow, col3.Index, fnGetdgvText(dgvSettlement, iRow, col3.Index))
                    '    End If
                    '    nManualTotal = nManualTotal - fnGetdgvText(dgvSettlement, iRow, col3.Index)
                    '    fnSetNumeric(dgvSettlement, nRowTotal, col3.Index, nManualTotal)
                End If
            Next iRow
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub dgvSettlement_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSettlement.CellEnter
        Try
            subCalcTotal()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cmdOK_Click(sender As System.Object, e As System.EventArgs) Handles cmdOK.Click
        Try
            If txtUserID.Text.Trim = "" Then
                MsgBox("Please key in userid.", MsgBoxStyle.Information)
                If txtUserID.CanFocus Then txtUserID.Focus()
                Exit Sub
            End If
            If txtUserPassword.Text.Trim = "" Then
                MsgBox("Please key in password.", MsgBoxStyle.Information)
                If txtUserPassword.CanFocus Then txtUserPassword.Focus()
                Exit Sub
            End If
            subGetPassword()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As System.EventArgs) Handles cmdSave.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If fncTerminalSalesSave() Then
                'subPrintSettlementReport()
                'subPrintBillCancelReceipt()
                'subPrintDiscountReceipt()
                'subPrintPaymodeChageReceipt()
                'subPrintOverallSettlementReport()
                subPrintSettlementInReceipt()
                subEODProcess()
                Me.Dispose()
                'Dim objEod As New frmDayEndtoHQ
                'objEod.Show()
                'objEod = Nothing

                Dim objEod As New frmUpdatetoHQ
                objEod.Show()
                objEod = Nothing
            Else
                MsgBox("Transaction Not Updated.", MsgBoxStyle.Critical)
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Public Function fncTerminalSalesSave() As Boolean
        Dim objSales As New ClsSales
        Dim bResult As Boolean
        Dim sPaymode As String
        Dim nComputerTotal As Decimal
        Dim nManualTotal As Decimal
        Dim nValue As Decimal
        Dim nQty As Decimal
        Dim iRow As Integer
        Try
            OpenConnection()
            BeginTransaction()
            If fncDeleteExisting() = False Then
                fncTerminalSalesSave = False
                RollBackTransaction()
                MsgBox("Error in Save", MsgBoxStyle.Critical)
                Exit Function
            End If

            For iRow = 0 To dgvSettlement.RowCount - 1
                If fnGetdgvText(dgvSettlement, iRow, col4.Index) = "D" Then
                    nValue = fnGetdgvText(dgvSettlement, iRow, col1.Index)
                    nQty = fnGetdgvText(dgvSettlement, iRow, col2.Index)
                    If Not objSales.fncTerminalSalesDinaminationAmountSave(sCashierID, sTerminalCode, dtDate.Text, _
                                                                           nValue, nQty, bResult) Then
                        fncTerminalSalesSave = False
                        RollBackTransaction()
                        MsgBox("Error in Save", MsgBoxStyle.Critical)
                    End If
                    ''ElseIf fnGetdgvText(dgvSettlement, iRow, col4.Index) = "P" Or fnGetdgvText(dgvSettlement, iRow, col4.Index) = "F" Then
                ElseIf fnGetdgvText(dgvSettlement, iRow, col4.Index) = "P" Then
                    sPaymode = fnGetdgvText(dgvSettlement, iRow, col1.Index)
                    nComputerTotal = fnGetdgvText(dgvSettlement, iRow, col2.Index)
                    nManualTotal = fnGetdgvText(dgvSettlement, iRow, col3.Index)
                    If Not objSales.fncTerminalSalesPayModeAmountSave(sCashierID, dtDate.Text, sTerminalCode, sPaymode, _
                                                                  nComputerTotal, nManualTotal, bResult) Then
                        fncTerminalSalesSave = False
                        RollBackTransaction()
                        MsgBox("Error in Save", MsgBoxStyle.Critical)
                    End If
                End If
            Next iRow

            If bResult = True Then
                CommitTransaction()
                fncTerminalSalesSave = True
            Else
                RollBackTransaction()
                fncTerminalSalesSave = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            RollBackTransaction()
            fncTerminalSalesSave = False
        Finally
            CloseConnection()
        End Try
    End Function
    Private Function fncDeleteExisting() As Boolean
        Dim objSales As New ClsSales
        Try
            fncDeleteExisting = objSales.fncDeleteExsistingTerminalSales(dtDate.Text, sTerminalCode)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Function
    Private Sub subPrintSettlementInReceipt()
        Dim ReportTitle As String
        Dim sSelection As String
        Dim sSalesType$
        Dim frmrep As New frmReports
        Dim CR As New ReportDocument
        Dim j As Integer
        Dim objSR As New ClsSalesReturn
        Dim sReportName As String = ""
        Dim sSelectionFormula As String = ""
        Dim sFormula1Name As String = ""
        Dim sFormula1Value As String = ""
        Dim sReportTitle As String = ""
        Try
            'sReportName = "TerminalUpdationSales.Rpt"
            strpath = Application.StartupPath & "\Reports\TerminalUpdationSales.rpt"
            sReportTitle = "Settlement Report For " & FormatDateTime(dtDate.Text, 2)
            sSelectionFormula = "{tblTerminalSales.Date} = " & fncFormatReportDate(dtDate.Text)
            sSelectionFormula = sSelectionFormula & " and " & "{tblTerminalSales.TerminalCode}=" & fncFormatSQLText(sTerminalCode)
            sSelectionFormula = sSelectionFormula & " and " & "{tblTerminalSales.CashierId}=" & fncFormatSQLText(sCashierID)

            sFormula1Name = "TillAmount"
            sFormula1Value = nFloatAmount

            CR.Load(strpath)
            If Not ReportLogOnToServer(CR) Then
                Exit Sub
            End If
            ReportLogOnToServer(CR)
            CR.Refresh()
            frmrep.ReportViewer.ReportSource = CR
            'sSelection = "{tblInventoryRepairHeader.RepairNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
            frmrep.ReportViewer.SelectionFormula = sSelectionFormula
            CR.DataDefinition.RecordSelectionFormula = sSelectionFormula
            'CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
            'CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
            'CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
            'CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
            'CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
            'CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
            ' frmrep.Show()
            CR.PrintToPrinter(1, True, 0, 0)
            'frmrep.ReportViewer.Visible = True
            'frmrep.ReportViewer.DisplayToolbar = True
            'frmrep.ReportViewer.CloseView(False)
            'frmrep.WindowState = FormWindowState.Maximized
            frmrep = Nothing
            frmrep = New frmReports

            'subPrintReportInReciptPrinter(Me, sReportName, sReportTitle, sSelectionFormula, True, sFormula1Name, sFormula1Value, False, _
            '                     "", "", True, "", "", True)


            ' strpath = Application.StartupPath & "\Reports\rptSalesJournalSummary.rpt"
            sReportName = "rptSalesJournalSummary.Rpt"
            sReportTitle = "Sales Report"

            sSelectionFormula = "{tblSalesHeader.InvoiceDate} = " & fncFormatReportDate(dtDate.Text)
            '[  sSelectionFormula = sSelectionFormula & " and " & "{tblSalesDetail.SellHire}=" & Val(gObjPM.gpTypeSellthrough)

            ' subPrintReportInReciptPrinter(Me, sReportName, sReportTitle, sSelectionFormula, True, sFormula1Name, sFormula1Value, False, _
            '                       "", "", True, "", "", True)


            'CR.Load(strpath)
            'If Not ReportLogOnToServer(CR) Then
            '    Exit Sub
            'End If
            'ReportLogOnToServer(CR)
            'CR.Refresh()
            'frmrep.ReportViewer.ReportSource = CR
            ''sSelection = "{tblInventoryRepairHeader.RepairNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
            'frmrep.ReportViewer.SelectionFormula = sSelectionFormula
            'CR.DataDefinition.RecordSelectionFormula = sSelectionFormula
            ''CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
            ''CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
            ''CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
            ''CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
            ''CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
            ''CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
            '' frmrep.Show()
            'CR.PrintToPrinter(1, True, 0, 0)
            ''frmrep.ReportViewer.Visible = True
            ''frmrep.ReportViewer.DisplayToolbar = True
            ''frmrep.ReportViewer.CloseView(False)
            ''frmrep.WindowState = FormWindowState.Maximized
            'frmrep = Nothing
            'frmrep = New frmReports


            CR.ExportToDisk(ExportFormatType.PortableDocFormat, Application.StartupPath & "Settlement" & ".pdf")
            Dim sFilePath As String
            sFilePath = Application.StartupPath & "Settlement" & ".pdf"
            'MsgBox("3")
            If (UCase(Objuser.CompanyName)) = "ABIRAAME JEWELLERS" Then
                fncSendMailWithAttachment(sFilePath, "Settlement Report", "", "softshabeer@yahoo.com", "singaiasraf76", "smtp.mail.yahoo.com", "465")
                subGetEmailReports()
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Public Sub subGetEmailReports()
        Dim sLn$, sSplit$, sFilePath$
        Dim adoRS As DataSet
        Dim ssql As String
        Dim DR As DataRow()
        Dim i As Integer
        Dim sLLocationAddress1 As String
        Dim sLLocationAddress2 As String
        Try

            If Trim(gthisLocation) = "99" Then
                sLLocationAddress1 = "99 SERANGOON ROAD"
                sLLocationAddress2 = "SINGAPORE 218004"
            Else
                sLLocationAddress1 = "69 SERANGOON ROAD"
                sLLocationAddress2 = "SINGAPORE 217974"
            End If

            sLn = "---------------------------------------"
            sSplit = "|"

            sFilePath = Application.StartupPath & "\" & dtDate.Text & ".txt"
            FileOpen(1, sFilePath, OpenMode.Output)
            Print(1, sLn & vbCrLf)
            Print(1, "            DAY END DETAILS" & vbCrLf)
            Print(1, sLn & vbCrLf)
            Print(1, Objuser.CompanyName & vbCrLf)
            Print(1, sLLocationAddress1 & vbCrLf)
            Print(1, sLLocationAddress2 & vbCrLf)
            Print(1, Objuser.sLocationAddress3 & vbCrLf)
            Print(1, Objuser.Phone & vbCrLf)
            Print(1, Objuser.Fax & vbCrLf)
            Print(1, Objuser.Email & vbCrLf)
            Print(1, "Date & Time       : " & (dtDate.Text) & vbCrLf)
            Print(1, sLn & vbCrLf)
            Print(1, "           SALES DETAILS" & vbCrLf)
            Print(1, sLn & vbCrLf)
            Print(1, " " & vbCrLf)
            'sSql = "select sum(netTotal)as NetAmount from vwSalesHeader where InvoiceDate>=" & fncFormatSQLText(ServerDate)
            'GetRecordset(gsConn, ssql, adoRsReports)

            ssql = "Select sum(netTotal)as NetAmount from vwSalesHeader where InvoiceDate=" & fncFormatSQLDate(dtDate.Text)
            adoRS = dm.getDataSetWithSqlAdapter(ssql, "Table", "Table")
            'Print(1, "Total Sales       : " & Space(15 - Len(Format(Val(adoRS.Tables(0).Rows(0)("NetAmount") & "")))) & Format(Val(adoRS.Tables(0).Rows(0)("NetAmount") & "")) & vbCrLf)
            Print(1, "Total Sales       : " & Space(15 - Len(Format(Val(adoRS.Tables(0).Rows(0)("NetAmount") & ""), "0.00"))) & Format(Val(adoRS.Tables(0).Rows(0)("NetAmount") & ""), "0.00") & vbCrLf)
            Print(1, sLn & vbCrLf)
            Print(1, "          PAYMODE DETAILS" & vbCrLf)
            Print(1, sLn & vbCrLf)
            Print(1, " " & vbCrLf)

            ' ssql = "select A.paymode as Paymode,sum(amount)as TotalAmount from vwpaymodesummary A,tblPaymode B where a.Paymode=B.Paymode and B.Active=1 and locationcode = " & fncFormatSQLText(gthisLocation) & " and InvoiceDate>=" & fncFormatSQLDate(dtDate.Text) & " group by A.paymode,position order by  B.Position"
            ssql = "select paymode as Paymode,sum(amount)as TotalAmount from vwpaymodesummary  where locationcode = " & fncFormatSQLText(gthisLocation) & " and InvoiceDate=" & fncFormatSQLDate(dtDate.Text) & " group by paymode order by  paymode"
            DR = DataManager.RowReturnProjExecNonQryWithDataRow(ssql)
            For i = 0 To DR.Length - 1
                Print(1, Trim(DR(i)("Paymode") & "") & Space(17 - Len(Trim(DR(i)("Paymode") & ""))) & " : " & Space(15 - Len(Format(Val(DR(i)("TotalAmount") & ""), "0.00"))) & Format(Val(DR(i)("TotalAmount") & ""), "0.00") & vbCrLf)
                'Print(1, Trim(DR(i)("Paymode") & "" & " : " & Space(15 - Len(Format(Val(DR(i)("TotalAmount") & ""), "0.00")))) & Format(Val(DR(i)("TotalAmount") & ""), "0.00") & vbCrLf)
            Next
            'For i = 1 To adoRsReports.RecordCount
            '    Print #1, Trim(adoRsReports!PayMode) & Space(17 - Len(Trim(adoRsReports!PayMode))) & " : " & Space(15 - Len(Format(Trim(adoRsReports!TotalAmount), AmountDisplay))) & Format(Trim(adoRsReports!TotalAmount), AmountDisplay)
            '    adoRsReports.MoveNext()
            'Next
            'sangeetha 2011-04-08 voucher detail print
            Print(1, sLn & vbCrLf)
            Print(1, "  CATEGORYWISE SALES DETAILS" & vbCrLf)
            Print(1, sLn & vbCrLf)
            Print(1, " " & vbCrLf)
            'sSql = "select sum(netTotal)as NetAmount from vwSalesHeader where InvoiceDate>=" & fncFormatSQLText(ServerDate)
            'GetRecordset(gsConn, ssql, adoRsReports)

            ssql = "Select CategoryCode as CategoryCode,Weight as Weight,Pcs as Pcs from vwSalesCategory where InvoiceDate=" & fncFormatSQLDate(dtDate.Text)
            DR = DataManager.RowReturnProjExecNonQryWithDataRow(ssql)
            'Print(1, "Total Sales       : " & Space(15 - Len(Format(Val(adoRS.Tables(0).Rows(0)("NetAmount") & "")))) & Format(Val(adoRS.Tables(0).Rows(0)("NetAmount") & "")) & vbCrLf)
            For i = 0 To DR.Length - 1
                Print(1, Trim(DR(i)("CategoryCode") & "") & Space(17 - Len(Trim(DR(i)("CategoryCode") & ""))) & " : " & Space(7 - Len(Format(Val(DR(i)("Pcs") & ""), "0.00"))) & Format(Val(DR(i)("Pcs") & ""), "0.00") & " : " & Space(7 - Len(Format(Val(DR(i)("Weight") & ""), "0.00"))) & Format(Val(DR(i)("Weight") & ""), "0.00") & vbCrLf)
            Next
            Print(1, sLn & vbCrLf)
            Print(1, " " & vbCrLf)

            ''Print(1, sLn & vbCrLf)
            ''Print(1, "  CATALOGUEWISE SALES DETAILS" & vbCrLf)
            ''Print(1, sLn & vbCrLf)
            ''Print(1, " " & vbCrLf)
            ' ''sSql = "select sum(netTotal)as NetAmount from vwSalesHeader where InvoiceDate>=" & fncFormatSQLText(ServerDate)
            ' ''GetRecordset(gsConn, ssql, adoRsReports)

            ''ssql = "Select CatalogueCode as CatalogueCode,Weight as Weight from vwSalesCatalogue where InvoiceDate=" & fncFormatSQLDate(dtDate.Text)
            ''DR = DataManager.RowReturnProjExecNonQryWithDataRow(ssql)
            ' ''Print(1, "Total Sales       : " & Space(15 - Len(Format(Val(adoRS.Tables(0).Rows(0)("NetAmount") & "")))) & Format(Val(adoRS.Tables(0).Rows(0)("NetAmount") & "")) & vbCrLf)
            ''For i = 0 To DR.Length - 1
            ''    Print(1, Trim(DR(i)("CatalogueCode") & "") & Space(17 - Len(Trim(DR(i)("CatalogueCode") & ""))) & " : " & Space(15 - Len(Format(Val(DR(i)("Weight") & ""), "0.00"))) & Format(Val(DR(i)("Weight") & ""), "0.00") & vbCrLf)
            ''Next
            ''Print(1, sLn & vbCrLf)
            ''Print(1, " " & vbCrLf)
            'sSql = "SELECT VoucherCode,sum(VoucherAmount)VoucherAmount,sum(Denomination)Denomination,sum(VoucherTotal)VoucherTotal FROM tblSupplierAndOurVoucherDetailCUR where invoicedate>=" & fncFormatSQLText(txtDayEndDate) & " Group By VoucherCode"
            'GetRecordset(gsConn, sSql, adoRsReports)
            'Print #1, "        VOUCHER DETAILS"
            'Print #1, "VoucherCode" & " " & "Qty" & "      " & "Amount" & "  " & "  Total"
            'For i = 1 To adoRsReports.RecordCount
            '  Print #1, Space(1) + Left$(adoRsReports!VoucherCode & "", 10) + Space(2) + (Trim(adoRsReports!Denomination & "")) + Space(9 - Len(((adoRsReports!Denomination & "")))) + Format(adoRsReports!VoucherAmount, "####0.00") + Space(6) + Format(adoRsReports!VoucherTotal, "####0.00")
            '    adoRsReports.MoveNext()
            'Next
            'Print #1, sLn
            'Print #1, " "
            'sSql = "select sum(amount) as TotalPayModesAmount from  vwpaymodesummary  where InvoiceDate>=" & fncFormatSQLText(txtDayEndDate.Text)
            'GetRecordset(gsConn, sSql, adoRsReports)
            'Print #1, "Total             : " & Space(15 - Len(Format(Trim(adoRsReports!TotalPayModesAmount), AmountDisplay))) & Format(Trim(adoRsReports!TotalPayModesAmount), AmountDisplay)
            'Print #1, sLn
            'Print #1, "        SETTLEMENT DETAILS"
            'Print #1, sLn
            'Print #1, " "
            ''sSql = "select sum(computeramount) as ComputerAmount,sum(manualamount) as ManualAmount,sum(sortorexces) as ShortageorExcess from vwterminalsales where Date>=" & fncFormatSQLText(txtDayEndDate.Text)
            ''GetRecordset(gsConn, sSql, adoRsReports)
            ''Print #1, "ComputerAmount    : " & Space(15 - Len(Format(Trim(adoRsReports!ComputerAmount), AmountDisplay))) & Format(Trim(adoRsReports!ComputerAmount), AmountDisplay)
            ''Print #1, "ManualAmount      : " & Space(15 - Len(Format(Trim(adoRsReports!ManualAmount), AmountDisplay))) & Format(Trim(adoRsReports!ManualAmount), AmountDisplay)
            ''Print #1, sLn
            ''Print #1, "        EXCESS OR SHORTAGE"
            ''Print #1, sLn
            ''Print #1, " "
            ''Print #1, "Excess OR Shortage: " & Space(15 - Len(Format(Trim(adoRsReports!ShortageorExcess), AmountDisplay))) & Format(Trim(adoRsReports!ShortageorExcess), AmountDisplay)
            ''Print #1, sLn
            ''Print #1, "            ANALYSIS"
            ''Print #1, sLn
            ''Print #1, " "
            'ssql = "select sum(netTotal)as NetAmount,count(InvoiceNo) as NoOfBill from vwSalesHeader where InvoiceDate>=" & fncFormatSQLText(txtDayEndDate.Text)
            'DR = DataManager.RowReturnProjExecNonQryWithDataRow(ssql)
            'Print #1, "Sales Amount      : " & Space(15 - Len(Format(Val(adoRS.Tables(0).Rows(0)("NetAmount") & ""), "0.00"))) & Format(Val(adoRS.Tables(0).Rows(0)("NetAmount") & ""), "0.00")
            'Print #1, "No of Bills       : " & Space(15 - Len(Trim(adoRsReports!NoOfBill))) & Trim(adoRsReports!NoOfBill)
            'Print #1, "Avg/Bill          : " & Space(15 - Len(Format(Trim(adoRsReports!NetAmount) / Trim(adoRsReports!NoOfBill), AmountDisplay))) & Format(Trim(adoRsReports!NetAmount) / Trim(adoRsReports!NoOfBill), AmountDisplay)
            'Print #1, sLn
            Print(1, "    ......HAVE A PLEASANT DAY......" & vbCrLf)
            Print(1, sLn & vbCrLf)
            FileClose(1)
            'sEmailList = fnGetEmailId("IPHONE")
            'Swetha 30-01-2015 DNS Email - DisplayName
            'If sCompanyShortCode = "DNS" Then
            ' Call subSendEmail(Trim(gthisLocation) & " Sales - iPhone Report", "Hi, OverView Day End Report For the Date " & txtDayEndDate.Text, sFilePath, sEmailList, "", "")
            If (UCase(Objuser.CompanyName)) = "ABIRAAME JEWELLERS" Then
                fncSendMailWithAttachment(sFilePath, "DAY END DETAILS", "", "softshabeer@yahoo.com", "singaiasraf76", "smtp.mail.yahoo.com", "465")
            End If
            'Else
            'Call subSendEmail(Trim(gthisLocation) & " - iPhone Report", "Hi, OverView Day End Report For the Date " & txtDayEndDate.Text, sFilePath, sEmailList, "", "")
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            FileClose(1)
        End Try
    End Sub
    Public Function fncSendMailWithAttachment(ByVal sFilePath As String, ByVal sEmailStr As String, ByVal sReport As String, ByVal sEMailId As String, ByVal sEmailPwd As String, ByVal sMailServer As String, ByVal sPort As String) As Boolean
        'Dim obj As System.Web.Mail.SmtpMail
        'Dim oMsg As MailMessage = New MailMessage
        'Dim objMail As New MailMessage
        'Dim dsEmail As New DataSet
        'Dim iCnt As Integer
        'Dim sMailTo As String
        'Dim dm As New DataManager
        'Dim bStatus As Boolean
        'Dim sMailid() As String
        'Dim objAttach As System.Web.Mail.MailAttachment
        'Dim mailPort As Integer
        'Try
        '    objMail = New MailMessage
        '    'If sMailTo.Length > 0 Then
        '    '    sMailTo = sMailTo.Remove(sMailTo.Length - 1, 1)
        '    'End If
        '    oMsg.From = sEMailId
        '    oMsg.To = sReceiverMailID
        '    Dim Attachment As System.Web.Mail.MailAttachment
        '    Attachment = New System.web.Mail.MailAttachment(sFilePath)
        '    oMsg.Attachments.Add(Attachment)
        '    oMsg.Subject = "Currency Stock Report as on " & DateTime.Now
        '    oMsg.BodyEncoding = System.Text.Encoding.UTF8
        '    oMsg.Body = sEmailStr
        '    oMsg.Priority = MailPriority.High

        '    SmtpMail.SmtpServer = sSMTP

        '    SmtpMail.Send(oMsg)
        '    MsgBox("Mail send successfully", MsgBoxStyle.Information)
        '    bStatus = True
        'Catch ex As Exception
        '    Throw ex
        '    bStatus = False
        'Finally
        '    fncSendMailWithAttachment = bStatus
        'End Try
        Dim objMail As New MailMessage
        Dim dsEmail As New DataSet
        Dim iCnt As Integer
        Dim sMailTo As String
        'Dim objGeneral As New clsGeneral
        Dim bStatus As Boolean
        Dim sMailid() As String
        Dim objAttch As Mail.Attachment
        Dim mailPort As Integer
        Try
            'objGeneral = New clsGeneral
            'dsEmail = objGeneral.fncGetDatasetWithQuery("EMAIL", "Select * from EmailList where Active = 1 and UPPER(ReportName) =" & fncFormatSQLText(UCase(sReport)))
            'If dsEmail.Tables(0).Rows.Count > 0         Then
            '    For iCnt = 0 To dsEmail.Tables(0).Rows.Count - 1
            '        If fncValidateEmail(dsEmail.Tables(0).Rows(iCnt)("Emailid").ToString()) = True Then
            '            sMailTo &= Trim(dsEmail.Tables(0).Rows(iCnt)("Emailid").ToString()) & ";"
            '        End If
            '    Next
            'Else
            '    bStatus = True
            '    Exit Function
            'End If
            If (UCase(Objuser.CompanyName)) = "ABIRAAME JEWELLERS" Then
                sMailTo = "arun@ajs.com.sg;dicksonpalani@gmail.com;anuarun22@gmail.com;"
                '  sMailTo = "shabeer@unipro.com.sg;shabeer.unipro@gmail.com;"
            End If

            'sMailTo = "shabeer@unipro.com.sg;shabeer.unipro@gmail.com;"
            ' sMailTo = "shabeer@unipro.com.sg1"
            objMail = New MailMessage
            If sMailTo.Length > 0 Then
                sMailTo = sMailTo.Remove(sMailTo.Length - 1, 1)
            End If
            objMail.From = New MailAddress("abigoldngems@gmail.com")
            sMailid = sMailTo.Split(";")
            For iCnt = 0 To sMailid.Length - 1
                objMail.To.Add(sMailid(iCnt))
            Next
            objMail.Subject = sReport & "Settlement Report"
            objAttch = New Attachment(sFilePath)
            objMail.Attachments.Add(objAttch)
            objMail.BodyEncoding = System.Text.Encoding.UTF8
            objMail.Body = sEmailStr
            objMail.IsBodyHtml = False
            objMail.Priority = MailPriority.High
            'objMail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure

            'Dim smtp As New SmtpClient("smtp.mail.yahoo.com")
            'smtp.Credentials = New System.Net.NetworkCredential(Trim("softshabeer@yahoo.com"), Trim("singaiasraf76"))
            'mailPort = CInt("465")
            'smtp.Port = mailPort
            ''smtp.Host = gsMailServer
            'smtp.EnableSsl = False
            ''smtp.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis
            'Dim userState As Object = smtp

            Dim smtp As New SmtpClient("smtp.gmail.com")
            smtp.Credentials = New System.Net.NetworkCredential(Trim("abigoldngems@gmail.com"), Trim("Arun4321"))
            mailPort = CInt("587")
            smtp.Port = mailPort
            'smtp.Host = gsMailServer
            smtp.EnableSsl = 1

            'smtp.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis
            Dim userState As Object = smtp
            Try
                smtp.SendAsync(objMail, userState)
            Catch ex As System.Net.Mail.SmtpException
                MessageBox.Show(ex.Message, "Send Mail Error")
            End Try
            'objMail.Subject = sReport & " Report as on "
            'objMail.BodyEncoding = System.Text.Encoding.UTF8
            'objMail.Body = sEmailStr
            'objMail.IsBodyHtml = False
            'objMail.Priority = MailPriority.High
            ' ''objMail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
            'Dim smtp As New SmtpClient("smtp.gmail.com")
            'smtp.Credentials = New System.Net.NetworkCredential(Trim("retailcafe@uniproapps.com"), Trim("A62541892A"))
            'mailPort = CInt(587)
            'smtp.Port = mailPort
            ''smtp.Host = gsMailServer
            'smtp.EnableSsl = 1
            ''smtp.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis
            'Dim userState As Object = smtp
            'Try
            '    smtp.SendAsync(objMail, userState)
            'Catch ex As System.Net.Mail.SmtpException
            '    MessageBox.Show(ex.Message, "Send Mail Error")
            'End Try

            bStatus = True
        Catch ex As Exception
            Throw ex
            bStatus = False
        Finally
            fncSendMailWithAttachment = bStatus
        End Try
    End Function

    Private Sub subEODProcess()
      Dim sPrevEODDate, sEODDate As String
        Try
            If Not IsDate(dtDate.Text) Then
                MessageBox.Show("Enter Valid EOD Date", "GoldPOS")
                dtDate.Focus()
                Exit Sub
            End If

            ' sPrevEODDate = txtLDD.Text.Trim & "/" & txtLMM.Text.Trim & "/" & txtLYYYY.Text.Trim
            sEODDate = dtDate.Text

            'Check if last eod date is same day as eoddate or one day before eod date
            'If DateDiff("d", DateValue(sPrevEODDate), DateValue(sEODDate)) <> 0 And DateDiff("d", DateValue(sPrevEODDate), DateValue(sEODDate)) <> 1 Then
            '    MessageBox.Show("Last EOD Date is " & sPrevEODDate & ". EOD can be done only for " & sPrevEODDate & " or " & Format(DateAdd("d", 1, DateValue(sPrevEODDate)), "dd/MM/yyyy") & "!", "GoldPOS")
            '    Exit Sub
            'End If
            If UCase(sCompanyShortCode) = "RJ" Then
                If MessageBox.Show("Do you want to do the EOD Now ?", "GoldPOS", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    'do EOD process
                    If Not fncEODProcess(sEODDate) Then
                        MessageBox.Show("Error in EOD Process.")
                        Exit Sub
                    End If
                Else
                    Me.Dispose()
                    Exit Sub
                End If
            Else
                'do EOD process
                If Not fncEODProcess(sEODDate) Then
                    MessageBox.Show("Error in EOD Process.")
                    Exit Sub
                End If

                'If sAccountsModule = "1" Then
                '    'do settlement posting
                '    subPOSSettlement()
                'End If
            End If


            'Dim ctrl
            'For Each ctrl In Me.Controls
            '    If TypeOf ctrl Is CheckBox Then
            '        If ctrl.Checked = True Then
            '            Call UpdateEODReports(ctrl.Tag, "Y")
            '        Else
            '            Call UpdateEODReports(ctrl.Tag, "N")
            '        End If
            '    End If
            'Next

            'to print reports
            subPrintReports(sEODDate)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subPrintReports(ByVal sEODdate As String)
        Me.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim sTelephone, EODDate, sReportTitle As String
        Dim objcompany As New clsCompany
        Dim frmrep As New frmReports
        Dim frmrep1 As New frmReports
        Dim CR As New ReportDocument
        Dim CR1 As New ReportDocument
        Dim dsComp As New DataSet
        Dim sTradeInReportTitle As String
        Dim dsSales As New DataSet
        Dim objEod As New ClsEOD
        Dim sSalesDate As String
        Try
            'EODDate = txtYYYY.Text.Trim & "," & txtMM.Text.Trim & "," & txtDD.Text.Trim
            EODDate = dtDate.Text.Trim
            '  sTradeInReportTitle = "TradeIn Sales Journal Detail For  " & txtDD.Text.Trim & "/" & txtMM.Text.Trim & "/" & txtYYYY.Text.Trim
            dsComp = objcompany.getCompanyName
            If dsComp.Tables("GetCompanyName").Rows.Count > 0 Then
                strCompanyname = Trim(dsComp.Tables("GetCompanyName").Rows(0)("Name") & "")
                strAddress = Trim(dsComp.Tables("GetCompanyName").Rows(0)("Address1") & "") & " " & _
                             Trim(dsComp.Tables("GetCompanyName").Rows(0)("Address2") & "") & " " & _
                             Trim(dsComp.Tables("GetCompanyName").Rows(0)("City") & "")
                sTelephone = "Phone :" & Space(2) & Trim(dsComp.Tables("GetCompanyName").Rows(0)("Phone") & "") & Space(2) & _
                             "Fax :" & Space(2) & Trim(dsComp.Tables("GetCompanyName").Rows(0)("Fax") & "")
            End If
            ' Load EOD
            If UCase(sCompanyShortCode) <> "MJ" Then
                strpath = Application.StartupPath & "\Reports\rptEOD.rpt"
            Else
                strpath = Application.StartupPath & "\Reports\rptEODForMeena.rpt"
            End If
            CR.Load(strpath)
            ReportLogOnToServer(CR)
            CR.Refresh()
            frmrep.ReportViewer.ReportSource = CR
            strselectformula = "{tblEOD.LocationCode} = " & fncFormatSQLText(gthisLocation)
            strselectformula = strselectformula & " and {tblEOD.EODDate}= Date('" & EODDate & "')"

            CR.DataDefinition.FormulaFields("Name").Text = fncFormatSQLText(strCompanyname)
            CR.DataDefinition.FormulaFields("Address").Text = fncFormatSQLText(strAddress)
            CR.DataDefinition.FormulaFields("TelePhone").Text = fncFormatSQLText(sTelephone)

            frmrep.ReportViewer.SelectionFormula = strselectformula
            'If optPrint.Checked = True Then
            '    CR.DataDefinition.RecordSelectionFormula = strselectformula
            '    CR.PrintToPrinter(1, True, 0, 0)
            'ElseIf OptPreview.Checked = True Then
            frmrep.Show()
            frmrep.ReportViewer.Visible = True
            frmrep.ReportViewer.DisplayToolbar = True
            frmrep.ReportViewer.CloseView(False)
            frmrep.WindowState = FormWindowState.Maximized
            'End If
            frmrep = Nothing
            frmrep = New frmReports

            ' load day sales
            If UCase(sCompanyShortCode) = "RJ" Then
                
            Else
                If UCase(sCompanyShortCode) = "AS" Then
                    strpath = Application.StartupPath & "\Reports\rptDaySales_AS.rpt"
                ElseIf UCase(sCompanyShortCode) = "HN" Then
                    strpath = Application.StartupPath & "\Reports\rptDaySales_HN_SG.rpt"
                ElseIf UCase(sCompanyShortCode) = "MJ" Then
                    strpath = Application.StartupPath & "\Reports\rptDaySales_MJ.rpt"
                ElseIf UCase(sCompanyShortCode) = "SSJ" Then
                    strpath = Application.StartupPath & "\Reports\rptDaySales_SSJ.rpt"
                Else
                    strpath = Application.StartupPath & "\Reports\rptDaySales.rpt"
                End If
                CR1.Load(strpath)

                ReportLogOnToServer(CR1)
                CR1.Refresh()
                frmrep1.ReportViewer.ReportSource = CR1
                strselectformula = "Date({tblSalesHeader.InvoiceDate}) = date('" & EODDate & "')"
                If UCase(sCompanyShortCode) <> "HN" Then
                    strselectformula = strselectformula & " and {tblSalesHeader.SalesType} <> 'CRRETN '"
                    strselectformula = strselectformula & " and {tblSalesHeader.SalesType} <> 'CSRETN '"
                End If
                CR1.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(strCompanyname)
                CR1.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(strAddress)
                CR1.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(sTelephone)
                frmrep1.ReportViewer.SelectionFormula = strselectformula
                'CR1.DataDefinition.RecordSelectionFormula = strselectformula
                ' If optPrint.Checked = True Then
                CR1.DataDefinition.RecordSelectionFormula = strselectformula
                CR1.PrintToPrinter(1, True, 0, 0)
                ' ElseIf OptPreview.Checked = True Then
                'frmrep1.Show()
                'frmrep1.ReportViewer.Visible = True
                'frmrep1.ReportViewer.DisplayToolbar = True
                'frmrep1.ReportViewer.CloseView(False)
                'frmrep1.WindowState = FormWindowState.Maximized
                'End If
                frmrep1 = Nothing
                frmrep1 = New frmReports
            End If

            ' Print Flexi

            'strpath = Application.StartupPath & "\Reports\rptFlexiPaymentDetail.rpt"

            'CR1.Load(strpath)

            'ReportLogOnToServer(CR1)
            'CR1.Refresh()
            'frmrep1.ReportViewer.ReportSource = CR1
            'strselectformula = "Date({tblpaymodedetailforflexi.InvoiceDate}) = date('" & EODDate & "')"

            'strselectformula = strselectformula & " and {tblpaymodedetailforflexi.LocationCode} = " & fncFormatSQLText(gthisLocation)
            'strselectformula = strselectformula & " and {tblpaymodedetailforflexi.IsProcessed} = 'Y'"

            'CR1.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(sCompanyname)
            'CR1.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(sAddress)
            'CR1.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText(sReportTitle)
            'CR1.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(sPhone)
            'CR1.DataDefinition.FormulaFields("CompAddress4").Text = fncFormatSQLText(sFax)
            'CR1.DataDefinition.RecordSelectionFormula = sSql
            'frmrep1.ReportViewer.SelectionFormula = strselectformula
            ''CR1.DataDefinition.RecordSelectionFormula = strselectformula
            '' If optPrint.Checked = True Then
            ''CR1.DataDefinition.RecordSelectionFormula = strselectformula
            ''CR1.PrintToPrinter(1, True, 0, 0)
            '' ElseIf OptPreview.Checked = True Then
            'frmrep1.Show()
            'frmrep1.ReportViewer.Visible = True
            'frmrep1.ReportViewer.DisplayToolbar = True
            'frmrep1.ReportViewer.CloseView(False)
            'frmrep1.WindowState = FormWindowState.Maximized
            ''End If
            'frmrep1 = Nothing
            'frmrep1 = New frmReports






        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CR.Close()
            CR.Dispose()
            CR = Nothing
            Me.Cursor.Current = System.Windows.Forms.Cursors.Default
        End Try
    End Sub
    Private Function fncEODProcess(ByVal sEODDate As String) As Boolean
        Dim objEOD As New ClsEOD
        Dim bStatus As Boolean

        Cursor.Current = Cursors.WaitCursor
        objEOD.subOpenEODConnection()
        objEOD.subBeginEODTransaction()
        bStatus = False
        Try
            If Not objEOD.fncEODByCategory(sEODDate) Then
                objEOD.subRollBackEODTransaction()
                Exit Try
            End If
            If UCase(sCompanyShortCode) <> "MJ" Then
                If Not objEOD.fncEODByTray(sEODDate) Then
                    objEOD.subRollBackEODTransaction()
                    Exit Try
                End If
            End If

            If UCase(sCompanyShortCode) = "MJ" Then
                If Not objEOD.fncEODForMeena(sEODDate) Then
                    objEOD.subRollBackEODTransaction()
                    Exit Try
                End If
            Else
                If Not objEOD.fncEOD(sEODDate) Then
                    objEOD.subRollBackEODTransaction()
                    Exit Try
                End If
            End If

            'If Not objEOD.fncDailyStock(sEODDate) Then
            '    objEOD.subRollBackEODTransaction()
            '    Exit Try
            'End If


            objEOD.subCommitEODTransaction()
            bStatus = True
        Catch ex As Exception
            objEOD.subRollBackEODTransaction()
            MessageBox.Show(ex.Message)
        Finally
            objEOD.subCloseEODConnection()
            objEOD = Nothing
            fncEODProcess = bStatus
            Cursor.Current = Cursors.Default
        End Try
    End Function

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        subPrintSettlementInReceipt()
    End Sub
End Class