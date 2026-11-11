Public Class frmMemberType
    Inherits System.Windows.Forms.Form

    Const MemberType = 1
    Const MemberTypeDesc = 2
    Const MemberTypeDiscount = 3
    Const Active = 4

    Dim objMember As New clsMember
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents grdMember As AxFPSpread.AxvaSpread
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents txtdiscount As System.Windows.Forms.TextBox
    Friend WithEvents txtMemberTypeDesc As System.Windows.Forms.TextBox
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtMemberType As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmMemberType))
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.grdMember = New AxFPSpread.AxvaSpread
        Me.btnNew = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtMemberType = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.chkActive = New System.Windows.Forms.CheckBox
        Me.txtdiscount = New System.Windows.Forms.TextBox
        Me.txtMemberTypeDesc = New System.Windows.Forms.TextBox
        Me.btnDelete = New System.Windows.Forms.Button
        Me.pic = New System.Windows.Forms.PictureBox
        CType(Me.grdMember, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(704, 304)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "Close(Esc)"
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.Location = New System.Drawing.Point(544, 304)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(76, 23)
        Me.btnSave.TabIndex = 4
        Me.btnSave.Text = "Save(F7)"
        '
        'grdMember
        '
        Me.grdMember.Location = New System.Drawing.Point(10, 88)
        Me.grdMember.Name = "grdMember"
        Me.grdMember.OcxState = CType(resources.GetObject("grdMember.OcxState"), System.Windows.Forms.AxHost.State)
        Me.grdMember.Size = New System.Drawing.Size(398, 240)
        Me.grdMember.TabIndex = 41
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(464, 304)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.TabIndex = 6
        Me.btnNew.Text = "New(F6)"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txtMemberType)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.chkActive)
        Me.Panel1.Controls.Add(Me.txtdiscount)
        Me.Panel1.Controls.Add(Me.txtMemberTypeDesc)
        Me.Panel1.Location = New System.Drawing.Point(424, 88)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(355, 120)
        Me.Panel1.TabIndex = 1
        '
        'txtMemberType
        '
        Me.txtMemberType.BackColor = System.Drawing.Color.White
        Me.txtMemberType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMemberType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMemberType.Location = New System.Drawing.Point(136, 16)
        Me.txtMemberType.Name = "txtMemberType"
        Me.txtMemberType.Size = New System.Drawing.Size(144, 20)
        Me.txtMemberType.TabIndex = 0
        Me.txtMemberType.Text = ""
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(16, 64)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(96, 23)
        Me.Label4.TabIndex = 53
        Me.Label4.Text = "Discount"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(120, 23)
        Me.Label2.TabIndex = 52
        Me.Label2.Text = "Description"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 23)
        Me.Label1.TabIndex = 51
        Me.Label1.Text = "Member Type"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(16, 88)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(96, 23)
        Me.Label6.TabIndex = 50
        Me.Label6.Text = "Active"
        '
        'chkActive
        '
        Me.chkActive.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkActive.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkActive.Location = New System.Drawing.Point(136, 88)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(11, 24)
        Me.chkActive.TabIndex = 3
        '
        'txtdiscount
        '
        Me.txtdiscount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtdiscount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdiscount.Location = New System.Drawing.Point(136, 64)
        Me.txtdiscount.MaxLength = 3
        Me.txtdiscount.Name = "txtdiscount"
        Me.txtdiscount.Size = New System.Drawing.Size(112, 20)
        Me.txtdiscount.TabIndex = 2
        Me.txtdiscount.Tag = "NE;"
        Me.txtdiscount.Text = ""
        '
        'txtMemberTypeDesc
        '
        Me.txtMemberTypeDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMemberTypeDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMemberTypeDesc.Location = New System.Drawing.Point(136, 40)
        Me.txtMemberTypeDesc.MaxLength = 50
        Me.txtMemberTypeDesc.Name = "txtMemberTypeDesc"
        Me.txtMemberTypeDesc.Size = New System.Drawing.Size(208, 20)
        Me.txtMemberTypeDesc.TabIndex = 1
        Me.txtMemberTypeDesc.Tag = "NE;"
        Me.txtMemberTypeDesc.Text = ""
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.Location = New System.Drawing.Point(624, 304)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.TabIndex = 5
        Me.btnDelete.Text = "Delete(F8)"
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(8, 0)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(210, 75)
        Me.pic.TabIndex = 55
        Me.pic.TabStop = False
        '
        'frmMemberType
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(784, 334)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.grdMember)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnDelete)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Name = "frmMemberType"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MemberType"
        CType(Me.grdMember, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        ClearMe()
        txtMemberType.Focus()
        txtMemberType.ReadOnly = False



    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sActive As String
        Try
            If ValidateMe() Then
                If ChkActive.Checked = True Then
                    sActive = "1"
                Else
                    sActive = "0"
                End If
                If objMember.MemberTypeSave(txtMemberType.Text.Trim, txtMemberTypeDesc.Text.Trim, txtdiscount.Text.Trim, sActive) Then
                    MessageBox.Show("MemberType saved sucuessfully.")
                    ClearMe()
                    GetMemberTypeDetail()
                    grdMember.Focus()
                    ShowMemberTypeDetail(grdMember.ActiveRow)
                Else
                    MessageBox.Show("Error in Save")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        Try
            If grdMember.MaxRows > 0 Then
                Dim sMemberType As String

                sMemberType = GetText(grdMember, grdMember.ActiveRow, MemberType)
                If MessageBox.Show("Do you want to delete " & GetText(grdMember, grdMember.ActiveRow, MemberType) & " MemberType?", "GoldPOS", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    If objMember.DeleteMemberType(sMemberType) Then
                        MessageBox.Show("MemberType  detail deleted.")
                        grdMember.MaxRows = grdMember.MaxRows - 1
                        ClearMe()
                        GetMemberTypeDetail()
                        grdMember.Focus()
                        ShowMemberTypeDetail(grdMember.ActiveRow)
                    Else
                        MessageBox.Show("Error in deleting Member detail.")
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        
    End Sub

    Private Sub frmMemberType_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        subGridSetting(grdMember)
        GetMemberTypeDetail()
        ShowMemberTypeDetail(grdMember.ActiveRow)
        grdMember.Focus()



    End Sub

    Private Sub ClearMe()
        txtMemberType.Clear()
        txtMemberType.Text = ""

        txtMemberTypeDesc.Clear()
        txtdiscount.Clear()

        btnDelete.Enabled = False
        txtMemberType.Enabled = True
        chkActive.Checked = False

        'txtSalesManCode.Focus()
    End Sub
    Private Sub GetMemberTypeDetail()


        'txtMemberType.Enabled = True

        Try
            Dim dsMemberType As New DataSet
            Dim iCnt As Long
            Dim iActive As String

            dsMemberType = objMember.GetMemberType
            If Not IsNothing(dsMemberType) Then
                If dsMemberType.Tables("MemberType").Rows.Count > 0 Then
                    grdMember.MaxRows = 0
                    For iCnt = 0 To dsMemberType.Tables("MemberType").Rows.Count - 1
                        grdMember.MaxRows = grdMember.MaxRows + 1
                        SetText(grdMember, iCnt + 1, MemberType, Trim(dsMemberType.Tables("MemberType").Rows(iCnt)("MemberType") & ""))
                        SetText(grdMember, iCnt + 1, MemberTypeDesc, Trim(dsMemberType.Tables("MemberType").Rows(iCnt)("MemberTypeDescription") & ""))
                        SetText(grdMember, iCnt + 1, MemberTypeDiscount, Trim(dsMemberType.Tables("MemberType").Rows(iCnt)("MemberTypeDiscount") & ""))
                        SetText(grdMember, iCnt + 1, Active, Trim(dsMemberType.Tables("MemberType").Rows(iCnt)("Active") & ""))

                    Next
                    'grdMember.MaxRows = grdMember.MaxRows - 1

                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


    End Sub
    Private Sub ShowMemberType(ByVal Row As Integer)
        Try
            btnDelete.Enabled = True
            If Row > 0 Then
                txtMemberType.Text = GetText(grdMember, Row, MemberType)
                txtMemberType.Enabled = False
                txtMemberTypeDesc.Text = GetText(grdMember, Row, MemberTypeDesc)


            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtMemberDesc_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMemberTypeDesc.TextChanged

    End Sub
#Region "Validation"

    Private Function ValidateMe() As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            If txtMemberType.Text.Trim = "" Then
                MessageBox.Show("MemberType should not be blank.")
                txtMemberType.Focus()
                Exit Function
            End If
            If txtMemberTypeDesc.Text.Trim = "" Then
                MessageBox.Show("SalesManName should not be blank.")
                txtMemberTypeDesc.Focus()
                Exit Function
            End If
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            bStatus = False
        Finally
            ValidateMe = bStatus
        End Try

    End Function

#End Region
    Private Sub ShowMemberTypeDetail(ByVal Row As Integer)
        Try
            btnDelete.Enabled = True
            If grdMember.MaxRows > 0 Then
                txtMemberType.Text = GetText(grdMember, Row, MemberType)
                txtMemberType.ReadOnly = True

                txtMemberTypeDesc.Text = GetText(grdMember, Row, MemberTypeDesc)
                txtdiscount.Text = GetText(grdMember, Row, MemberTypeDiscount)
                If fnGetText(grdMember, Row, Active) = "1" Then
                    chkActive.Checked = True
                Else
                    chkActive.Checked = False
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub grdMember_Advance(ByVal sender As System.Object, ByVal e As AxFPSpread._DSpreadEvents_AdvanceEvent) Handles grdMember.Advance

    End Sub

    Private Sub grdMember_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_ClickEvent) Handles grdMember.ClickEvent
        ShowMemberTypeDetail(e.row)

    End Sub
    Private Sub frmMemberType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F6
                btnNew_Click(sender, e)
            Case Keys.F7
                btnSave_Click(sender, e)
            Case Keys.F8
                If btnDelete.Enabled Then btnDelete_Click(sender, e)
            Case Keys.Escape
                btnClose_Click(sender, e)

            Case Keys.Enter
                SendKeys.Send("{Tab}")
                ' Case Keys.Up
                '   SendKeys.Send("+{Tab}")
                'Case Keys.Down
                '   SendKeys.Send("{Tab}")
        End Select
    End Sub
    Private Sub SubLoadSales()

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()

    End Sub

    Private Sub txtMemberType_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMemberType.TextChanged

    End Sub

    Private Sub txtMemberType_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMemberType.LostFocus
        Dim sMemberType As String

        Try
            Dim dsMember As New DataSet
            Dim iCnt As Long
            sMemberType = txtMemberType.Text.Trim
            dsMember = objMember.GetMemberTypewithCode(sMemberType)
            If Not IsNothing(dsMember) Then
                If dsMember.Tables("MemberType").Rows.Count > 0 Then
                    txtMemberTypeDesc.Text = Trim(dsMember.Tables("MemberType").Rows(0)("MemberTypeDescription") & "")
                    txtdiscount.Text = Trim(dsMember.Tables("MemberType").Rows(0)("MemberTypeDiscount") & "")
                    chkActive.Checked = Trim(dsMember.Tables("memberType").Rows(0)("Active") & "")
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


    End Sub

    Private Sub frmMemberType_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated

        GetMemberTypeDetail()
    End Sub




    Private Sub txtdiscount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtdiscount.LostFocus
        If Not IsNumeric(txtdiscount.Text.Trim) Then
            MsgBox("Discount Should be Numeric")
            txtdiscount.Focus()
        End If
    End Sub
End Class

