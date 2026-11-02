Imports System
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Imports System.Text
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms
Imports System.Drawing.Image
Imports System.Runtime.InteropServices
Public Class frmPayOut
    Dim objPayOut As New clsPayOut
    Dim sDescription As String
    Dim CompName$, Address1$, Address2$, Country$, PhoneNo$, FaxNo$
    Shared xDocumentName As String = Application.StartupPath & "\Receipt.txt"


    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)> _
    Structure DOCINFOW
        <MarshalAs(UnmanagedType.LPWStr)> Public pDocName As String
        <MarshalAs(UnmanagedType.LPWStr)> Public pOutputFile As String
        <MarshalAs(UnmanagedType.LPWStr)> Public pDataType As String
    End Structure

    <DllImport("winspool.Drv", EntryPoint:="OpenPrinterW", _
    SetLastError:=True, CharSet:=CharSet.Unicode, _
    ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function OpenPrinter(ByVal src As String, ByRef hPrinter As IntPtr, ByVal pd As Long) As Boolean
    End Function
    <DllImport("winspool.Drv", EntryPoint:="ClosePrinter", _
       SetLastError:=True, CharSet:=CharSet.Unicode, _
       ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function ClosePrinter(ByVal hPrinter As IntPtr) As Boolean
    End Function
    <DllImport("winspool.Drv", EntryPoint:="StartDocPrinterW", _
       SetLastError:=True, CharSet:=CharSet.Unicode, _
       ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function StartDocPrinter(ByVal hPrinter As IntPtr, ByVal level As Int32, ByRef pDI As DOCINFOW) As Boolean
    End Function
    <DllImport("winspool.Drv", EntryPoint:="EndDocPrinter", _
       SetLastError:=True, CharSet:=CharSet.Unicode, _
       ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function EndDocPrinter(ByVal hPrinter As IntPtr) As Boolean
    End Function
    <DllImport("winspool.Drv", EntryPoint:="StartPagePrinter", _
       SetLastError:=True, CharSet:=CharSet.Unicode, _
       ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function StartPagePrinter(ByVal hPrinter As IntPtr) As Boolean
    End Function
    <DllImport("winspool.Drv", EntryPoint:="EndPagePrinter", _
       SetLastError:=True, CharSet:=CharSet.Unicode, _
       ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function EndPagePrinter(ByVal hPrinter As IntPtr) As Boolean
    End Function
    <DllImport("winspool.Drv", EntryPoint:="WritePrinter", _
       SetLastError:=True, CharSet:=CharSet.Unicode, _
       ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function WritePrinter(ByVal hPrinter As IntPtr, ByVal pBytes As IntPtr, ByVal dwCount As Int32, ByRef dwWritten As Int32) As Boolean
    End Function
    Private Sub btnNew_Click(sender As System.Object, e As System.EventArgs) Handles btnNew.Click
        subClearMe()
    End Sub
    Private Sub subClearMe()
        txtPayOutCode.Clear()
        txtPayOutDescription.Clear()
        txtRemarks.Clear()
        txtAmount.Clear()
        sDescription = ""
        txtPayOutNo.Clear()
        txtPayOutCode.ReadOnly = False
        txtPayOutCode.Focus()
    End Sub

    Private Sub btnPayOutSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnPayOutSearch.Click
        Dim frmHelpSearch As New frmHelpSearch(0, 0, "tblPayOutMaster", "L", 4, "Code", "", "Code", "Code", 80, "Description", "Description", 180, "Remarks", "Remarks", 80, "CreateUser", "CreateUser", 80)
        frmHelpSearch.ShowDialog()
        txtPayOutCode.Text = frmHelpSearch.Var1.Trim
        sDescription = frmHelpSearch.Var2.Trim
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        subSave()
    End Sub
    Private Sub subSave()
        Dim ds As DataSet
        Dim objPM As New clsPayMode
        Try
            If subValidateMe() Then

                ds = objPayOut.GetPayOutNo()
                If ds.Tables(0).Rows.Count > 0 Then
                    '   txtSplitInvCode.Text = "SC-" & Trim(gthisLocation) & (ds.Tables(0).Rows(0)("NextScrapInventoryNo"))
                    txtPayOutNo.Text = "PO-" & objPayOut.GetPayOutEntryNo()
                End If

                If Not objPayOut.PayOutDetailSave(txtPayOutNo.Text.Trim, Trim(ServerDate), txtPayOutCode.Text.Trim, Trim(sDescription), txtPayOutDescription.Text.Trim, Val(txtAmount.Text), txtRemarks.Text.Trim) Then
                    MessageBox.Show("Error in Save")
                ElseIf Not objPM.PayModeDetailSave(txtPayOutNo.Text.Trim, Format(Now, "dd/MM/yyyy"), "CASH", -Val(txtAmount.Text), Val(0), "PAYOUT", "PAYOUT") Then
                    MessageBox.Show("Error in Save")
                Else
                    MessageBox.Show("PayOut detail saved successfully.")
                    '   subOpenCashDrawer(gsReceiptPrinterName)
                    subPrintBill(txtPayOutNo.Text.Trim)
                    subClearMe()

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
            If txtPayOutCode.Text.Trim = "" Then
                MessageBox.Show("PayOutCode should not be blank.")
                txtPayOutCode.Focus()
                Exit Function
            End If
            If txtPayOutDescription.Text.Trim = "" Then
                MessageBox.Show("PayOut Description should not be blank.")
                txtPayOutDescription.Focus()
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

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub
    Private Sub subPrintBill(ByVal sInvoiceNo As String)
        Try
            Dim ReportTitle As String
            Dim sSelection As String
            Dim sSalesType$
            Dim frmrep As New frmReports
            Dim CR As New ReportDocument
            Dim j As Integer
            Dim dsCompany As DataSet
            Dim objClsCompany As New clsCompany1
            Dim sSql As String
            Dim sCompanyname, sAddress, sReportTitle, sPath, sPhone, sFax, sSelectFormula, sPrintFDate, sPrintTDate, sFromDate, sToDate As String
            Cursor = Cursors.WaitCursor

            strpath = Application.StartupPath & "\Reports\rptPayOut.rpt"
            CR.Load(strpath)
            If Not ReportLogOnToServer(CR) Then
                Exit Sub
            End If
            ReportLogOnToServer(CR)
            CR.Refresh()
            frmrep.ReportViewer.ReportSource = CR
            sSelection = "{tblPayoutdetail.PayOutNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
            frmrep.ReportViewer.SelectionFormula = sSelection
            CR.DataDefinition.RecordSelectionFormula = sSelection

            dsCompany = objClsCompany.getCompanyName
            If dsCompany.Tables("GetCompanyName").Rows.Count > 0 Then
                sCompanyname = Trim(dsCompany.Tables("GetCompanyName").Rows(0)("Name") & "")
                sAddress = Trim(dsCompany.Tables("GetCompanyName").Rows(0)("Address1") & "") & " " & _
                             Trim(dsCompany.Tables("GetCompanyName").Rows(0)("Address2") & "") & " " & _
                             Trim(dsCompany.Tables("GetCompanyName").Rows(0)("Country") & "")
                sPhone = Trim(dsCompany.Tables("GetCompanyName").Rows(0)("Phone") & "")
                sFax = Trim(dsCompany.Tables("GetCompanyName").Rows(0)("Fax") & "")
            End If


            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(sCompanyname)
            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(sAddress)
            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(sPhone & " " & sFax)
            'CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
            'CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
            'CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
            ''frmrep.Show()
            '   If bPrinter Then CR.PrintToPrinter(1, True, 0, 0)
            ''frmrep.ReportViewer.Visible = True
            ''frmrep.ReportViewer.DisplayToolbar = True
            ''frmrep.ReportViewer.CloseView(False)
            ''frmrep.WindowState = FormWindowState.Maximized
            ''frmrep = Nothing
            ''frmrep = New frmReports
            'CR.PrintToPrinter(3, True, 0, 0)
            'If bReprint Then
            '    CR.PrintToPrinter(1, True, 0, 0)
            'Else
            CR.PrintToPrinter(1, True, 0, 0)
            'End If
            frmrep = Nothing
            frmrep = New frmReports

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

#Region "Open Cash Drawer"
    Public Sub subOpenCashDrawer(ByVal sPrinter As String)
        Dim sWriteStr As String
        Dim ReceiptFileName As String = "/Receipt.txt"
        Try
            sWriteStr = Chr(27) & Chr(112) & Chr(0) & Chr(25) & Chr(250)
            subWriteToFile(sWriteStr, ReceiptFileName)
            SendFileToPrinter(sPrinter, Application.StartupPath & ReceiptFileName)
            subDeleteFile(ReceiptFileName)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub subWriteToFile(ByVal sWriteText As String, ByVal ReceiptFileName As String)
        Dim fp As StreamWriter
        Try
            fp = File.CreateText(Application.StartupPath & ReceiptFileName)

            fp.Write(sWriteText)
            fp.Close()
            Exit Sub
        Catch err As Exception
            MsgBox(err.Message)
        Finally
            fp = Nothing
        End Try
    End Sub
    Public Function SendFileToPrinter(ByVal szPrinterName As String, ByVal szFileName As String) As Boolean
        ' Open the file.
        Dim fs As New FileStream(szFileName, FileMode.Open)
        ' Create a BinaryReader on the file.
        Dim br As New BinaryReader(fs)
        ' Dim an array of bytes large enough to hold the file's contents.
        Dim bytes(fs.Length) As Byte
        Dim bSuccess As Boolean
        ' Your unmanaged pointer
        Dim pUnmanagedBytes As IntPtr
        bytes = br.ReadBytes(fs.Length)
        pUnmanagedBytes = Marshal.AllocCoTaskMem(fs.Length)
        Marshal.Copy(bytes, 0, pUnmanagedBytes, fs.Length)
        ' Send the unmanaged bytes to the printer.
        bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, fs.Length)
        ' Free the unmanaged memory that you allocated earlier.
        Marshal.FreeCoTaskMem(pUnmanagedBytes)
        fs.Close()
        fs = Nothing
        Return bSuccess
    End Function ' SendFileToPrinter()
    Public Shared Function SendBytesToPrinter(ByVal szPrinterName As String, ByVal pBytes As IntPtr, ByVal dwCount As Int32) As Boolean
        Dim hPrinter As IntPtr      ' The printer handle.
        Dim dwError As Int32        ' Last error - in case there was trouble.
        Dim di As DOCINFOW          ' Describes your document (name, port, data type).
        Dim dwWritten As Int32      ' The number of bytes written by WritePrinter().
        Dim bSuccess As Boolean     ' Your success code.

        ' Set up the DOCINFO structure.
        With di
            .pDocName = xDocumentName
            .pDataType = "RAW"
        End With
        'di.pDocName = xDocumentName
        'di.pDataType = "RAW"
        ' Assume failure unless you specifically succeed.
        bSuccess = False
        If OpenPrinter(szPrinterName.Normalize(), hPrinter, IntPtr.Zero) Then
            If StartDocPrinter(hPrinter, 1, di) Then
                If StartPagePrinter(hPrinter) Then
                    ' Write your printer-specific bytes to the printer.
                    bSuccess = WritePrinter(hPrinter, pBytes, dwCount, dwWritten)
                    EndPagePrinter(hPrinter)
                End If
                EndDocPrinter(hPrinter)
            End If
            ClosePrinter(hPrinter)
        End If
        ' If you did not succeed, GetLastError may give more information
        ' about why not.
        If bSuccess = False Then
            dwError = Marshal.GetLastWin32Error()
        End If
        Return bSuccess
    End Function ' SendBytesToPrinter()
    Public Sub subDeleteFile(ByVal ReceiptFileName As String)
        Try
            File.Delete(Application.StartupPath & ReceiptFileName)
            Exit Sub
        Catch err As Exception
            MsgBox(err.Message)
        Finally
        End Try
    End Sub
#End Region

    Private Sub btnPrint_Click(sender As System.Object, e As System.EventArgs) Handles btnPrint.Click
        If txtPayOutNo.Text.Trim <> "" Then
            subPrintBill(txtPayOutNo.Text.Trim)
        End If
    End Sub
End Class