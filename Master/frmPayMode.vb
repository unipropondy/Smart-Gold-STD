Public Class frmPayMode
    Inherits System.Windows.Forms.Form

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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtPosition As System.Windows.Forms.TextBox
    Friend WithEvents txtPayMode As System.Windows.Forms.TextBox
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents txtGLAc As System.Windows.Forms.TextBox
    Friend WithEvents txtCommision As System.Windows.Forms.TextBox
    Friend WithEvents ErrP As System.Windows.Forms.ErrorProvider
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents grdPayMode As System.Windows.Forms.DataGridView
    Friend WithEvents cPaymode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cActive As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents cPosition As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCommision As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cGLAccountCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label6 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPayMode))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.chkActive = New System.Windows.Forms.CheckBox()
        Me.txtGLAc = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCommision = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtPosition = New System.Windows.Forms.TextBox()
        Me.txtPayMode = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.ErrP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.grdPayMode = New System.Windows.Forms.DataGridView()
        Me.cPaymode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cDescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cActive = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.cPosition = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cCommision = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cGLAccountCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1.SuspendLayout()
        CType(Me.ErrP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdPayMode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.chkActive)
        Me.Panel1.Controls.Add(Me.txtGLAc)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtCommision)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtDescription)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtPosition)
        Me.Panel1.Controls.Add(Me.txtPayMode)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(408, 101)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(354, 176)
        Me.Panel1.TabIndex = 200
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(16, 116)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(96, 23)
        Me.Label6.TabIndex = 48
        Me.Label6.Text = "Active"
        '
        'chkActive
        '
        Me.chkActive.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkActive.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ErrP.SetIconAlignment(Me.chkActive, System.Windows.Forms.ErrorIconAlignment.MiddleLeft)
        Me.chkActive.Location = New System.Drawing.Point(120, 114)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(11, 24)
        Me.chkActive.TabIndex = 5
        '
        'txtGLAc
        '
        Me.txtGLAc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGLAc.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGLAc.Location = New System.Drawing.Point(120, 115)
        Me.txtGLAc.MaxLength = 20
        Me.txtGLAc.Name = "txtGLAc"
        Me.txtGLAc.Size = New System.Drawing.Size(112, 21)
        Me.txtGLAc.TabIndex = 4
        Me.txtGLAc.Tag = "NE;"
        Me.txtGLAc.Visible = False
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 116)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(126, 23)
        Me.Label3.TabIndex = 47
        Me.Label3.Text = "GLA/c"
        Me.Label3.Visible = False
        '
        'txtCommision
        '
        Me.txtCommision.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCommision.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCommision.Location = New System.Drawing.Point(120, 88)
        Me.txtCommision.MaxLength = 50
        Me.txtCommision.Name = "txtCommision"
        Me.txtCommision.Size = New System.Drawing.Size(112, 21)
        Me.txtCommision.TabIndex = 3
        Me.txtCommision.Tag = "NE;"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(16, 89)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(126, 23)
        Me.Label5.TabIndex = 45
        Me.Label5.Text = "Commision"
        '
        'txtDescription
        '
        Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescription.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.Location = New System.Drawing.Point(120, 61)
        Me.txtDescription.MaxLength = 20
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(176, 21)
        Me.txtDescription.TabIndex = 2
        Me.txtDescription.Tag = "NE;"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(16, 62)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(126, 23)
        Me.Label4.TabIndex = 43
        Me.Label4.Text = "Description"
        '
        'txtPosition
        '
        Me.txtPosition.BackColor = System.Drawing.Color.White
        Me.txtPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPosition.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPosition.Location = New System.Drawing.Point(120, 34)
        Me.txtPosition.MaxLength = 20
        Me.txtPosition.Name = "txtPosition"
        Me.txtPosition.Size = New System.Drawing.Size(64, 21)
        Me.txtPosition.TabIndex = 1
        Me.txtPosition.Tag = "NE;"
        '
        'txtPayMode
        '
        Me.txtPayMode.BackColor = System.Drawing.Color.White
        Me.txtPayMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPayMode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayMode.Location = New System.Drawing.Point(120, 7)
        Me.txtPayMode.MaxLength = 50
        Me.txtPayMode.Name = "txtPayMode"
        Me.txtPayMode.Size = New System.Drawing.Size(112, 21)
        Me.txtPayMode.TabIndex = 0
        Me.txtPayMode.Tag = "NE;"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(126, 23)
        Me.Label2.TabIndex = 41
        Me.Label2.Text = "Position"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(126, 23)
        Me.Label1.TabIndex = 40
        Me.Label1.Text = "Pay Mode"
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(662, 287)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 9
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(450, 287)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 40)
        Me.btnSave.TabIndex = 7
        Me.btnSave.Text = "Save(F7)"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDelete.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(556, 287)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(100, 40)
        Me.btnDelete.TabIndex = 8
        Me.btnDelete.Text = "Delete(F8)"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnNew.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Location = New System.Drawing.Point(344, 287)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(100, 40)
        Me.btnNew.TabIndex = 6
        Me.btnNew.Text = "New(F6)"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'ErrP
        '
        Me.ErrP.ContainerControl = Me
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(1, -6)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 92)
        Me.pic.TabIndex = 201
        Me.pic.TabStop = False
        '
        'grdPayMode
        '
        Me.grdPayMode.AllowUserToAddRows = False
        Me.grdPayMode.AllowUserToDeleteRows = False
        Me.grdPayMode.AllowUserToResizeColumns = False
        Me.grdPayMode.AllowUserToResizeRows = False
        Me.grdPayMode.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdPayMode.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdPayMode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdPayMode.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cPaymode, Me.cDescription, Me.cActive, Me.cPosition, Me.cCommision, Me.cGLAccountCode})
        Me.grdPayMode.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdPayMode.Location = New System.Drawing.Point(12, 101)
        Me.grdPayMode.Name = "grdPayMode"
        Me.grdPayMode.RowHeadersVisible = False
        Me.grdPayMode.Size = New System.Drawing.Size(390, 176)
        Me.grdPayMode.TabIndex = 202
        '
        'cPaymode
        '
        Me.cPaymode.HeaderText = "Paymode"
        Me.cPaymode.Name = "cPaymode"
        Me.cPaymode.Width = 120
        '
        'cDescription
        '
        Me.cDescription.HeaderText = "Description"
        Me.cDescription.Name = "cDescription"
        Me.cDescription.Width = 180
        '
        'cActive
        '
        Me.cActive.HeaderText = "Active"
        Me.cActive.Name = "cActive"
        '
        'cPosition
        '
        Me.cPosition.HeaderText = "Position"
        Me.cPosition.Name = "cPosition"
        Me.cPosition.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cPosition.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cPosition.Visible = False
        '
        'cCommision
        '
        Me.cCommision.HeaderText = "Commision"
        Me.cCommision.Name = "cCommision"
        Me.cCommision.Visible = False
        '
        'cGLAccountCode
        '
        Me.cGLAccountCode.HeaderText = "GLAccountCode"
        Me.cGLAccountCode.Name = "cGLAccountCode"
        '
        'frmPayMode
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(785, 339)
        Me.Controls.Add(Me.grdPayMode)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPayMode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PayMode"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.ErrP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdPayMode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


    Const PayMode = 1
    Const Description = 2
    Const Active = 3
    Const Position = 4
    Const Commission = 5
    Const GLAc = 6

    Dim objclsPayMode As New clsPayMode
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ClearMe()
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Sub
    Private Sub frmCategory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F6
                btnNew_Click(sender, e)
            Case Keys.F7
                btnSave_Click(sender, e)
            Case Keys.F8
                btnDelete_Click(sender, e)
            Case Keys.Escape
                btnClose_Click(sender, e)
            Case Keys.Return
                SendKeys.Send("{Tab}")
        End Select
    End Sub
    Private Sub ClearMe()
        txtPosition.Clear()
        txtPayMode.Clear()
        txtDescription.Clear()
        txtCommision.Clear()
        txtGLAc.Clear()
        chkActive.Checked = False
        txtPayMode.ReadOnly = False
        txtPosition.ReadOnly = False
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub frmPayMode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'subGridSetting(grdPayMode)
        subLoadPayModeDetail()
    End Sub
    Private Function ValidateMe() As Boolean
        Try
            ValidateMe = False
            If txtPosition.Text.Trim = "" Then
                MsgBox("Position should not be blank! ", MsgBoxStyle.Exclamation)
                txtPosition.Focus()
                Exit Function
            ElseIf Not IsNumeric(txtPosition.Text.Trim) Then
                MsgBox("Position should be Numeric!", MsgBoxStyle.Exclamation)
                txtPosition.Clear()
                txtPosition.Focus()
                Exit Function
            End If

            If txtPayMode.Text.Trim = "" Then
                MsgBox("PayMode should not be blank!", MsgBoxStyle.Exclamation)
                txtPayMode.Focus()
                Exit Function
            End If

            If txtDescription.Text.Trim = "" Then
                MsgBox("Description should not be blank!", MsgBoxStyle.Exclamation)
                txtDescription.Focus()
                Exit Function
            End If

            If txtCommision.Text.Trim = "" Then
                MsgBox("Commision code should not be blank!", MsgBoxStyle.Exclamation)
                txtCommision.Focus()
                Exit Function
            ElseIf Not IsNumeric(txtCommision.Text.Trim) Then
                MsgBox("Commision should be Numeric!", MsgBoxStyle.Exclamation)
                txtCommision.Clear()
                txtCommision.Focus()
                Exit Function
            End If

            ''If txtGLAc.Text.Trim = "" Then
            ''    MsgBox("txtGLAc should not be blank!", MsgBoxStyle.Exclamation)
            ''    txtGLAc.Focus()
            ''    Exit Function
            ''End If
            ValidateMe = True
        Catch ex As Exception
            MsgBox(Err.Description)
            ValidateMe = False
        Finally
            Err.Clear()
        End Try

    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sActive As Integer
        Try
            If ValidateMe() Then
                If chkActive.Checked Then
                    sActive = 1
                Else
                    sActive = 0
                End If
                If objclsPayMode.SavePayMode(Val(txtPosition.Text.Trim), txtPayMode.Text.Trim, txtDescription.Text.Trim, Val(txtCommision.Text.Trim), txtGLAc.Text.Trim, sActive) Then
                    MsgBox("Pay Mode saved successfully!", MsgBoxStyle.Information)
                    ClearMe()
                    subLoadPayModeDetail()
                Else
                    MsgBox("Error in Save !", MsgBoxStyle.Critical)
                End If
            End If
        Catch ex As Exception
            MsgBox(Err.Description)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If MsgBox("Do you want to Delete  :  " & fnGetdgvText(grdPayMode, grdPayMode.CurrentRow.Index, 0), vbYesNo) Then
                If Not objclsPayMode.DeletePayMode(fnGetdgvText(grdPayMode, grdPayMode.CurrentRow.Index, 0).Trim) Then
                    MsgBox("Error in Delete !", vbCritical)
                Else
                    subLoadPayModeDetail()
                End If
            End If
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Sub

    Private Sub subLoadPayModeDetail()
        Try
            Dim dsPayMode As New DataSet
            Dim iCnt As Long
            Dim iActive As Boolean
            dsPayMode = objclsPayMode.GetPayMode()
            If dsPayMode.Tables(0).Rows.Count > 0 Then
                grdPayMode.RowCount = 0
                For iCnt = 0 To dsPayMode.Tables(0).Rows.Count - 1
                    grdPayMode.Rows.Add()
                    fnSetdgvText(grdPayMode, iCnt, 0, Trim(dsPayMode.Tables(0).Rows(iCnt)("PayMode") & ""))
                    fnSetdgvText(grdPayMode, iCnt, 1, Trim(dsPayMode.Tables(0).Rows(iCnt)("Description") & ""))
                    If Trim(dsPayMode.Tables(0).Rows(iCnt)("Active") & "") Then
                        iActive = True
                    Else
                        iActive = False
                    End If
                    fnSetdgvText(grdPayMode, iCnt, 2, iActive)
                    fnSetdgvText(grdPayMode, iCnt, 3, Trim(dsPayMode.Tables(0).Rows(iCnt)("Position") & ""))
                    fnSetdgvText(grdPayMode, iCnt, 4, Val(Trim(dsPayMode.Tables(0).Rows(iCnt)("Commission") & "")))
                    fnSetdgvText(grdPayMode, iCnt, 5, Trim(dsPayMode.Tables(0).Rows(iCnt)("GLAccountCode") & ""))
                Next
            End If
            'grdPayMode.MaxRows -= 1
            fncGetRowDetail(0)
        Catch ex As Exception
            MsgBox(Err.Description)
        End Try
    End Sub

    Private Sub fncGetRowDetail(ByVal iRow As Integer)
        txtPayMode.Text = Trim(fnGetdgvText(grdPayMode, iRow, 0))
        txtDescription.Text = Trim(fnGetdgvText(grdPayMode, iRow, 1))
        txtCommision.Text = Format(Val(Trim(fnGetdgvText(grdPayMode, iRow, 4))), "0.00")
        txtPosition.Text = Val(Trim(fnGetdgvText(grdPayMode, iRow, 3)))
        txtGLAc.Text = Trim(fnGetdgvText(grdPayMode, iRow, 5))
        If fnGetdgvText(grdPayMode, iRow, 2) = True Then
            chkActive.Checked = True
        Else
            chkActive.Checked = False
        End If
        txtPayMode.ReadOnly = True
        txtPosition.ReadOnly = True
    End Sub

    Private Sub txtPayMode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPayMode.LostFocus
        If txtPayMode.Text.Trim <> "" Then
            subGetDetail(txtPayMode.Text.Trim)
        End If
    End Sub
    Private Sub subGetDetail(ByVal sPayModeCode As String)
        Try
            Dim ds As New DataSet
            ds = objclsPayMode.GetPayModeDetail(sPayModeCode)
            If ds.Tables(0).Rows.Count > 0 Then
                txtPayMode.Text = Trim(ds.Tables(0).Rows(0)("paymode") & "")
                txtDescription.Text = Trim(ds.Tables(0).Rows(0)("description") & "")
                txtPosition.Text = Val(Trim(ds.Tables(0).Rows(0)("position") & ""))
                txtCommision.Text = Val(Trim(ds.Tables(0).Rows(0)("commission") & ""))
                txtGLAc.Text = Trim(ds.Tables(0).Rows(0)("GLAccountCode") & "")
                If Trim(ds.Tables(0).Rows(0)("active") & "") Then
                    chkActive.Checked = True
                Else
                    chkActive.Checked = False
                End If
                txtPayMode.ReadOnly = True
                txtPosition.ReadOnly = True
            End If
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try

    End Sub

    Private Sub txtPosition_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPosition.LostFocus
        If txtPosition.Text.Trim.Length <> 0 And IsNumeric(txtPosition.Text.Trim) Then
            subGetDetailByPosition(txtPosition.Text.Trim)
        End If
    End Sub
    Private Sub subGetDetailByPosition(ByVal sPosition As String)
        Try
            Dim ds As New DataSet
            ds = objclsPayMode.GetPayModeDetailByPosition(sPosition)
            If ds.Tables(0).Rows.Count > 0 Then
                txtPayMode.Text = Trim(ds.Tables(0).Rows(0)("paymode") & "")
                txtDescription.Text = Trim(ds.Tables(0).Rows(0)("description") & "")
                txtPosition.Text = Val(Trim(ds.Tables(0).Rows(0)("position") & ""))
                txtCommision.Text = Val(Trim(ds.Tables(0).Rows(0)("commission") & ""))
                txtGLAc.Text = Trim(ds.Tables(0).Rows(0)("GLAccountCode") & "")
                If Trim(ds.Tables(0).Rows(0)("active") & "") Then
                    chkActive.Checked = True
                Else
                    chkActive.Checked = False
                End If
                txtPayMode.ReadOnly = True
                txtPosition.ReadOnly = True
            End If
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try

    End Sub

    Private Sub grdPayMode_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPayMode.CellContentClick

    End Sub

    Private Sub grdPayMode_Click(sender As Object, e As System.EventArgs) Handles grdPayMode.Click
        fncGetRowDetail(grdPayMode.CurrentRow.Index)
    End Sub
End Class
