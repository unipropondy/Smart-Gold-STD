Imports System.IO
Imports System.Runtime.InteropServices
Imports System
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

<ComClass(clsReceiptToCustomer.ClassId, clsReceiptToCustomer.InterfaceId, clsReceiptToCustomer.EventsId)> _
Public Class clsReceiptToCustomer
    Shared xDocumentName As String = Application.StartupPath & "\Receipt.txt"
#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "cda55763-1e9d-4d7a-ae23-5ac47365bfb1"
    Public Const InterfaceId As String = "4f74376c-c871-4133-8773-f77e24929168"
    Public Const EventsId As String = "dc3cff5d-32d8-4cff-a1e8-b0cb5e0b4d1f"
#End Region
    ' Structure and API declarions:
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


    Dim Dm As New DataManager

    Public Function fnNextReceiptNo() As String
        On Error GoTo errTrap
        Dim NextNo As String
        Dim NextRNo As String
        NextNo = Dm.ValReturnProjExecNonQryAcc("Select NextReciptNo from tblCompanyAR where CompanyID='" & Trim(CompId) & "'")
        If Val(NextNo) <> 0 Then
            NextRNo = Trim(sLocationCode) & "-R" & Format(Val(NextNo + 1), "0000000")
        Else
            NextRNo = Trim(sLocationCode) & "-R0000001"
        End If
        fnNextReceiptNo = NextRNo
        Exit Function
errTrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    Public Function fnSaveHeaderInfo(ByVal ReceiptNo As String, ByVal VoucherRef As String, ByVal ReceiptDate As String, _
                                        ByVal CustomerID As String, ByVal Department As String, ByVal Type As String, ByVal AccountNo As String, _
                                        ByVal Description As String, ByVal DocDate As String, ByVal DocRef As String, ByVal BankName As String, ByVal Branch As String, ByVal Folio As String, ByVal Amount As Double, ByVal TotalAmt As Double, _
                                        ByVal ReceivedUp As Double, ByVal Discount As Double, ByVal Applied As Double, ByVal CurrCode As String, _
                                        ByVal CurrRate As Double, ByVal JournalNo As String, _
                                        ByVal CompanyCode As String, ByVal CreatedBy As String, _
                                        ByVal CreatedDate As String, ByVal ModifiedBy As String, ByVal ModifiedDate As String, ByVal Flag As String) As String
        On Error GoTo Errtrap
        Dim Dm As DataManager
        Dim sExec As String
        sExec = ""
        sExec = "Exec AR_ReceiptCustomerHeader "
        sExec &= fncFormatSQLText(ReceiptNo) & "," & fncFormatSQLText(VoucherRef) & "," & fncFormatSQLDate(ReceiptDate) & "," & fncFormatSQLText(CustomerID) & ","
        sExec &= fncFormatSQLText(Department) & "," & fncFormatSQLText(Type) & "," & fncFormatSQLText(AccountNo) & "," & fncFormatSQLText(Description) & ","
        sExec &= fncFormatSQLDate(DocDate) & "," & fncFormatSQLText(DocRef) & "," & fncFormatSQLText(BankName) & "," & fncFormatSQLText(Branch) & "," & fncFormatSQLText(Folio) & "," & Val(Amount) & "," & Val(TotalAmt) & "," & Val(ReceivedUp) & "," & Val(Discount) & "," & Val(Applied) & ","
        sExec &= fncFormatSQLText(CurrCode) & "," & Val(CurrRate) & "," & fncFormatSQLText(JournalNo) & ","
        sExec &= fncFormatSQLText(CompanyCode) & "," & fncFormatSQLText(CreatedBy) & "," & fncFormatSQLDate(CreatedDate) & "," & fncFormatSQLText(ModifiedBy) & "," & fncFormatSQLDate(ModifiedDate) & "," & fncFormatSQLText(Flag)
        If Flag = "I" Then
            fnSaveHeaderInfo = Dm.ReturnProjExecNonQryAcc(sExec)
        ElseIf Dm.ProjExecNonQry(sExec) = False Then
            Return "F"
        Else
            Return ReceiptNo
        End If
        Exit Function
Errtrap:
        MsgBox(Err.Description & "Error in Save Header Info..", MsgBoxStyle.ApplicationModal)
        Err.Clear()
        Exit Function
    End Function
    Public Function fnAppliedTransSave(ByVal ReceiptNo As String, ByVal SlNo As Long, ByVal CustomerCode As String, _
                                    ByVal InvDate As String, ByVal DocType As String, ByVal RefNo As String, ByVal TranNo As String, _
                                   ByVal TranAmount As Double, ByVal ReceivedUp As Double, ByVal Discount As Double, ByVal Balance As Double, _
                                   ByVal FillFlag As Int16, ByVal PartialFlag As Int16, ByVal NoneFlag As Int16, _
                                   ByVal CreatedBy As String, ByVal CreatedDate As String, ByVal ModifiedBy As String, ByVal ModifiedDate As String, ByVal Flag As String)
        On Error GoTo Errtrap
        Dim Dm As DataManager
        Dim sExec As String
        sExec = ""
        sExec = "Exec AR_ReceiptAppliedTransaction "
        sExec = sExec & fncFormatSQLText(ReceiptNo) & "," & SlNo & "," & fncFormatSQLText(CustomerCode) & "," & fncFormatSQLDate(InvDate) & "," & fncFormatSQLText(DocType) & ","
        sExec = sExec & fncFormatSQLText(RefNo) & "," & fncFormatSQLText(TranNo) & "," & TranAmount & "," & Val(ReceivedUp) & "," & Val(Discount) & "," & Val(Balance) & "," & FillFlag & "," & PartialFlag & "," & NoneFlag & ","
        sExec = sExec & fncFormatSQLText(CreatedBy) & "," & fncFormatSQLDate(CreatedDate) & "," & fncFormatSQLText(ModifiedBy) & "," & fncFormatSQLDate(ModifiedDate) & "," & fncFormatSQLText(Flag)
        fnAppliedTransSave = Dm.ProjExecNonQryAcc(sExec)
        Exit Function
Errtrap:
        MsgBox(Err.Description & " Error in Save Applied Trans Section..", MsgBoxStyle.ApplicationModal)
        Err.Clear()
        Exit Function
    End Function
    Public Function fnDeleteRecord(ByVal ReceiptNo As String, ByVal CustomerID As String) As Boolean
        On Error GoTo Errtrap
        Dim Dm As DataManager
        Dim sExec As String
        sExec = ""
        sExec = "Exec AR_DeleteCustomerReceipt " & fncFormatSQLText(ReceiptNo) & "," & fncFormatSQLText(CustomerID)
        fnDeleteRecord = Dm.ProjExecNonQryAcc(sExec)
        Exit Function
Errtrap:
        MsgBox("Error in Delete Receipt", MsgBoxStyle.ApplicationModal)
        Exit Function
    End Function
    Public Function NextREJournlNo() As String
        On Error GoTo errtrap
        Dim NextNo As String
        Dim NextJNo As String
        NextNo = Dm.ValReturnProjExecNonQryAcc("Select NextReceipt from tblParameters where CompanyID='" & Trim(CompId) & "'")
        If Val(NextNo) <> 0 Then
            NextJNo = "RE-" & Format(Val(NextNo), "00000000")
        Else
            NextJNo = "RE-00000001"
        End If
        NextREJournlNo = NextJNo
        Exit Function
errtrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    Public Function UpdateJournalNo(ByVal ReceiptNo As String, ByVal Jn As String) As Boolean
        On Error GoTo errTrap
        UpdateJournalNo = False
        Dim DM As New DataManager
        Dim sExec As String
        UpdateJournalNo = DM.ProjExecNonQryAcc("Exec AR_UpdateCustomerReceiptJournal " & fncFormatSQLText(ReceiptNo) & "," & fncFormatSQLText(Jn))
        Exit Function
errTrap:

    End Function
    Public Function DeleteAROpenItem(ByVal ReceiptNo As String) As Boolean
        On Error GoTo errtrap
        DeleteAROpenItem = False
        Dim Dm As New DataManager
        DeleteAROpenItem = Dm.ProjExecNonQryAcc("Exec AR_DeleteAROPenItem " & fncFormatSQLText(ReceiptNo))
        Exit Function
errtrap:
        MsgBox(Err.Description & "Error in Delete AR Open Item")
    End Function
    ' Batch 
    Public Function OpenBatchPostinConnection()
        'Dim X As System.Data.SqlClient.SqlDataReader
        gsConBatchPosting.ConnectionString = gsConAcc
        gsConBatchPosting.Open()
        ' Dim Cmd As New SqlClient.SqlCommand("Select * from tblParameters with (TABLOCKX)", gsConBatchPosting)
        ' X = Cmd.ExecuteReader()
    End Function
    Public Function gpBeginTransaction()
        gsTrans = gsConBatchPosting.BeginTransaction
    End Function
    Public Function gpCommitTransaction()
        gsTrans.Commit()
    End Function
    Public Function NonQryBatchPosting(ByVal Query As String) As Boolean
        On Error GoTo ErrTrap
        NonQryBatchPosting = False
        Dim Cmd As New SqlClient.SqlCommand(Query, gsConBatchPosting)
        Cmd.Transaction = gsTrans
        Cmd.ExecuteNonQuery()
        NonQryBatchPosting = True
        Exit Function
ErrTrap:
        NonQryBatchPosting = False
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    Public Function CloseBatchConnection()
        gsConBatchPosting.Close()
    End Function
    Public Function gpRollBackBatchConnection()
        gsTrans.Rollback()
    End Function


    Public Function fnSaveHeaderInfoWithTran(ByVal ReceiptNo As String, ByVal VoucherRef As String, ByVal ReceiptDate As String, _
                                           ByVal CustomerID As String, ByVal Department As String, ByVal Type As String, ByVal AccountNo As String, _
                                           ByVal Description As String, ByVal DocDate As String, ByVal DocRef As String, ByVal BankName As String, ByVal Branch As String, ByVal Folio As String, ByVal Amount As Double, ByVal TotalAmt As Double, _
                                           ByVal ReceivedUp As Double, ByVal Discount As Double, ByVal Applied As Double, ByVal CurrCode As String, _
                                           ByVal CurrRate As Double, ByVal JournalNo As String, _
                                           ByVal CompanyCode As String, ByVal CreatedBy As String, _
                                           ByVal CreatedDate As String, ByVal ModifiedBy As String, ByVal ModifiedDate As String, ByVal Flag As String) As String
        On Error GoTo ErrTrap
        Dim Dm As DataManager
        Dim sExec As String
        sExec = ""
        sExec = "Exec AR_ReceiptCustomerHeader "
        sExec &= fncFormatSQLText(ReceiptNo) & "," & fncFormatSQLText(VoucherRef) & "," & fncFormatSQLDate(ReceiptDate) & "," & fncFormatSQLText(CustomerID) & ","
        sExec &= fncFormatSQLText(Department) & "," & fncFormatSQLText(Type) & "," & fncFormatSQLText(AccountNo) & "," & fncFormatSQLText(Description) & ","
        sExec &= fncFormatSQLDate(DocDate) & "," & fncFormatSQLText(DocRef) & "," & fncFormatSQLText(BankName) & "," & fncFormatSQLText(Branch) & "," & fncFormatSQLText(Folio) & "," & Val(Amount) & "," & Val(TotalAmt) & "," & Val(ReceivedUp) & "," & Val(Discount) & "," & Val(Applied) & ","
        sExec &= fncFormatSQLText(CurrCode) & "," & Val(CurrRate) & "," & fncFormatSQLText(JournalNo) & ","
        sExec &= fncFormatSQLText(CompanyCode) & "," & fncFormatSQLText(CreatedBy) & "," & fncFormatSQLDate(CreatedDate) & "," & fncFormatSQLText(ModifiedBy) & "," & fncFormatSQLDate(ModifiedDate) & "," & fncFormatSQLText(Flag)
        Dim Cmd As New SqlClient.SqlCommand(sExec, gsConBatchPosting)
        Cmd.Transaction = gsTrans
        fnSaveHeaderInfoWithTran = Cmd.ExecuteScalar()
        'fnSaveReceiptOtherHeaderWithTran = True
        Exit Function
ErrTrap:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
        Err.Clear()
    End Function
    Public Function fnAppliedTransSavewithTran(ByVal ReceiptNo As String, ByVal SlNo As Long, ByVal CustomerCode As String, _
                                    ByVal InvDate As String, ByVal DocType As String, ByVal RefNo As String, ByVal TranNo As String, _
                                   ByVal TranAmount As Double, ByVal ReceivedUp As Double, ByVal Discount As Double, ByVal Balance As Double, _
                                   ByVal FillFlag As Int16, ByVal PartialFlag As Int16, ByVal NoneFlag As Int16, _
                                   ByVal CreatedBy As String, ByVal CreatedDate As String, ByVal ModifiedBy As String, ByVal ModifiedDate As String, ByVal Flag As String) As Boolean
        On Error GoTo ErrTrap
        Dim Dm As DataManager
        Dim sExec As String
        sExec = ""
        sExec = "Exec AR_ReceiptAppliedTransaction "
        sExec = sExec & fncFormatSQLText(ReceiptNo) & "," & SlNo & "," & fncFormatSQLText(CustomerCode) & "," & fncFormatSQLDate(InvDate) & "," & fncFormatSQLText(DocType) & ","
        sExec = sExec & fncFormatSQLText(RefNo) & "," & fncFormatSQLText(TranNo) & "," & TranAmount & "," & Val(ReceivedUp) & "," & Val(Discount) & "," & Val(Balance) & "," & FillFlag & "," & PartialFlag & "," & NoneFlag & ","
        sExec = sExec & fncFormatSQLText(CreatedBy) & "," & fncFormatSQLDate(CreatedDate) & "," & fncFormatSQLText(ModifiedBy) & "," & fncFormatSQLDate(ModifiedDate) & "," & fncFormatSQLText(Flag)

        Dim Cmd As New SqlClient.SqlCommand(sExec, gsConBatchPosting)
        Cmd.Transaction = gsTrans
        Cmd.ExecuteNonQuery()
        fnAppliedTransSavewithTran = True
        Exit Function
ErrTrap:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
        fnAppliedTransSavewithTran = False
        Err.Clear()
    End Function
    Public Function fnDeleteRecordWithTran(ByVal ReceiptNo As String, ByVal CustomerID As String) As Boolean
        On Error GoTo Errtrap
        Dim sExec As String
        fnDeleteRecordWithTran = False
        sExec = ""
        sExec = "Exec AR_DeleteCustomerReceipt " & fncFormatSQLText(ReceiptNo) & "," & fncFormatSQLText(CustomerID)
        Dim Cmd As New SqlClient.SqlCommand(sExec, gsConBatchPosting)
        Cmd.Transaction = gsTrans
        Cmd.ExecuteNonQuery()
        fnDeleteRecordWithTran = True
        Exit Function
Errtrap:
        fnDeleteRecordWithTran = False
    End Function

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
    End Function
    Public Sub subDeleteFile(ByVal ReceiptFileName As String)
        Try
            File.Delete(Application.StartupPath & ReceiptFileName)
            Exit Sub
        Catch err As Exception
            MsgBox(err.Message)
        Finally
        End Try
    End Sub

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
    End Function



End Class


