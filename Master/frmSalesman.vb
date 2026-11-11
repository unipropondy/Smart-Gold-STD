Public Class frmSalesMan
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
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtSalesManCode As System.Windows.Forms.TextBox
    Friend WithEvents txtSalesManName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents ErrP As System.Windows.Forms.ErrorProvider
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents txtSortOrder As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chkShowInReport As System.Windows.Forms.CheckBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblAllowSR As System.Windows.Forms.Label
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents cboSalesManGroup As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents grdSalesMan As System.Windows.Forms.DataGridView
    Friend WithEvents cSalesmanCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSalesManName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cD As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cG As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cH As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkAllowSalesReturn As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSalesMan))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cboSalesManGroup = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblAllowSR = New System.Windows.Forms.Label()
        Me.chkAllowSalesReturn = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.chkShowInReport = New System.Windows.Forms.CheckBox()
        Me.txtSortOrder = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.txtSalesManCode = New System.Windows.Forms.TextBox()
        Me.txtSalesManName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.ErrP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.grdSalesMan = New System.Windows.Forms.DataGridView()
        Me.cSalesmanCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cSalesManName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cG = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cH = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1.SuspendLayout()
        CType(Me.ErrP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSalesMan, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnNew.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Location = New System.Drawing.Point(392, 428)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(100, 40)
        Me.btnNew.TabIndex = 7
        Me.btnNew.Text = "New(F6)"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(710, 428)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(498, 428)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 40)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "Save(F7)"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.cboSalesManGroup)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.lblAllowSR)
        Me.Panel1.Controls.Add(Me.chkAllowSalesReturn)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.chkShowInReport)
        Me.Panel1.Controls.Add(Me.txtSortOrder)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtRemarks)
        Me.Panel1.Controls.Add(Me.txtPassword)
        Me.Panel1.Controls.Add(Me.txtSalesManCode)
        Me.Panel1.Controls.Add(Me.txtSalesManName)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(392, 128)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(418, 294)
        Me.Panel1.TabIndex = 36
        '
        'cboSalesManGroup
        '
        Me.cboSalesManGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSalesManGroup.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSalesManGroup.FormattingEnabled = True
        Me.cboSalesManGroup.Items.AddRange(New Object() {"SALES", "ADMIN", "SUPER ADMIN"})
        Me.cboSalesManGroup.Location = New System.Drawing.Point(136, 104)
        Me.cboSalesManGroup.Name = "cboSalesManGroup"
        Me.cboSalesManGroup.Size = New System.Drawing.Size(121, 21)
        Me.cboSalesManGroup.TabIndex = 40
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(16, 104)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(114, 23)
        Me.Label7.TabIndex = 41
        Me.Label7.Text = "SalesMan Group"
        '
        'lblAllowSR
        '
        Me.lblAllowSR.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAllowSR.Location = New System.Drawing.Point(16, 243)
        Me.lblAllowSR.Name = "lblAllowSR"
        Me.lblAllowSR.Size = New System.Drawing.Size(114, 40)
        Me.lblAllowSR.TabIndex = 34
        Me.lblAllowSR.Text = "Allow SalesReturn /Bill Cancel"
        '
        'chkAllowSalesReturn
        '
        Me.chkAllowSalesReturn.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllowSalesReturn.Location = New System.Drawing.Point(136, 245)
        Me.chkAllowSalesReturn.Name = "chkAllowSalesReturn"
        Me.chkAllowSalesReturn.Size = New System.Drawing.Size(39, 24)
        Me.chkAllowSalesReturn.TabIndex = 33
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(16, 211)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(114, 25)
        Me.Label6.TabIndex = 32
        Me.Label6.Text = "Show In Report"
        '
        'chkShowInReport
        '
        Me.chkShowInReport.Location = New System.Drawing.Point(136, 210)
        Me.chkShowInReport.Name = "chkShowInReport"
        Me.chkShowInReport.Size = New System.Drawing.Size(39, 24)
        Me.chkShowInReport.TabIndex = 6
        '
        'txtSortOrder
        '
        Me.txtSortOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSortOrder.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSortOrder.Location = New System.Drawing.Point(136, 169)
        Me.txtSortOrder.MaxLength = 50
        Me.txtSortOrder.Name = "txtSortOrder"
        Me.txtSortOrder.Size = New System.Drawing.Size(208, 21)
        Me.txtSortOrder.TabIndex = 5
        Me.txtSortOrder.Tag = "NE;"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(16, 164)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(170, 25)
        Me.Label5.TabIndex = 30
        Me.Label5.Text = "Sort Order"
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(136, 131)
        Me.txtRemarks.MaxLength = 50
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(208, 21)
        Me.txtRemarks.TabIndex = 4
        Me.txtRemarks.Tag = "NE;"
        '
        'txtPassword
        '
        Me.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPassword.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword.Location = New System.Drawing.Point(136, 72)
        Me.txtPassword.MaxLength = 20
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(112, 21)
        Me.txtPassword.TabIndex = 3
        Me.txtPassword.Tag = "NE;"
        '
        'txtSalesManCode
        '
        Me.txtSalesManCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesManCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesManCode.Location = New System.Drawing.Point(136, 10)
        Me.txtSalesManCode.MaxLength = 20
        Me.txtSalesManCode.Name = "txtSalesManCode"
        Me.txtSalesManCode.Size = New System.Drawing.Size(112, 21)
        Me.txtSalesManCode.TabIndex = 0
        Me.txtSalesManCode.Tag = "NE;"
        '
        'txtSalesManName
        '
        Me.txtSalesManName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesManName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesManName.Location = New System.Drawing.Point(136, 41)
        Me.txtSalesManName.MaxLength = 50
        Me.txtSalesManName.Name = "txtSalesManName"
        Me.txtSalesManName.Size = New System.Drawing.Size(208, 21)
        Me.txtSalesManName.TabIndex = 1
        Me.txtSalesManName.Tag = "NE;"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(16, 128)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(170, 25)
        Me.Label4.TabIndex = 28
        Me.Label4.Text = "Remarks"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 73)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(170, 23)
        Me.Label3.TabIndex = 27
        Me.Label3.Text = "Password"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(170, 23)
        Me.Label2.TabIndex = 26
        Me.Label2.Text = "SalesMan Name"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(170, 23)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "SalesMan Code"
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDelete.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(604, 428)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(100, 40)
        Me.btnDelete.TabIndex = 5
        Me.btnDelete.Text = "Delete(F8)"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'ErrP
        '
        Me.ErrP.ContainerControl = Me
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(6, -4)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 122
        Me.pic.TabStop = False
        '
        'grdSalesMan
        '
        Me.grdSalesMan.AllowUserToAddRows = False
        Me.grdSalesMan.AllowUserToDeleteRows = False
        Me.grdSalesMan.AllowUserToResizeColumns = False
        Me.grdSalesMan.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdSalesMan.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdSalesMan.ColumnHeadersHeight = 25
        Me.grdSalesMan.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cSalesmanCode, Me.cSalesManName, Me.cC, Me.cD, Me.cE, Me.cF, Me.cG, Me.cH})
        Me.grdSalesMan.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdSalesMan.Location = New System.Drawing.Point(6, 126)
        Me.grdSalesMan.Name = "grdSalesMan"
        Me.grdSalesMan.RowHeadersVisible = False
        Me.grdSalesMan.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.grdSalesMan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdSalesMan.Size = New System.Drawing.Size(376, 296)
        Me.grdSalesMan.TabIndex = 123
        '
        'cSalesmanCode
        '
        Me.cSalesmanCode.HeaderText = "SalesMan Code"
        Me.cSalesmanCode.Name = "cSalesmanCode"
        Me.cSalesmanCode.Width = 170
        '
        'cSalesManName
        '
        Me.cSalesManName.HeaderText = "Sales Man Name"
        Me.cSalesManName.Name = "cSalesManName"
        Me.cSalesManName.Width = 200
        '
        'cC
        '
        Me.cC.HeaderText = "C"
        Me.cC.Name = "cC"
        Me.cC.Visible = False
        '
        'cD
        '
        Me.cD.HeaderText = "D"
        Me.cD.Name = "cD"
        Me.cD.Visible = False
        '
        'cE
        '
        Me.cE.HeaderText = "E"
        Me.cE.Name = "cE"
        Me.cE.Visible = False
        '
        'cF
        '
        Me.cF.HeaderText = "F"
        Me.cF.Name = "cF"
        Me.cF.Visible = False
        '
        'cG
        '
        Me.cG.HeaderText = "G"
        Me.cG.Name = "cG"
        Me.cG.Visible = False
        '
        'cH
        '
        Me.cH.HeaderText = "H"
        Me.cH.Name = "cH"
        Me.cH.Visible = False
        '
        'frmSalesMan
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(7, 14)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(830, 480)
        Me.Controls.Add(Me.grdSalesMan)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnDelete)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSalesMan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Salesman"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.ErrP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSalesMan, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region
    Const SalesManCode = 1
    Const SalesManName = 2
    Const Password = 3
    Const Remarks = 4
    Const cSortOrder = 5
    Const cShowInReport = 6
    Const cAllowSalesReturn = 7
    Const cSalesManGroup = 8
    Dim objSalesman As New clsSalesMan

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub



    Private Sub frmSalesMan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'subGridSetting(grdSalesman)
        GetSalesManDetails()
        ShowSalesManDetail(0)
        'grdSalesman.Focus()
    End Sub

#Region "Validation"

    Private Function ValidateMe() As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            If txtSalesManCode.Text.Trim = "" Then
                MessageBox.Show("SaleManCode should not be blank.")
                txtSalesManCode.Focus()
                Exit Function
            End If
            If txtSalesManName.Text.Trim = "" Then
                MessageBox.Show("SalesManName should not be blank.")
                txtSalesManName.Focus()
                Exit Function
            End If
            If cboSalesManGroup.Text.Trim = "" Then
                MessageBox.Show("SalesManGroup should not be blank.")
                txtSalesManName.Focus()
                Exit Function
            End If
            If Val(txtSortOrder.Text) = 0 Then
                MessageBox.Show("Sort Order Can Not Be Zero.")
                txtSortOrder.Focus()
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

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        ClearMe()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim nShowInReport As Integer
        Dim nAllowSalesReturn As Integer
        Try
            If ValidateMe() Then
                If subSortOrderExists(txtSalesManCode.Text.Trim, Val(txtSortOrder.Text)) Then
                    If chkShowInReport.Checked = True Then
                        nShowInReport = 1
                    Else
                        nShowInReport = 0
                    End If
                    If chkAllowSalesReturn.Checked = True Then
                        nAllowSalesReturn = 1
                    Else
                        nAllowSalesReturn = 0
                    End If
                    If objSalesman.SalesManSave(txtSalesManCode.Text.Trim, txtSalesManName.Text.Trim, txtPassword.Text.Trim, _
                                txtRemarks.Text.Trim, Val(txtSortOrder.Text), nShowInReport, nAllowSalesReturn, cboSalesManGroup.Text.Trim) Then
                        MessageBox.Show("SalesMan detail saved sucssfully.")
                        ClearMe()
                        GetSalesManDetails()
                        grdSalesman.Focus()
                        ShowSalesManDetail(grdSalesMan.CurrentRow.Index)
                    Else
                        MessageBox.Show("Error in Save")
                    End If
                Else
                    MessageBox.Show("Sort Order Already Exists.")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If grdSalesMan.RowCount - 1 > 0 And grdSalesMan.CurrentRow.Index > 0 Then
                Dim sSalesManCode As String
                sSalesManCode = fnGetdgvText(grdSalesMan, grdSalesMan.CurrentRow.Index, SalesManCode)
                If MessageBox.Show("Do you want to delete " & GetText(grdSalesMan, grdSalesMan.CurrentRow.Index, SalesManCode) & " SalesMan?", "GoldPOS", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    If objSalesman.DeleteSalesMan(sSalesManCode) Then
                        MessageBox.Show("SalesMan detail deleted.")
                        ClearMe()
                        GetSalesManDetails()
                        grdSalesMan.Focus()
                        ShowSalesManDetail(grdSalesMan.CurrentRow.Index)
                    Else
                        MessageBox.Show("Error in deleting Salesman detail.")
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmCategory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F6
                btnNew_Click(sender, e)
            Case Keys.F7
                btnSave_Click(sender, e)
            Case Keys.F8
                If btnDelete.Enabled Then btnDelete_Click(sender, e)
            Case Keys.Escape
                btnClose_Click(sender, e)
            Case Keys.Return
                SendKeys.Send("{Tab}")
            Case Keys.Up
                SendKeys.Send("+{Tab}")
            Case Keys.Down
                SendKeys.Send("{Tab}")
        End Select
    End Sub

    'Private Sub txtSalesManCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtSalesManCode.Validating
    '    If txtSalesManCode.Text.Trim = "" Then
    '        ErrP.SetError(txtSalesManCode, "Should not be empty.")
    '        txtSalesManCode.Focus()
    '    Else
    '        ErrP.Dispose()
    '    End If
    'End Sub

    'Private Sub txtSalesManName_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtSalesManName.Validating
    '    If txtSalesManName.Text.Trim = "" Then
    '        ErrP.SetError(txtSalesManName, "Should not be empty.")
    '        txtSalesManName.Focus()
    '    Else
    '        ErrP.Dispose()
    '    End If
    'End Sub
    Private Sub GetSalesManDetails()
        Try
            Dim dsSalesman As New DataSet
            Dim iCnt As Long
            dsSalesman = objSalesman.GetSalesMan
            If Not IsNothing(dsSalesman) Then
                If dsSalesman.Tables("SalesMan").Rows.Count > 0 Then
                    grdSalesMan.RowCount = 0
                    For iCnt = 0 To dsSalesman.Tables("SalesMan").Rows.Count - 1
                        grdSalesMan.Rows.Add()
                        fnSetdgvText(grdSalesMan, iCnt, 0, Trim(dsSalesman.Tables("SalesMan").Rows(iCnt)("SalesManCode") & ""))
                        fnSetdgvText(grdSalesMan, iCnt, 1, Trim(dsSalesman.Tables("SalesMan").Rows(iCnt)("SalesManName") & ""))
                        fnSetdgvText(grdSalesMan, iCnt, 2, Trim(dsSalesman.Tables("SalesMan").Rows(iCnt)("Password") & ""))
                        fnSetdgvText(grdSalesMan, iCnt, 3, Trim(dsSalesman.Tables("SalesMan").Rows(iCnt)("Remarks") & ""))
                        fnSetdgvText(grdSalesMan, iCnt, 4, Trim(dsSalesman.Tables("SalesMan").Rows(iCnt)("SortOrder") & ""))
                        fnSetdgvText(grdSalesMan, iCnt, 5, Trim(dsSalesman.Tables("SalesMan").Rows(iCnt)("ShowInReport") & ""))
                        fnSetdgvText(grdSalesMan, iCnt, 6, Trim(dsSalesman.Tables("SalesMan").Rows(iCnt)("AllowSalesReturn") & ""))
                        fnSetdgvText(grdSalesMan, iCnt, 7, Trim(dsSalesman.Tables("SalesMan").Rows(iCnt)("SalesManGroup") & ""))
                    Next
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ShowSalesManDetail(ByVal Row As Integer)
        Try
            btnDelete.Enabled = True
            If Row >= 0 Then
                txtSalesManCode.Text = fnGetdgvText(grdSalesMan, Row, 0)
                txtSalesManCode.Enabled = False
                txtSalesManName.Text = fnGetdgvText(grdSalesMan, Row, 1)
                txtPassword.Text = fnGetdgvText(grdSalesMan, Row, 2)
                txtRemarks.Text = fnGetdgvText(grdSalesMan, Row, 3)
                txtSortOrder.Text = fnGetdgvText(grdSalesMan, Row, 4)
                If Val(fnGetdgvText(grdSalesMan, Row, 5)) = 1 Then
                    chkShowInReport.Checked = True
                Else
                    chkShowInReport.Checked = False
                End If
                If Val(fnGetdgvText(grdSalesMan, Row, 6)) = 1 Then
                    chkAllowSalesReturn.Checked = True
                Else
                    chkAllowSalesReturn.Checked = False
                End If
                cboSalesManGroup.Text = fnGetdgvText(grdSalesMan, Row, 7)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    'Private Sub grdSalesMan_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_ClickEvent)
    '    ShowSalesManDetail(e.row)
    'End Sub

    'Private Sub grdSaleman_LeaveRow(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_LeaveRowEvent)
    '    ShowSalesManDetail(e.newRow)
    'End Sub
    Private Sub ClearMe()
        txtSalesManCode.Clear()
        txtSalesManName.Clear()
        txtPassword.Clear()
        txtRemarks.Clear()
        txtSortOrder.Clear()
        cboSalesManGroup.SelectedIndex = 1
        chkShowInReport.Checked = False
        chkAllowSalesReturn.Checked = False
        btnDelete.Enabled = False
        txtSalesManCode.Enabled = True
        txtSalesManCode.Focus()
    End Sub
    Private Sub txtSalesManCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSalesManCode.LostFocus
        Try
            Dim i As Long
            Dim dsSalesman As New DataSet
            Dim iCnt As Long
            dsSalesman = objSalesman.GetSalesManwithCode(txtSalesManCode.Text.Trim)
            If Not IsNothing(dsSalesman) Then
                If dsSalesman.Tables("SalesMan").Rows.Count > 0 Then
                    txtSalesManCode.Text = Trim(dsSalesman.Tables("SalesMan").Rows(0)("SalesManCode") & "")
                    txtSalesManName.Text = Trim(dsSalesman.Tables("SalesMan").Rows(0)("SalesManName") & "")
                    txtPassword.Text = Trim(dsSalesman.Tables("SalesMan").Rows(0)("Password") & "")
                    txtRemarks.Text = Trim(dsSalesman.Tables("SalesMan").Rows(0)("Remarks") & "")
                    txtSortOrder.Text = Val(dsSalesman.Tables("SalesMan").Rows(0)("SortOrder") & "")
                    If Val(dsSalesman.Tables("SalesMan").Rows(0)("ShowInReport") & "") Then
                        chkShowInReport.Checked = True
                    Else
                        chkShowInReport.Checked = False
                    End If
                    If Val(dsSalesman.Tables("SalesMan").Rows(0)("AllowSalesReturn") & "") Then
                        chkAllowSalesReturn.Checked = True
                    Else
                        chkAllowSalesReturn.Checked = False
                    End If
                    cboSalesManGroup.Text = Trim(dsSalesman.Tables("SalesMan").Rows(0)("SalesManGroup") & "")
                End If
                'For i = 0 To grdSalesMan.RowCount - 1
                '    If Trim(fnGetdgvText(grdSalesMan, i, 1)) = txtSalesManCode.Text.Trim Then
                '        grdSalesMan.Row = i
                '        grdSalesMan.Col = 1
                '        grdSalesMan.Action = FPSpread.ActionConstants.ActionActiveCell
                '        Exit For
                '    End If
                'Next
                    txtSalesManCode.Enabled = False
                    txtSalesManName.Focus()
                End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function subSortOrderExists(ByVal sSalesmanCode As String, ByVal nSortOrder As Integer) As Boolean
        subSortOrderExists = False
        Dim nRow As Long
        For nRow = 0 To grdSalesMan.RowCount - 1
            If Trim(fnGetdgvText(grdSalesMan, nRow, SalesManCode)).ToUpper <> sSalesmanCode.ToUpper Then
                If Val(fnGetdgvText(grdSalesMan, nRow, cSortOrder)) = nSortOrder Then
                    Exit Function
                End If
            End If
        Next nRow
        subSortOrderExists = True
    End Function

    Private Sub lblAllowSR_Click(sender As System.Object, e As System.EventArgs) Handles lblAllowSR.Click

    End Sub

    Private Sub grdSalesMan_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSalesMan.CellClick
        ShowSalesManDetail(grdSalesMan.CurrentRow.Index)
    End Sub

    Private Sub grdSalesMan_CurrentCellChanged(sender As Object, e As System.EventArgs) Handles grdSalesMan.CurrentCellChanged
        'ShowSalesManDetail(grdSalesMan.CurrentRow.Index)
    End Sub

    Private Sub grdSalesMan_PreviewKeyDown(sender As Object, e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles grdSalesMan.PreviewKeyDown

    End Sub

    Private Sub grdSalesMan_RowLeave(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSalesMan.RowLeave

    End Sub

    Private Sub grdSalesMan_RowStateChanged(sender As Object, e As System.Windows.Forms.DataGridViewRowStateChangedEventArgs) Handles grdSalesMan.RowStateChanged
        'ShowSalesManDetail(grdSalesMan.CurrentRow.Index)
    End Sub

    Private Sub grdSalesMan_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSalesMan.CellContentClick

    End Sub
End Class
