Public Class frmTray
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
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents txtTrayID As System.Windows.Forms.TextBox
    Friend WithEvents txtTrayName As System.Windows.Forms.TextBox
    Friend WithEvents txtSalesmanName As System.Windows.Forms.TextBox
    Friend WithEvents txtSalesmanID As System.Windows.Forms.TextBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents ErrP As System.Windows.Forms.ErrorProvider
    Friend WithEvents chkIsCustomerTray As System.Windows.Forms.CheckBox
    Friend WithEvents btnSalesManSearch As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnTrayGroupSearch As System.Windows.Forms.Button
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents grdTray As System.Windows.Forms.DataGridView
    Friend WithEvents cTrayId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cTrayName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents sSalesManID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSalesmanName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cIscustomerTray As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cTrayGroup As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtTrayGroup As System.Windows.Forms.TextBox

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTray))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnTrayGroupSearch = New System.Windows.Forms.Button()
        Me.txtTrayGroup = New System.Windows.Forms.TextBox()
        Me.chkIsCustomerTray = New System.Windows.Forms.CheckBox()
        Me.txtSalesmanName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTrayID = New System.Windows.Forms.TextBox()
        Me.txtTrayName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnSalesManSearch = New System.Windows.Forms.Button()
        Me.txtSalesmanID = New System.Windows.Forms.TextBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.ErrP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.grdTray = New System.Windows.Forms.DataGridView()
        Me.cTrayId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cTrayName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.sSalesManID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cSalesmanName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cIscustomerTray = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cTrayGroup = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1.SuspendLayout()
        CType(Me.ErrP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdTray, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Window
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.btnTrayGroupSearch)
        Me.Panel1.Controls.Add(Me.txtTrayGroup)
        Me.Panel1.Controls.Add(Me.chkIsCustomerTray)
        Me.Panel1.Controls.Add(Me.txtSalesmanName)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtTrayID)
        Me.Panel1.Controls.Add(Me.txtTrayName)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.btnSalesManSearch)
        Me.Panel1.Controls.Add(Me.txtSalesmanID)
        Me.Panel1.Location = New System.Drawing.Point(431, 112)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(389, 216)
        Me.Panel1.TabIndex = 15
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(16, 90)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 23)
        Me.Label5.TabIndex = 79
        Me.Label5.Text = "Tray Group"
        '
        'btnTrayGroupSearch
        '
        Me.btnTrayGroupSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnTrayGroupSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTrayGroupSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTrayGroupSearch.Location = New System.Drawing.Point(313, 91)
        Me.btnTrayGroupSearch.Name = "btnTrayGroupSearch"
        Me.btnTrayGroupSearch.Size = New System.Drawing.Size(23, 21)
        Me.btnTrayGroupSearch.TabIndex = 78
        Me.btnTrayGroupSearch.Text = "..."
        Me.btnTrayGroupSearch.UseVisualStyleBackColor = False
        '
        'txtTrayGroup
        '
        Me.txtTrayGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTrayGroup.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTrayGroup.Location = New System.Drawing.Point(136, 91)
        Me.txtTrayGroup.MaxLength = 20
        Me.txtTrayGroup.Name = "txtTrayGroup"
        Me.txtTrayGroup.Size = New System.Drawing.Size(179, 21)
        Me.txtTrayGroup.TabIndex = 77
        Me.txtTrayGroup.Tag = "NE;"
        '
        'chkIsCustomerTray
        '
        Me.chkIsCustomerTray.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIsCustomerTray.ForeColor = System.Drawing.Color.Black
        Me.chkIsCustomerTray.Location = New System.Drawing.Point(16, 189)
        Me.chkIsCustomerTray.Name = "chkIsCustomerTray"
        Me.chkIsCustomerTray.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkIsCustomerTray.Size = New System.Drawing.Size(136, 16)
        Me.chkIsCustomerTray.TabIndex = 76
        Me.chkIsCustomerTray.Text = "Tray for outside demo"
        Me.chkIsCustomerTray.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSalesmanName
        '
        Me.txtSalesmanName.BackColor = System.Drawing.Color.White
        Me.txtSalesmanName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesmanName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesmanName.Location = New System.Drawing.Point(136, 157)
        Me.txtSalesmanName.MaxLength = 50
        Me.txtSalesmanName.Name = "txtSalesmanName"
        Me.txtSalesmanName.ReadOnly = True
        Me.txtSalesmanName.Size = New System.Drawing.Size(200, 21)
        Me.txtSalesmanName.TabIndex = 4
        Me.txtSalesmanName.Tag = "NE;"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(16, 156)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(136, 23)
        Me.Label4.TabIndex = 32
        Me.Label4.Text = "Salesman Name"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 123)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 23)
        Me.Label3.TabIndex = 31
        Me.Label3.Text = "Salesman ID"
        '
        'txtTrayID
        '
        Me.txtTrayID.BackColor = System.Drawing.Color.White
        Me.txtTrayID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTrayID.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTrayID.Location = New System.Drawing.Point(136, 25)
        Me.txtTrayID.MaxLength = 20
        Me.txtTrayID.Name = "txtTrayID"
        Me.txtTrayID.Size = New System.Drawing.Size(112, 21)
        Me.txtTrayID.TabIndex = 0
        Me.txtTrayID.Tag = "NE;"
        '
        'txtTrayName
        '
        Me.txtTrayName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTrayName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTrayName.Location = New System.Drawing.Point(136, 58)
        Me.txtTrayName.MaxLength = 50
        Me.txtTrayName.Name = "txtTrayName"
        Me.txtTrayName.Size = New System.Drawing.Size(200, 21)
        Me.txtTrayName.TabIndex = 1
        Me.txtTrayName.Tag = "NE;"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 57)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 23)
        Me.Label2.TabIndex = 30
        Me.Label2.Text = "Tray Name"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 23)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "Tray ID"
        '
        'btnSalesManSearch
        '
        Me.btnSalesManSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSalesManSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSalesManSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSalesManSearch.Location = New System.Drawing.Point(313, 124)
        Me.btnSalesManSearch.Name = "btnSalesManSearch"
        Me.btnSalesManSearch.Size = New System.Drawing.Size(23, 21)
        Me.btnSalesManSearch.TabIndex = 3
        Me.btnSalesManSearch.Text = "..."
        Me.btnSalesManSearch.UseVisualStyleBackColor = False
        '
        'txtSalesmanID
        '
        Me.txtSalesmanID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesmanID.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesmanID.Location = New System.Drawing.Point(136, 124)
        Me.txtSalesmanID.MaxLength = 20
        Me.txtSalesmanID.Name = "txtSalesmanID"
        Me.txtSalesmanID.Size = New System.Drawing.Size(179, 21)
        Me.txtSalesmanID.TabIndex = 2
        Me.txtSalesmanID.Tag = "NE;"
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(492, 348)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 40)
        Me.btnSave.TabIndex = 6
        Me.btnSave.Text = "Save(F7)"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDelete.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(606, 348)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(100, 40)
        Me.btnDelete.TabIndex = 7
        Me.btnDelete.Text = "Delete(F8)"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnNew.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Location = New System.Drawing.Point(378, 348)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(100, 40)
        Me.btnNew.TabIndex = 5
        Me.btnNew.Text = "New(F6)"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(720, 348)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'ErrP
        '
        Me.ErrP.ContainerControl = Me
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(8, 0)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 86
        Me.pic.TabStop = False
        '
        'grdTray
        '
        Me.grdTray.AllowUserToAddRows = False
        Me.grdTray.AllowUserToDeleteRows = False
        Me.grdTray.AllowUserToResizeColumns = False
        Me.grdTray.AllowUserToResizeRows = False
        Me.grdTray.BackgroundColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdTray.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdTray.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdTray.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cTrayId, Me.cTrayName, Me.sSalesManID, Me.cSalesmanName, Me.cIscustomerTray, Me.cTrayGroup})
        Me.grdTray.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdTray.Location = New System.Drawing.Point(4, 112)
        Me.grdTray.Name = "grdTray"
        Me.grdTray.RowHeadersVisible = False
        Me.grdTray.Size = New System.Drawing.Size(421, 216)
        Me.grdTray.TabIndex = 87
        '
        'cTrayId
        '
        Me.cTrayId.HeaderText = "Tray ID"
        Me.cTrayId.Name = "cTrayId"
        '
        'cTrayName
        '
        Me.cTrayName.HeaderText = "Tray Name"
        Me.cTrayName.Name = "cTrayName"
        Me.cTrayName.Width = 150
        '
        'sSalesManID
        '
        Me.sSalesManID.HeaderText = "Salesman ID"
        Me.sSalesManID.Name = "sSalesManID"
        Me.sSalesManID.Width = 150
        '
        'cSalesmanName
        '
        Me.cSalesmanName.HeaderText = "Salesman Name"
        Me.cSalesmanName.Name = "cSalesmanName"
        Me.cSalesmanName.Visible = False
        '
        'cIscustomerTray
        '
        Me.cIscustomerTray.HeaderText = "IsCustomerTray"
        Me.cIscustomerTray.Name = "cIscustomerTray"
        Me.cIscustomerTray.Visible = False
        '
        'cTrayGroup
        '
        Me.cTrayGroup.HeaderText = "Tray Group"
        Me.cTrayGroup.Name = "cTrayGroup"
        Me.cTrayGroup.Visible = False
        '
        'frmTray
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(7, 14)
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(845, 400)
        Me.Controls.Add(Me.grdTray)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTray"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Tray"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.ErrP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdTray, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Const TrayID = 1
    Const TrayName = 2
    Const SalesmanID = 3
    Const SalesmanName = 4
    Const IsCustomerTray = 5
    Const TrayGroup = 6
    Dim objTray As New clsTray
    Private Function ValidateMe() As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            If txtTrayID.Text.Trim = "" Then
                MessageBox.Show("TrayID should not be blank.")
                txtTrayID.Focus()
                Exit Function
            End If
            If txtTrayName.Text.Trim = "" Then
                MessageBox.Show("TrayName should not be blank.")
                txtTrayName.Focus()
                Exit Function
            End If
            If txtSalesmanID.Text.Trim = "" Then
                MessageBox.Show("SalesmanID should not be blank.")
                txtSalesmanID.Focus()
                Exit Function
            End If
            'check valid classcode

            If txtSalesmanName.Text.Trim = "" Then
                MessageBox.Show("SalesmanName should not be blank.")
                txtSalesmanName.Focus()
                Exit Function
            End If

            If txtTrayGroup.Text.Trim = "" Then
                MessageBox.Show("TrayGroup should not be blank.")
                txtTrayGroup.Focus()
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

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub btnSalesManSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalesManSearch.Click
        Try
            Dim frmSalesMan As New frmHelpSearch(0, 0, "tblSalesMan", "L", 3, "SalesManName", "", "SalesManCode", "SalesManCode", 80, "SalesManName", "SalesManName", 180, "Remarks", "Remarks", 80)
            frmSalesMan.ShowDialog()
            txtSalesmanID.Text = frmSalesMan.Var1.Trim
            txtSalesmanName.Text = frmSalesMan.Var2.Trim
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmTray_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ' subGridSetting(grdTray)
            GetTrayDetails()
            ShowTrayDetail(grdTray.CurrentRow.Index)
            grdTray.Focus()
            'txtTrayID.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        ClearMe()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim cIsCustomerTray As Char
        Try
            If ValidateMe() Then
                cIsCustomerTray = IIf(chkIsCustomerTray.Checked = True, "Y", "N")
                If objTray.TraySave(txtTrayID.Text.Trim, txtTrayName.Text.Trim, txtSalesmanID.Text.Trim, txtSalesmanName.Text.Trim, cIsCustomerTray, txtTrayGroup.Text.Trim) Then
                    MessageBox.Show("Tray detail saved sucssfully.")
                    ClearMe()
                    GetTrayDetails()
                    grdTray.Focus()
                    ShowTrayDetail(grdTray.CurrentRow.Index)
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
            If grdTray.RowCount >= 0 And grdTray.CurrentRow.Index >= 0 Then
                Dim sTrayId As String
                sTrayId = fnGetdgvText(grdTray, grdTray.CurrentRow.Index, TrayID)
                If objTray.IsTrayContainsItems(sTrayId) Then
                    MessageBox.Show("This Tray contain some item. Please move the items and try again.")
                    Exit Sub
                End If
                If MessageBox.Show("Do you want to delete " & fnGetdgvText(grdTray, grdTray.CurrentRow.Index, TrayID) & " Category?", "GoldPOS", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    If objTray.TrayDelete(sTrayId) Then
                        MessageBox.Show("Tray detail deleted.")
                        ClearMe()
                        GetTrayDetails()
                        grdTray.Focus()
                        ShowTrayDetail(grdTray.CurrentRow.Index)
                    Else
                        MessageBox.Show("Error in deleting Category detail.")
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmTray_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
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
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtTrayID_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtTrayID.Validating
        If txtTrayID.Text.Trim = "" Then
            ErrP.SetError(txtTrayID, "Should not be empty.")
            txtTrayID.Focus()
        Else
            ErrP.Dispose()
        End If
    End Sub
    Private Sub txtTrayName_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtTrayName.Validating
        If txtTrayName.Text.Trim = "" Then
            ErrP.SetError(txtTrayName, "Should not be empty.")
            txtTrayName.Focus()
        Else
            ErrP.Dispose()
        End If
    End Sub
    Private Sub txtSalesmanID_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtSalesmanID.Validating
        If txtSalesmanID.Text.Trim = "" Then
            ErrP.SetError(txtSalesmanID, "Should not be empty.")
            txtSalesmanID.Focus()
        Else
            ErrP.Dispose()
        End If
    End Sub
    Private Sub txtSalesmanName_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtSalesmanName.Validating
        If txtSalesmanName.Text.Trim = "" Then
            ErrP.SetError(txtSalesmanName, "Should not be empty.")
            txtSalesmanName.Focus()
        Else
            ErrP.Dispose()
        End If
    End Sub
    Private Sub txtTrayGroup_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtTrayGroup.Validating
        If txtTrayGroup.Text.Trim = "" Then
            ErrP.SetError(txtTrayGroup, "Should not be empty.")
            txtTrayGroup.Focus()
        Else
            ErrP.Dispose()
        End If
    End Sub
    Private Sub ClearMe()
        txtTrayID.Clear()
        txtTrayName.Clear()
        txtSalesmanID.Clear()
        txtSalesmanName.Clear()
        txtTrayGroup.Clear()
        btnDelete.Enabled = False
        txtTrayID.ReadOnly = False
        chkIsCustomerTray.Checked = False
        txtTrayID.Focus()
    End Sub
    Private Sub txtTrayID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTrayID.LostFocus
        Dim iCnt As Integer
        If txtTrayID.Text.Trim <> "" Then
            For iCnt = 0 To grdTray.RowCount
                If UCase(txtTrayID.Text.Trim) = UCase(Trim(fnGetdgvText(grdTray, iCnt, 0))) Then
                    ShowTrayDetail(iCnt)
                    Exit For
                End If
            Next
        End If
        'Try
        '    If txtTrayID.Text.Trim <> "" Then
        '        txtTrayName.Text = objTray.GetClassName(txtTrayID.Text.Trim)
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub


    Private Sub GetTrayDetails()
        Try
            Dim dsTray As New DataSet
            Dim iCnt As Long
            dsTray = objTray.GetTray()
            If Not IsNothing(dsTray) Then
                If dsTray.Tables("Tray").Rows.Count > 0 Then
                    grdTray.RowCount = 0
                    For iCnt = 0 To dsTray.Tables("Tray").Rows.Count - 1
                        grdTray.Rows.Add()
                        fnSetdgvText(grdTray, iCnt, 0, Trim(dsTray.Tables("Tray").Rows(iCnt)("TrayID") & ""))
                        fnSetdgvText(grdTray, iCnt, 1, Trim(dsTray.Tables("Tray").Rows(iCnt)("TrayName") & ""))
                        fnSetdgvText(grdTray, iCnt, 2, Trim(dsTray.Tables("Tray").Rows(iCnt)("SalesmanID") & ""))
                        fnSetdgvText(grdTray, iCnt, 3, Trim(dsTray.Tables("Tray").Rows(iCnt)("SalesmanName") & ""))
                        fnSetdgvText(grdTray, iCnt, 4, Trim(dsTray.Tables("Tray").Rows(iCnt)("IsCustomerTray") & ""))
                        fnSetdgvText(grdTray, iCnt, 5, Trim(dsTray.Tables("Tray").Rows(iCnt)("TrayGroup") & ""))
                    Next
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ShowTrayDetail(ByVal Row As Integer)
        Try
            btnDelete.Enabled = True
            If Row >= 0 Then
                txtTrayID.Text = fnGetdgvText(grdTray, Row, 0)
                txtTrayID.ReadOnly = True
                txtTrayName.Text = fnGetdgvText(grdTray, Row, 1)
                txtSalesmanID.Text = fnGetdgvText(grdTray, Row, 2)
                txtSalesmanName.Text = fnGetdgvText(grdTray, Row, 3)
                txtTrayGroup.Text = fnGetdgvText(grdTray, Row, 4)
                If Trim(fnGetdgvText(grdTray, Row, 5)) = "Y" Then
                    chkIsCustomerTray.Checked = True
                Else
                    chkIsCustomerTray.Checked = False
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtTrayID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTrayID.KeyDown
        If e.KeyCode = Keys.F1 Then btnSalesManSearch_Click(sender, e)
    End Sub
    Private Sub txtSalesmanID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSalesmanID.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then btnSalesManSearch_Click(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnTrayGroupSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTrayGroupSearch.Click
        Try
            Dim frmTrayGroup As New frmHelpSearch(0, 0, "tblTrayGroup", "L", 3, "TrayGroupName", "", "TrayGroupCode", "TrayGroupCode", 80, "TrayGroupName", "TrayGroupName", 180, "CreateUser", "CreateUser", 180)
            frmTrayGroup.ShowDialog()
            txtTrayGroup.Text = frmTrayGroup.Var1.Trim
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub grdTray_Click(sender As Object, e As System.EventArgs) Handles grdTray.Click
        ShowTrayDetail(grdTray.CurrentRow.Index)
    End Sub
End Class
