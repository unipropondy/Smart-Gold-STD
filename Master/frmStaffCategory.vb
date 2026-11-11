Public Class frmStaffCategory
    Private Sub btnCatagorySearch_Click(sender As System.Object, e As System.EventArgs) Handles btnCatagorySearch.Click
        Dim frmHelpCategory As New frmHelpSearch(0, 0, "tblCategory", "L", 4, "CategoryCode", "", "CategoryCode", "CategoryCode", 80, "CategoryName", "CategoryName", 180, "ClassCode", "ClassCode", 60, "Remarks", "Remarks", 80)
        frmHelpCategory.ShowDialog()
        txtCategoryCode.Text = frmHelpCategory.Var1.Trim
        txtCategoryName.Text = frmHelpCategory.Var2.Trim
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click

    End Sub
End Class