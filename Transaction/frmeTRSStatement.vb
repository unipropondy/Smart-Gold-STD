Public Class frmeTRSStatement

    Public Sub txtBillNo_LostFocus(sender As Object, e As System.EventArgs) Handles txtBillNo.LostFocus
        Dim objSalesRet As New ClsSalesReturn
        Dim Ds As DataSet
        Try
            If txtBillNo.Text.Trim <> "" Then

                subGetSalesDetail()
                txtDocId.Focus()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Public Sub subGetSalesDetail()

        Dim i As Long
        Dim j As Long
        Dim ODs As DataSet
        Dim tempInvCode As String
        Dim objSales As New ClsSalesReturn
        Dim objCustomer As New clsCustomer
        Dim Ds As DataSet
        Dim DsHdr As DataSet
        Dim DsCust As DataSet
        Dim DsMember As DataSet


        Try
            
            DsHdr = objSales.GetSalesHeaderByInvoiceNo(txtBillNo.Text.Trim)
            If DsHdr.Tables("Invoice").Rows.Count > 0 Then
                

                'txtCustomerCode.Text = Trim(DsHdr.Tables("Invoice").Rows(0)("CustomerCode") & "")
                'txtMemberCode.Text = Trim(DsHdr.Tables("Invoice").Rows(0)("MemberCode") & "")
                ''HAJ change for Date format, on 23 Oct 2007
                'txtBDD.Text = Mid(DsHdr.Tables("Invoice").Rows(0)("BDate"), 7, 2)
                'txtBMM.Text = Mid(DsHdr.Tables("Invoice").Rows(0)("BDate"), 5, 2)
                'txtBYYYY.Text = Mid(DsHdr.Tables("Invoice").Rows(0)("BDate"), 1, 4)
                txtBDD.Text = Mid(DsHdr.Tables("Invoice").Rows(0)("BDate"), 1, 2)
                txtBMM.Text = Mid(DsHdr.Tables("Invoice").Rows(0)("BDate"), 4, 2)
                txtBYYYY.Text = Mid(DsHdr.Tables("Invoice").Rows(0)("BDate"), 7, 4)
                If Trim(DsHdr.Tables("Invoice").Rows(0)("SalesType") & "") = "INSTALLMENT" Then
                    txtNetAmount.Text = Val(DsHdr.Tables("Invoice").Rows(0)("NetTotal")) - Val(DsHdr.Tables("Invoice").Rows(0)("TRadeInAMt"))
                Else
                    txtNetAmount.Text = Val(DsHdr.Tables("Invoice").Rows(0)("NetTotal"))
                End If
                txtGST.Text = Val(DsHdr.Tables("Invoice").Rows(0)("Gst"))
                txtPassportNo.Text = Trim(DsHdr.Tables("Invoice").Rows(0)("Remarks") & "")
                'txtSalesMan.Text = Trim(DsHdr.Tables("Invoice").Rows(0)("SalesmanCode") & "")
                dtDate.Text = Trim(DsHdr.Tables("Invoice").Rows(0)("BDate") & "")




            End If



        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub btnBillClear_Click(sender As System.Object, e As System.EventArgs) Handles btnBillClear.Click
        txtBillNo.Text = ""
        txtBDD.Text = ""
        txtBMM.Text = ""
        txtBYYYY.Text = ""
        txtSalesMan.Text = ""
        cboSalesman.Text = ""
        txtPassportNo.Text = ""
        txtGST.Text = ""
        txtNetAmount.Text = ""
        txtDocId.Text = ""
        txtRemarks.Text = ""
        chkAbove7000.Checked = False
        chkSealedBag.Checked = False
        txtSealedBag.Text = ""
        txteTRSStaff.Text = ""
        txtStaffSign.Text = ""
        picImage.Name = ""
        picImage.Image = Nothing
        optSales7000No.Checked = True
        optSealedBagNo.Checked = True
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        Dim AccType As String
        Dim BillDate As String
        Dim sGender As String
        Dim DOA As String
        Dim ByteArr() As Byte
        Dim objSales As New ClsSales
        Dim objSR As New ClsSalesReturn

        Try
            If ValidateMe() Then

                ReDim ByteArr(0)
                Dim ImageStream As New IO.MemoryStream
                If Not picImage.Image Is Nothing Then
                    picImage.Image.Save(ImageStream, System.Drawing.Imaging.ImageFormat.Jpeg)
                    ReDim ByteArr(CInt(ImageStream.Length - 1))
                    ImageStream.Position = 0
                    ImageStream.Read(ByteArr, 0, CInt(ImageStream.Length))
                Else
                    ReDim ByteArr(0)
                End If

                BillDate = txtBDD.Text.Trim & "/" & txtBMM.Text.Trim & "/" & txtBYYYY.Text.Trim


                If objSales.eTRSStatementSave(txtBillNo.Text.Trim, BillDate, cboSalesman.Text.Trim, txtPassportNo.Text.Trim, Val(txtGST.Text), Val(txtNetAmount.Text), _
                  txtDocId.Text.Trim, dtDate.Text, dtTime.Text, txtRemarks.Text.Trim, IIf(optSales7000Yes.Checked, "Y", "N"), IIf(optSealedBagYes.Checked, "Y", "N"), txtSealedBag.Text.Trim, txteTRSStaff.Text.Trim, txtStaffSign.Text.Trim) Then



                    gsConBatchPosting.ConnectionString = gsCon
                    gsConBatchPosting.Open()
                    gsTrans = gsConBatchPosting.BeginTransaction

                    If objSR.eTRSStatementImageSave(txtBillNo.Text.Trim, _
                   ByteArr, picImage.Name, sUserID) Then
                        gsTrans.Commit()
                    Else
                        MsgBox("Transaction not saved", MsgBoxStyle.Information)
                        'TransactionRollBack(oTconn)
                        gsTrans.Rollback()
                        Exit Sub
                    End If

                    gsConBatchPosting.Close()

                    MessageBox.Show("eTRS Statement saved sucssfully.")
                    btnBillClear_Click(sender, e)
                    txtBillNo.Focus()
                Else
                    MessageBox.Show("Error in Save")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function ValidateMe() As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            If txtBillNo.Text.Trim = "" Then
                MessageBox.Show("BillN0 should not be blank.")
                txtBillNo.Focus()
                Exit Function
            End If
            If txtPassportNo.Text.Trim = "" Then
                MessageBox.Show("PassportNo should not be blank.")
                txtPassportNo.Focus()
                Exit Function
            End If
            If txtDocId.Text.Trim = "" Then
                MessageBox.Show("DocId should not be blank.")
                txtDocId.Focus()
                Exit Function
            End If
            'If Len(txtDocId.Text.Trim) <> 19 Then
            '    MessageBox.Show("DocId should be 19 Characters")
            '    txtDocId.Focus()
            '    Exit Function
            'End If
            If cboSalesman.Text.Trim = "" Then
                MessageBox.Show("Salesman should not be blank.")
                cboSalesman.Focus()
                Exit Function
            End If
            If picImage.Image Is Nothing Then
                MessageBox.Show("Customer Image Should not be Empty")
                Exit Function
            End If
            If optSealedBagYes.Checked Then
                If txtSealedBag.Text = "" Then
                    MessageBox.Show("Sealed Bag Serial Number Should be not Empty")
                    txtSealedBag.Focus()
                    Exit Function
                End If
            End If
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            bStatus = False
        Finally
            ValidateMe = bStatus
        End Try

    End Function

    Private Sub frmeTRSStatement_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F6
                btnBillClear_Click(sender, e)
            Case Keys.F7
                btnSave_Click(sender, e)
            Case Keys.Escape
                btnClose_Click(sender, e)
            Case Keys.Up
                SendKeys.Send("+{Tab}")
            Case Keys.Down
                SendKeys.Send("{Tab}")
            Case Keys.Enter
                SendKeys.Send("{Tab}")
        End Select
    End Sub

    Private Sub frmeTRSStatement_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Trim(gthisLocation) = "99" Then
            cboSalesman.Items.Add("GANESH")
            cboSalesman.Items.Add("ARAVIND")
            cboSalesman.Items.Add("ARUN")
        ElseIf Trim(gthisLocation) = "85" Then
            'cboSalesman.Items.Add("M.SARAVANAN")
            'cboSalesman.Items.Add("SIMON")
            'cboSalesman.Items.Add("A.CHELLADURAI")
            'cboSalesman.Items.Add("SAMPATHKUMAR")
            'cboSalesman.Items.Add("MUTHIAH")
            'cboSalesman.Items.Add("MA.SARAVANAN")
            'cboSalesman.Items.Add("JOY")
            cboSalesman.Items.Add("PANG WEI KOON")
            cboSalesman.Items.Add("CHELLAMUTHU GANESAN")
            cboSalesman.Items.Add("RAJAGOPAL MANGALESWARAN")
        ElseIf Trim(gthisLocation) = "69" Then
            cboSalesman.Items.Add("PANG WEI KOON")
            cboSalesman.Items.Add("CHELLAMUTHU GANESAN")
            cboSalesman.Items.Add("RAJAGOPAL MANGALESWARAN")
        End If
        optSales7000Yes_CheckedChanged(sender, e)
    End Sub

    Private Sub Label16_Click(sender As System.Object, e As System.EventArgs) Handles Label16.Click

    End Sub

    Private Sub Label19_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub TextBox3_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtStaffSign.TextChanged

    End Sub

    Private Sub btnBrowseImage_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowseImage.Click
        Dim OpenFileDialog1 As New OpenFileDialog


        With OpenFileDialog1
            .CheckFileExists = True
            .ShowReadOnly = False
            ' .Filter = "All Files|*.*|Bitmap Files (*)|*.bmp;*.gif;*.jpg"
            .Filter = "All Files|*.*|JPEG Files (*)|*.jpg"
            .FilterIndex = 2

            If .ShowDialog = DialogResult.OK Then
                ' Load the specified file into a PictureBox control.

                picImage.Image = Image.FromFile(.FileName)
                AutosizeImage(.FileName, picImage)
                picImage.Name = .SafeFileName
            End If
        End With
    End Sub

    Public Sub AutosizeImage(ByVal ImagePath As String, ByVal picBox As PictureBox, Optional ByVal pSizeMode As PictureBoxSizeMode = PictureBoxSizeMode.CenterImage)
        Try
            picBox.Image = Nothing
            picBox.SizeMode = pSizeMode
            If System.IO.File.Exists(ImagePath) Then
                Dim imgOrg As Bitmap
                Dim imgShow As Bitmap
                Dim g As Graphics
                Dim divideBy, divideByH, divideByW As Double
                imgOrg = DirectCast(Bitmap.FromFile(ImagePath), Bitmap)

                divideByW = imgOrg.Width / picBox.Width
                divideByH = imgOrg.Height / picBox.Height
                If divideByW > 1 Or divideByH > 1 Then
                    If divideByW > divideByH Then
                        divideBy = divideByW
                    Else
                        divideBy = divideByH
                    End If

                    imgShow = New Bitmap(CInt(CDbl(imgOrg.Width) / divideBy), CInt(CDbl(imgOrg.Height) / divideBy))
                    imgShow.SetResolution(imgOrg.HorizontalResolution, imgOrg.VerticalResolution)
                    g = Graphics.FromImage(imgShow)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    g.DrawImage(imgOrg, New Rectangle(0, 0, CInt(CDbl(imgOrg.Width) / divideBy), CInt(CDbl(imgOrg.Height) / divideBy)), 0, 0, imgOrg.Width, imgOrg.Height, GraphicsUnit.Pixel)
                    g.Dispose()
                Else
                    imgShow = New Bitmap(imgOrg.Width, imgOrg.Height)
                    imgShow.SetResolution(imgOrg.HorizontalResolution, imgOrg.VerticalResolution)
                    g = Graphics.FromImage(imgShow)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    g.DrawImage(imgOrg, New Rectangle(0, 0, imgOrg.Width, imgOrg.Height), 0, 0, imgOrg.Width, imgOrg.Height, GraphicsUnit.Pixel)
                    g.Dispose()
                End If
                imgOrg.Dispose()

                picBox.Image = imgShow

            Else
                picBox.Image = Nothing
            End If


        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try


    End Sub

    Private Sub Panel1_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub optSealedBagYes_CheckedChanged(sender As Object, e As System.EventArgs) Handles optSealedBagYes.CheckedChanged
        If optSealedBagYes.Checked Then
            txtSealedBag.Enabled = True
        Else
            txtSealedBag.Enabled = False
        End If
    End Sub

    Private Sub optSealedBagNo_CheckedChanged(sender As Object, e As System.EventArgs) Handles optSealedBagNo.CheckedChanged
        If optSealedBagNo.Checked Then
            txtSealedBag.Enabled = False
        Else
            txtSealedBag.Enabled = True
        End If
    End Sub

    Private Sub optSales7000Yes_CheckedChanged(sender As Object, e As System.EventArgs) Handles optSales7000Yes.CheckedChanged
        If optSales7000Yes.Checked = True Then
            optSealedBagYes.Enabled = False
            optSealedBagNo.Enabled = False
            txtSealedBag.Enabled = True
            optSealedBagYes.Checked = True
        Else
            optSealedBagYes.Enabled = False
            optSealedBagNo.Enabled = False
            txtSealedBag.Enabled = False
            optSealedBagNo.Checked = True
        End If
    End Sub

    Private Sub optSales7000No_CheckedChanged(sender As Object, e As System.EventArgs) Handles optSales7000No.CheckedChanged
        If optSales7000No.Checked = True Then
            optSealedBagYes.Enabled = False
            optSealedBagNo.Enabled = False
            txtSealedBag.Enabled = False
            optSealedBagNo.Checked = True
        Else
            optSealedBagYes.Enabled = False
            optSealedBagNo.Enabled = False
            txtSealedBag.Enabled = True
            optSealedBagYes.Checked = True
        End If
    End Sub
End Class