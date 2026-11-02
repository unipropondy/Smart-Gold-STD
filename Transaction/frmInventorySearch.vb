Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmInventorySearch

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub btnCatagorySearch_Click(sender As System.Object, e As System.EventArgs) Handles btnCatagorySearch.Click
        Try
            Dim frmHelpCategory As New frmHelpSearch(0, 0, "tblCategory", "L", 4, "CategoryCode", "", "CategoryCode", "CategoryCode", 80, "CategoryName", "CategoryName", 180, "ClassCode", "ClassCode", 60, "Remarks", "Remarks", 80)
            frmHelpCategory.ShowDialog()
            txtCategory.Text = frmHelpCategory.Var1.Trim
        Catch ex As Exception
            MessageBox.Show(Err.Description)
        End Try
    End Sub

    Private Sub btnCatalogueSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnCatalogueSearch.Click
        Try
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCatalogue", "L", 4, "CatalogueName", "CategoryCode=" & fncFormatSQLText(txtCategory.Text), "CatalogueCode", "CatalogueCode", 80, "CatalogueName", "CatalogueName", 180, "CategoryCode", "CategoryCode", 60, "GoldLoss", "GoldLoss", 80)
            frmHelpClass.ShowDialog()
            txtCatalogue.Text = frmHelpClass.Var1.Trim
        Catch ex As Exception
            MessageBox.Show(Err.Description)
        End Try
    End Sub
    Private Sub FetchInventoryDetails()
        Dim sSql, sFromLocStatus As String
        Dim iRows, iCount As Integer
        Dim DM As New DataManager
        Dim drH As DataRow()

        sSql = "Select b.Locationcode, a.InventoryCode,a.Description,case a.Weight when 0 then a.StoneWeight else a.Weight End Weight,a.SizeCode,a.SizeType,Case Status when 'IN' then 'IN' else '' END [Status] from tblInventory a,"
        sSql = sSql & " tblInventoryStock  b where a.InventoryCode=b.InventoryCode "
        If Trim(txtCategory.Text) <> "" Then
            sSql = sSql & " and a.CategoryCode=" & fncFormatSQLText(txtCategory.Text)
        End If
        If Trim(txtCatalogue.Text) <> "" Then
            sSql = sSql & " and a.CatalogueCode=" & fncFormatSQLText(txtCatalogue.Text)
        End If
        If Val(txtFromWeight.Text) > 0 And Val(txtToWeight.Text) > 0 Then
            sSql = sSql & " and a.Weight >= " & Val(txtFromWeight.Text)
            sSql = sSql & " and a.Weight <= " & Val(txtToWeight.Text)
        End If
        If Trim(txtInventoryCode.Text) <> "" Then
            sSql = sSql & " and a.InventoryCode=" & fncFormatSQLText(txtInventoryCode.Text)
        End If
        sSql = sSql & " and b.qtyonhand > 0 Order by b.Locationcode"



        drH = DM.RowReturnProjExecNonQryWithDataRowAcc(sSql)
        grdCatalogue.RowCount = 0
        If drH.Length > 0 Then
            '    grdCatalogue.Visible = True
            For iRows = 0 To drH.Length - 1
                grdCatalogue.Rows.Add()
                fnSetdgvText(grdCatalogue, iRows, 0, Trim(drH(iRows)("Locationcode") & ""))
                fnSetdgvText(grdCatalogue, iRows, 1, Trim(drH(iRows)("InventoryCode") & ""))
                fnSetdgvText(grdCatalogue, iRows, 2, Trim(drH(iRows)("Description") & ""))
                fnSetdgvText(grdCatalogue, iRows, 3, Trim(drH(iRows)("Weight") & ""))
                fnSetdgvText(grdCatalogue, iRows, 4, Trim(drH(iRows)("SizeCode") & ""))
                fnSetdgvText(grdCatalogue, iRows, 5, Trim(drH(iRows)("SizeType") & ""))
                fnSetdgvText(grdCatalogue, iRows, 6, Trim(drH(iRows)("Status") & ""))

                'Dim a As String
                'a = Trim(drH(iRows)("Status") & "")
                'If a = "IN" Then
                '    'Call RowColor(grdTran, iRows, 0, grdTran.MaxRows, grdTran.MaxCols, Color.Red)
                '    Call RowColor(grdCatalogue, iRows, 0, iRows, grdCatalogue.ColumnCount, Color.Red)

                'End If
            Next
        Else
            MsgBox("Stock Not Available", MsgBoxStyle.Critical)
        End If
        drH = Nothing
        sSql = Nothing
    End Sub
    Sub RowColor(ByVal grd As Object, ByVal Row1 As Long, ByVal Col1 As Long, ByVal Row2 As Long, ByVal Col2 As Long, ByVal Color As System.Drawing.Color)
        'set the row color in the grid
        grd.Row = Row1
        grd.Col = Col1
        grd.Row2 = Row2
        grd.Col2 = Col2
        grd.BlockMode = True
        grd.ForeColor = Color
        'grd.Backcolor = Color
        grd.BlockMode = False
    End Sub
    Private Sub btnFetch_Click(sender As System.Object, e As System.EventArgs) Handles btnFetch.Click
        FetchInventoryDetails()
    End Sub

    Private Sub btnPrint_Click(sender As System.Object, e As System.EventArgs) Handles btnPrint.Click
        subPrint()
    End Sub
    Private Sub subPrint()
        Dim sSql As String
        Dim sCompanyname, sAddress, sReportTitle, sPath, sPhone, sFax, sSelectFormula, sPrintFDate, sPrintTDate, sFromDate, sToDate As String
        Dim CR As New ReportDocument
        Dim frmRep As New frmReports
        Dim sSelectFld As String
        Dim dsCompany As DataSet
        Dim objClsCompany As New clsCompany1
        Try
            ' If UCase(sCompanyShortCode) = "JH" Then
            '    sPath = Application.StartupPath & "\Reports\rptTransferOutDetail_JH.rpt"
            'Else
            sPath = Application.StartupPath & "\Reports\rptCatalogueSearch.rpt"
            'End If
            sReportTitle = "Transfer Note"
            sSql = " {tblInventory.CategoryCode} = " & fncFormatSQLText(txtCategory.Text)
            sSql = sSql & " and {tblInventory.Cataloguecode} = " & fncFormatSQLText(txtCatalogue.Text)
            If Val(txtFromWeight.Text) > 0 And Val(txtToWeight.Text) > 0 Then
                sSql = sSql & " and {tblInventoryStock.weight} >= " & Val(txtFromWeight.Text)
                sSql = sSql & " and {tblInventoryStock.weight} <= " & Val(txtToWeight.Text)
            End If
            sSql = sSql & " and {tblInventoryStock.Qtyonhand} > 0 "
            dsCompany = objClsCompany.getCompanyName
            If dsCompany.Tables("GetCompanyName").Rows.Count > 0 Then
                sCompanyname = Trim(dsCompany.Tables("GetCompanyName").Rows(0)("Name") & "")
                sAddress = Trim(dsCompany.Tables("GetCompanyName").Rows(0)("Address1") & "") & " " & _
                             Trim(dsCompany.Tables("GetCompanyName").Rows(0)("Address2") & "") & " " & _
                             Trim(dsCompany.Tables("GetCompanyName").Rows(0)("Country") & "")
                sPhone = Trim(dsCompany.Tables("GetCompanyName").Rows(0)("Phone") & "")
                sFax = Trim(dsCompany.Tables("GetCompanyName").Rows(0)("Fax") & "")
            End If
            CR.Load(sPath)
            ReportLogOnToServer(CR)
            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(sCompanyname)
            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(sAddress)
            CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText(sReportTitle)
            CR.DataDefinition.FormulaFields("RPTSELECTION").Text = fncFormatSQLText(sSelectFld)
            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(sPhone)
            CR.DataDefinition.FormulaFields("CompAddress4").Text = fncFormatSQLText(sFax)
            CR.DataDefinition.RecordSelectionFormula = sSql

            CR.Refresh()
            frmRep.ReportViewer.SelectionFormula = sSql
            frmRep.ReportViewer.ReportSource = CR
            frmRep.ShowDialog()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CR.Close()
            CR = Nothing
            frmRep = Nothing
            objClsCompany = Nothing
            dsCompany = Nothing
        End Try
    End Sub

    Private Sub Label2_Click(sender As System.Object, e As System.EventArgs) Handles Label2.Click

    End Sub

    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        txtCategory.Text = ""
        txtCatalogue.Text = ""
        txtFromWeight.Text = ""
        txtToWeight.Text = ""
        txtInventoryCode.Text = ""
        grdCatalogue.RowCount = 0
    End Sub
End Class