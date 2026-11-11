Public Class frmNricType
    Inherits System.Windows.Forms.Form
    Const NricTypeCode = 1
    Const Description = 2
    Const Active = 3

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
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtNricDesc As System.Windows.Forms.TextBox
    Friend WithEvents txtNricTypeCode As System.Windows.Forms.TextBox
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents grdNric As AxFPSpread.AxvaSpread
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmNricType))
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.grdNric = New AxFPSpread.AxvaSpread
        Me.btnNew = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtNricTypeCode = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.chkActive = New System.Windows.Forms.CheckBox
        Me.txtNricDesc = New System.Windows.Forms.TextBox
        Me.btnDelete = New System.Windows.Forms.Button
        Me.pic = New System.Windows.Forms.PictureBox
        CType(Me.grdNric, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(616, 224)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 23)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close(Esc)"
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.Location = New System.Drawing.Point(456, 224)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(72, 23)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save(F7)"
        '
        'grdNric
        '
        Me.grdNric.Location = New System.Drawing.Point(8, 88)
        Me.grdNric.Name = "grdNric"
        Me.grdNric.OcxState = CType(resources.GetObject("grdNric.OcxState"), System.Windows.Forms.AxHost.State)
        Me.grdNric.Size = New System.Drawing.Size(376, 120)
        Me.grdNric.TabIndex = 47
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(376, 224)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(72, 23)
        Me.btnNew.TabIndex = 6
        Me.btnNew.Text = "New(F6)"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txtNricTypeCode)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.chkActive)
        Me.Panel1.Controls.Add(Me.txtNricDesc)
        Me.Panel1.Location = New System.Drawing.Point(392, 88)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(296, 120)
        Me.Panel1.TabIndex = 42
        '
        'txtNricTypeCode
        '
        Me.txtNricTypeCode.BackColor = System.Drawing.Color.White
        Me.txtNricTypeCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNricTypeCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNricTypeCode.Location = New System.Drawing.Point(136, 16)
        Me.txtNricTypeCode.Name = "txtNricTypeCode"
        Me.txtNricTypeCode.Size = New System.Drawing.Size(144, 20)
        Me.txtNricTypeCode.TabIndex = 0
        Me.txtNricTypeCode.Text = ""
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
        Me.Label1.Text = "Nric Type Code"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(16, 80)
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
        Me.chkActive.Location = New System.Drawing.Point(136, 72)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(11, 24)
        Me.chkActive.TabIndex = 2
        '
        'txtNricDesc
        '
        Me.txtNricDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNricDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNricDesc.Location = New System.Drawing.Point(136, 40)
        Me.txtNricDesc.MaxLength = 50
        Me.txtNricDesc.Name = "txtNricDesc"
        Me.txtNricDesc.Size = New System.Drawing.Size(144, 20)
        Me.txtNricDesc.TabIndex = 1
        Me.txtNricDesc.Tag = "NE;"
        Me.txtNricDesc.Text = ""
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.Location = New System.Drawing.Point(536, 224)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(72, 23)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "Delete(F8)"
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(8, 8)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(210, 75)
        Me.pic.TabIndex = 56
        Me.pic.TabStop = False
        '
        'frmNricType
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(696, 254)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.grdNric)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnDelete)
        Me.KeyPreview = True
        Me.Name = "frmNricType"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "NricType"
        CType(Me.grdNric, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmNricType_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        subGridSetting(grdNric)
        GetNricTypeDetail()
        ShowNricTypeDetail(grdNric.ActiveRow)
        grdNric.Focus()

    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        ClearMe()
        txtNricTypeCode.Focus()
        txtNricTypeCode.ReadOnly = False

    End Sub
    Private Sub ClearMe()
        txtNricTypeCode.Clear()
        txtNricDesc.Clear()
        chkActive.Checked = False

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sActive As String
        Try
            If txtNricTypeCode.Text = "" Then
                MsgBox("Nric TypeCode Should not be Empty")
                txtNricTypeCode.Focus()
                Exit Sub
            End If
            If chkActive.Checked = True Then
                sActive = "1"
            Else
                sActive = "0"
            End If
            If objMember.NricTypeSave(txtNricTypeCode.Text.Trim, txtNricDesc.Text.Trim, sActive) Then
                MessageBox.Show("Nric Type saved sucuessfully.")
                ClearMe()
                GetNricTypeDetail()
                grdNric.Focus()
                ShowNricTypeDetail(grdNric.ActiveRow)
            Else
                MessageBox.Show("Error in Save")
            End If
            ' End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function ValidateMe() As Boolean
        If txtNricTypeCode.Text = "" Then
            MsgBox("Nric Type Should Not Be Empty")
            txtNricTypeCode.Focus()
            Exit Function

        End If


    End Function
    Private Sub GetNricTypeDetail()

        Try
            Dim dsNricType As New DataSet
            Dim iCnt As Long
            Dim iActive As String

            dsNricType = objMember.GetNricType
            If Not IsNothing(dsNricType) Then
                If dsNricType.Tables("NricType").Rows.Count > 0 Then
                    grdNric.MaxRows = 0
                    For iCnt = 0 To dsNricType.Tables("NricType").Rows.Count - 1
                        grdNric.MaxRows = grdNric.MaxRows + 1
                        SetText(grdNric, iCnt + 1, NricTypeCode, Trim(dsNricType.Tables("NricType").Rows(iCnt)("NricTypeCode") & ""))
                        SetText(grdNric, iCnt + 1, Description, Trim(dsNricType.Tables("NricType").Rows(iCnt)("Description") & ""))
                        SetText(grdNric, iCnt + 1, Active, Trim(dsNricType.Tables("NricType").Rows(iCnt)("Active") & ""))

                    Next
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


    End Sub
    

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If grdNric.MaxRows > 0 Then
                Dim sNricTypeCode As String

                sNricTypeCode = GetText(grdNric, grdNric.ActiveRow, NricTypeCode)
                If MessageBox.Show("Do you want to delete " & GetText(grdNric, grdNric.ActiveRow, NricTypeCode) & " NricTypeCode?", "GoldPOS", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    If objMember.DeleteNricType(sNricTypeCode) Then
                        MessageBox.Show(" Nric Type   detail deleted.")
                        grdNric.MaxRows = grdNric.MaxRows - 1
                        ClearMe()
                        GetNricTypeDetail()
                        grdNric.Focus()
                        ShowNricTypeDetail(grdNric.ActiveRow)
                    Else
                        MessageBox.Show("Error in deleting Nric detail.")
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub ShowNricTypeDetail(ByVal Row As Integer)
        Try
            btnDelete.Enabled = True
            If grdNric.MaxRows > 0 Then
                txtNricTypeCode.Text = GetText(grdNric, Row, NricTypeCode)
                txtNricTypeCode.ReadOnly = True
                txtNricDesc.Text = GetText(grdNric, Row, Description)
                If fnGetText(grdNric, Row, Active) = "1" Then
                    chkActive.Checked = True
                Else
                    chkActive.Checked = False
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmNricType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
               
        End Select
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()

    End Sub

    Private Sub frmNricType_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        GetNricTypeDetail()

    End Sub

    Private Sub grdNric_Advance(ByVal sender As System.Object, ByVal e As AxFPSpread._DSpreadEvents_AdvanceEvent) Handles grdNric.Advance

    End Sub

    Private Sub grdNric_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_ClickEvent) Handles grdNric.ClickEvent
        ShowNricTypeDetail(e.row)
    End Sub
End Class
