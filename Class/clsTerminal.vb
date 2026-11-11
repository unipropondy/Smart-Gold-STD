Public Class clsTerminal
    Dim dm As New DataManager
    Public Function fnSaveTerminal(ByVal TerminalCode As String, ByVal TerminalName As String, _
                                  ByVal ComputerName As String, ByVal LocationCode As String, _
                                  ByVal CustDispAttach As String, ByVal CustDispPortNo As String, _
                                  ByVal BarcodeAttach As String, ByVal BarcodeportNo As String, _
                                  ByVal printerAttach As String, ByVal PrinterType As String, _
                                  ByVal CashDrawerAttach As String, ByVal CashDrawerportNo As String, _
                                  ByVal Remarks As String, ByVal VoiceEnable As String, _
                                  ByVal CreateUser As String, _
                                  ByVal Mode As String) As Boolean
        On Error GoTo ErrTrap
        Dim sExec As String
        fnSaveTerminal = False
        sExec = ""
        sExec &= " Sp_Terminal "
        sExec &= fncFormatSQLText(TerminalCode) & "," & fncFormatSQLText(TerminalName) & "," & fncFormatSQLText(LocationCode) & ","
        sExec &= fncFormatSQLText(ComputerName) & "," & 0 & "," & fncFormatSQLText(CustDispAttach) & ","
        sExec &= fncFormatSQLText(CustDispPortNo) & "," & fncFormatSQLText(BarcodeAttach) & ","
        sExec &= fncFormatSQLText(BarcodeportNo) & "," & fncFormatSQLText(printerAttach) & ","
        sExec &= fncFormatSQLText(PrinterType) & "," & fncFormatSQLText(CashDrawerAttach) & ","
        sExec &= fncFormatSQLText(CashDrawerportNo) & "," & fncFormatSQLText(Remarks) & ","
        sExec &= fncFormatSQLText(VoiceEnable) & "," & fncFormatSQLText(sUserID) & "," & fncFormatSQLText(Mode)
        fnSaveTerminal = dm.ProjExecNonQry(sExec)
        Exit Function
ErrTrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Function

    Public Function getlist() As DataRow()
        getlist = dm.RowReturnProjExecNonQryWithDataRow("select * from tblTerminal")
    End Function

#Region "Added By Sarathiy"

    Public Function GetTerminal(ByVal sTerminalCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select * from tblTerminal where TerminalCode = " & fncFormatSQLText(sTerminalCode)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Terminal", "Terminal")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetTerminal = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function GetTerminalByComputer(ByVal sComputerName As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "SELECT * FROM tblTerminal WHERE ComputerName = " & fncFormatSQLText(sComputerName)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Terminal", "Terminal")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetTerminalByComputer = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function


#End Region

End Class
