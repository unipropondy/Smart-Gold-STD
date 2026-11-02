Public Class frmSupplierMCPerc
    Dim sSupplierCode As String
    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        Dim sinventorycode As String
        Try
            gsConBatchPosting.ConnectionString = gsCon
            gsConBatchPosting.Open()
            gsTrans = gsConBatchPosting.BeginTransaction
            If fncInventory() Then
                gsTrans.Commit()
                MsgBox("Transaction Saved Successfully")
            Else
                MsgBox("Transaction not saved")
                gsTrans.Rollback()

            End If
            gsConBatchPosting.Close()
            btnClear_Click(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            gsConBatchPosting.Close()
        End Try
    End Sub
    Private Function fncInventory() As Boolean
        Dim sPDate As String
        Dim Ds As DataSet
        Dim objInventory As New clsInventory
        Dim objpurchase As New clsPurchase
        Dim objDirectPurchase As New clsDirectPurchase
        Dim sError As String
        Dim bInventory As Boolean
        Dim bPurchase As Boolean
        Dim bPurchaseBill As Boolean
        Dim bJobsheet As Boolean
        Dim i As Long
        Dim sError1 As String
        Dim sError2 As String
        Dim sError3 As String
        Dim sPMCType As String
        Dim sSMCType As String
        Dim sPayType As String
        Dim bNewJobSheet As Boolean
        Dim sType As String
        Try

            bInventory = False

            If optWeight.Checked Then
                sType = "W"
            ElseIf optPCS.Checked Then
                sType = "P"
            End If

            bPurchaseBill = objpurchase.SupplierMCPercSave(txtSupplierCode.Text.Trim, txtCatalogueCode.Text.Trim, Trim(sType), Val(txtAmount.Text), _
                             Val(txtFromWeight.Text), Val(txtToWeight.Text), sUserID)
            If Not bPurchaseBill Then
                MsgBox("Error in Saving Supplier MC Percentage", MsgBoxStyle.Critical)
                fncInventory = False
                Exit Function
            End If

            fncInventory = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Function

    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        txtSupplierCode.Text = ""
        txtSupplierName.Text = ""
        txtCatalogueCode.Text = ""
        txtCatalogueName.Text = ""
        txtFromWeight.Text = ""
        txtToWeight.Text = ""
        optWeight.Checked = True
        txtAmount.Text = ""
    End Sub


    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub btnSupplierCodeSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSupplierCodeSearch.Click
        Try
            Dim e1 As New System.EventArgs
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSupplier", "L", 4, "SupplierName", "", "SupplierCode", "SupplierCode", 80, "SupplierName", "SupplierName", 180, "Address1", "Address1", 60, "Address2", "Address2", 80)
            frmHelpClass.ShowDialog()
            txtSupplierCode.Text = frmHelpClass.Var1.Trim
            txtSupplierName.Text = frmHelpClass.Var2.Trim
            txtSupplierCode_LostFocus(sender, e1)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtSupplierCode_LostFocus(sender As Object, e As System.EventArgs) Handles txtSupplierCode.LostFocus
        Try
            sSupplierCode = Trim(txtSupplierCode.Text)
            subGetSupplier(sender, e)
            
            Exit Sub
        Catch ex As Exception
        End Try
    End Sub
    Private Sub subGetSupplier(ByVal sender As Object, ByVal e As System.EventArgs)
        Try


            Dim sSql As String
            Dim DR As DataRow()
            If Trim(sSupplierCode) <> "" Then
                sSql = "Select * from tblSupplier where SupplierCode=" & fncFormatSQLText(sSupplierCode)
                DR = DataManager.RowReturnProjExecNonQryWithDataRow(sSql)
                If DR.Length > 0 Then
                    txtSupplierName.Text = Trim(DR(0)("SupplierName") & "")
                Else
                    MsgBox("Invalid supplier code!", vbCritical)
                    txtSupplierCode.Text = ""
                    txtSupplierName.Text = ""
                    sSupplierCode = ""
                End If
            Else
                txtSupplierCode.Text = ""
                txtSupplierName.Text = ""
                sSupplierCode = ""
            End If

            Exit Sub
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnCatalogueCode_Click(sender As Object, e As System.EventArgs) Handles btnCatalogueCode.Click
        Dim e1 As New System.EventArgs
        Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCatalogue", "L", 4, "CatalogueName", "", "CatalogueCode", "CatalogueCode", 80, "CatalogueName", "CatalogueName", 180, "CategoryCode", "CategoryCode", 60, "GoldLoss", "GoldLoss", 80)
        frmHelpClass.ShowDialog()
        txtCatalogueCode.Text = frmHelpClass.Var1.Trim
        txtCatalogueCode_LostFocus(sender, e1)
    End Sub

    Private Sub txtCatalogueCode_LostFocus(sender As Object, e As System.EventArgs) Handles txtCatalogueCode.LostFocus
        Try
            If txtCatalogueCode.Text.Trim <> "" Then
                subGetCatalogue(txtCatalogueCode.Text.Trim)

            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subGetCatalogue(ByVal sCatalogueCode As String)
        Try
            Dim Dr As DataRow()
            Dim sSql As String
            sSql = "select a.Cataloguecode,a.CatalogueName,A.Categorycode,b.ClassCode from  tblcatalogue A ,tblcategory B"
            sSql = sSql & " where a.categorycode=b.Categorycode and a.Cataloguecode=" & fncFormatSQLText(sCatalogueCode)
            Dr = DataManager.RowReturnProjExecNonQryWithDataRow(sSql)
            If Dr.Length > 0 Then
                txtCatalogueName.Text = Trim(Dr(0)("CatalogueName") & "")
                txtCatalogueCode.Text = Trim(Dr(0)("Cataloguecode") & "")

            Else
                MsgBox("Invalid CatalogueCode", MsgBoxStyle.Critical)
                'subClearDetail()
                txtCatalogueCode.Focus()
            End If
        Catch ex As Exception

        End Try
    End Sub

End Class