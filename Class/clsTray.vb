Public Class clsTray
    Dim dm As New DataManager

    Public Function TraySave(ByVal TrayID As String, ByVal TrayName As String, ByVal SalesmanID As String, ByVal SalesmanName As String, ByVal IsCustomerTray As String, ByVal sTrayGroup As String) As Boolean
        Dim SExec As String
        Try
            TraySave = False
            SExec = "BEGIN TRANSACTION" & vbCrLf
            SExec &= "Exec Sp_TrayMaster"

            SExec &= fncFormatSQLText(TrayID) & "," & fncFormatSQLText(TrayName) & "," & _
                     fncFormatSQLText(SalesmanID) & "," & fncFormatSQLText(SalesmanName) & "," & _
                     fncFormatSQLText(IsCustomerTray) & "," & fncFormatSQLText(sTrayGroup) & "," & fncFormatSQLText(sUserID) & vbCrLf

            SExec = SExec & "COMMIT TRANSACTION" & vbCrLf
            TraySave = dm.ProjExecNonQry(SExec)
            Exit Function
        Catch ex As Exception
            TraySave = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function



    Public Function TrayDelete(ByVal TrayID As String) As Boolean

        Dim sExec As String
        Try
            TrayDelete = False
            sExec = sExec & "DELETE FROM tblTrayMaster WHERE TrayID=" & fncFormatSQLText(TrayID) & vbCrLf
            dm.ProjExecNonQry(sExec)
            TrayDelete = True
            Exit Function
        Catch ex As Exception
            TrayDelete = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function
    Public Function GetTray() As DataSet
        Dim Ds As New DataSet
        Dim sExec As String
        Try
            sExec = " select TrayID, TrayName, SalesmanID, SalesmanName, IsCustomerTray,TrayGroup from tblTrayMaster "
            Ds = dm.getDataSetWithSqlAdapter(sExec, "Tray", "Tray")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetTray = Ds
            Ds = Nothing
            sExec = Nothing
        End Try
    End Function

#Region "Added by Sarathy"

    Public Function TrayTransfer(ByVal sTrayID As String, ByVal sInventoryCode As String, ByVal sOldTrayID As String, ByVal sRemarks As String) As Boolean
        Dim sQry As String
        Dim bTrayTransfer As Boolean
        Try
            bTrayTransfer = False
            sQry = " Exec sp_TrayTransfer " & fncFormatSQLText(sInventoryCode) & "," & fncFormatSQLText(sTrayID) & "," & fncFormatSQLText(sOldTrayID) & "," & fncFormatSQLText(sRemarks) & "," & fncFormatSQLText(sUserID)
            dm.ProjExecNonQry(sQry)
            bTrayTransfer = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            TrayTransfer = bTrayTransfer
        End Try
    End Function
    Public Function CategoryUpdate(ByVal sInventoryCode As String, ByVal sCategorycode As String) As Boolean
        Dim sQry As String
        Dim bTrayTransfer As Boolean
        Try
            bTrayTransfer = False
            sQry = " Exec sp_CategoryUpdate " & fncFormatSQLText(sInventoryCode) & "," & fncFormatSQLText(sCategorycode) & "," & fncFormatSQLText(sUserID)
            dm.ProjExecNonQry(sQry)
            bTrayTransfer = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CategoryUpdate = bTrayTransfer
        End Try
    End Function
    Public Function RFIDItemStatusUpdate(ByVal sTrayID As String, ByVal sInventorycode As String) As Boolean
        Dim bStatus As Boolean
        Dim sQry As String
        Try
            sQry = "update Item set TrayId=" & fncFormatSQLText(sTrayID) & " Where ItemCode=" & fncFormatSQLText(sInventorycode)
            bStatus = ProjExecNonQryRFID(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            RFIDItemStatusUpdate = bStatus
            sQry = Nothing
        End Try
    End Function
    Public Function IsTrayContainsItems(ByVal sTrayID As String) As Boolean
        Dim sQry As String
        Dim DS As New DataSet
        Dim bStatus As Boolean
        Try
            sQry = " select * from tblinventory where qtyonhand <> 0 and trayid =  " & fncFormatSQLText(sTrayID)
            DS = dm.getDataSetWithSqlAdapter(sQry, "TrayInv", "TrayInv")
            If DS.Tables(0).Rows.Count > 0 Then
                bStatus = True
            Else
                bStatus = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            IsTrayContainsItems = bStatus
            DS = Nothing
        End Try
    End Function
    Public Shared Function ProjExecNonQryRFID(ByVal Query As String) As Boolean
        ProjExecNonQryRFID = True
        Try
            Dim Cn As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(gsConRFID)
            Cn.Open()
            Dim Cmd As New SqlClient.SqlCommand(Query)
            Cmd.Connection = Cn
            Cmd.CommandTimeout = 600
            Cmd.ExecuteNonQuery()
            Cn.Close()
            Cn.Dispose()
            Cn = Nothing
        Catch ex As Exception
            Try
                MsgBox("Error in Exec Record")
                ProjExecNonQryRFID = False
            Catch
            End Try
            Throw
            MsgBox("unexpected error")
            ProjExecNonQryRFID = False
        End Try
    End Function

#End Region

#Region "Added by Murugan"

    Public Function GetTraywithCode(ByVal sTrayID As String) As DataSet
        Dim sQry As String
        Dim DS As New DataSet
        Try

            sQry = " select * from tbltraymaster where trayid =  " & fncFormatSQLText(sTrayID)
            DS = dm.getDataSetWithSqlAdapter(sQry, "Tray", "Tray")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            GetTraywithCode = DS
            DS = Nothing
        End Try
    End Function

#End Region



End Class
