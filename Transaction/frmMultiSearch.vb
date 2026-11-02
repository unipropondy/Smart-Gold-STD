Public Class frmMultiSearch
    Public Const CSearchLocation = "Location"
    Dim iRow As Integer
    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        objGeneralSelect.sSelectedValue = ""
        objGeneralSelect.sSelectedDescription = ""
        Me.Dispose()
    End Sub

    Private Sub frmMultiSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'setForm(Me)
        Me.Text = objGeneralSelect.sFormCaption
        subLoadDetails()
    End Sub
    Private Sub subLoadDetails()
        Dim sSelect As String = ""
        Dim sCode As String

        Dim sFlexiLocation As String
        sFlexiLocation = Trim(gthisLocation)
        If Trim(sFlexiLocation) = "99" Then
            sFlexiLocation = "103"
        ElseIf Trim(sLocationCode) = "27" Then
            sFlexiLocation = "1"
        End If
        Try
            lstSearch.Items.Clear()
            Select Case objGeneralSelect.sMode
                Case CSearchLocation
                    sSelect = "Select SubscriptionID , CustomerName from vwFlexiFinalClosure  where IsProcessed='N' and locationcode=" & fncFormatSQLText(sFlexiLocation) & " order by SubscriptionID "
            End Select
            Dim dsSearch As DataSet
            If sSelect <> "" Then
                dsSearch = fncGetDatasetWithQuery(gsConAcc, "Search", sSelect)
                For iRow = 0 To dsSearch.Tables("Search").Rows.Count - 1
                    sCode = Trim(dsSearch.Tables("Search").Rows(iRow).Item(0)) & " , " & Trim(dsSearch.Tables("Search").Rows(iRow).Item(1))
                    lstSearch.Items.Add(sCode)
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

            Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Dim sArray() As String
        Dim sCode As String
        Dim sOutput As String
        Dim ItemChecked As Object
        Try
            sOutput = ""
            For Each ItemChecked In lstSearch.CheckedItems

                ' Use the IndexOf method to get the index of an item.   
                'MessageBox.Show("Item with title: " + "" + itemChecked.ToString() + "" + _
                '                ", is checked. Checked state is: " + _
                '                lstSearch.GetItemCheckState(lstSearch.Items.IndexOf(itemChecked)).ToString() + ".")

                sArray = Split(itemChecked.ToString(), ",")
                sCode = sArray(0)
                If sOutput = "" Then
                    sOutput = sCode
                Else
                    sOutput = sOutput & " , " & sCode
                End If
            Next

            
            If sOutput <> "" Then
                objGeneralSelect.sSelectedValue = sOutput
                objGeneralSelect.sSelectedDescription = ""
            Else
                objGeneralSelect.sSelectedValue = ""
                objGeneralSelect.sSelectedDescription = ""
            End If
            Me.Dispose()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub chkSelect_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSelect.CheckedChanged
        Try
            If chkSelect.Checked = True Then
                chkSelect.Text = "UnSelect All"
                For iRow = 0 To lstSearch.Items.Count - 1
                    lstSearch.SetItemChecked(iRow, True)
                Next
            Else
                chkSelect.Text = "Select All"
                For iRow = 0 To lstSearch.Items.Count - 1
                    lstSearch.SetItemChecked(iRow, False)
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class