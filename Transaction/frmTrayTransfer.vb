Public Class frmTrayTransfer
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
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnTraySearch As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents txtInventoryCode As System.Windows.Forms.TextBox
    Friend WithEvents txtItemDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtOldTray As System.Windows.Forms.TextBox
    Friend WithEvents txtTrayDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtTrayID As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents btnOldTraySearch As System.Windows.Forms.Button
    Friend WithEvents pnlInventory As System.Windows.Forms.Panel
    Friend WithEvents btnPanelClose As System.Windows.Forms.Button
    Friend WithEvents chkSelectAll As System.Windows.Forms.CheckBox
    Friend WithEvents btnPanelOK As System.Windows.Forms.Button
    Friend WithEvents grdInventory As AxFPSpread.AxvaSpread
    Friend WithEvents btnInventorySearch As System.Windows.Forms.Button
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents txtBarCode As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTrayTransfer))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnOldTraySearch = New System.Windows.Forms.Button()
        Me.btnInventorySearch = New System.Windows.Forms.Button()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnTraySearch = New System.Windows.Forms.Button()
        Me.txtTrayID = New System.Windows.Forms.TextBox()
        Me.txtOldTray = New System.Windows.Forms.TextBox()
        Me.txtTrayDescription = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtInventoryCode = New System.Windows.Forms.TextBox()
        Me.txtItemDescription = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.pnlInventory = New System.Windows.Forms.Panel()
        Me.txtBarCode = New System.Windows.Forms.TextBox()
        Me.btnPanelClose = New System.Windows.Forms.Button()
        Me.chkSelectAll = New System.Windows.Forms.CheckBox()
        Me.btnPanelOK = New System.Windows.Forms.Button()
        Me.grdInventory = New AxFPSpread.AxvaSpread()
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.Panel1.SuspendLayout()
        Me.pnlInventory.SuspendLayout()
        CType(Me.grdInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.btnOldTraySearch)
        Me.Panel1.Controls.Add(Me.btnInventorySearch)
        Me.Panel1.Controls.Add(Me.txtRemarks)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.btnTraySearch)
        Me.Panel1.Controls.Add(Me.txtTrayID)
        Me.Panel1.Controls.Add(Me.txtOldTray)
        Me.Panel1.Controls.Add(Me.txtTrayDescription)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtInventoryCode)
        Me.Panel1.Controls.Add(Me.txtItemDescription)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(20, 106)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(412, 248)
        Me.Panel1.TabIndex = 0
        '
        'btnOldTraySearch
        '
        Me.btnOldTraySearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnOldTraySearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOldTraySearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOldTraySearch.Location = New System.Drawing.Point(272, 24)
        Me.btnOldTraySearch.Name = "btnOldTraySearch"
        Me.btnOldTraySearch.Size = New System.Drawing.Size(24, 20)
        Me.btnOldTraySearch.TabIndex = 45
        Me.btnOldTraySearch.Text = "..."
        Me.btnOldTraySearch.UseVisualStyleBackColor = False
        '
        'btnInventorySearch
        '
        Me.btnInventorySearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnInventorySearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInventorySearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInventorySearch.Location = New System.Drawing.Point(272, 56)
        Me.btnInventorySearch.Name = "btnInventorySearch"
        Me.btnInventorySearch.Size = New System.Drawing.Size(24, 20)
        Me.btnInventorySearch.TabIndex = 44
        Me.btnInventorySearch.Text = "..."
        Me.btnInventorySearch.UseVisualStyleBackColor = False
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(160, 184)
        Me.txtRemarks.MaxLength = 50
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(240, 48)
        Me.txtRemarks.TabIndex = 42
        Me.txtRemarks.Tag = "NE;"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(24, 184)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 20)
        Me.Label6.TabIndex = 43
        Me.Label6.Text = "Remarks"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(24, 128)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 20)
        Me.Label5.TabIndex = 41
        Me.Label5.Text = "Tray ID"
        '
        'btnTraySearch
        '
        Me.btnTraySearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnTraySearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTraySearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTraySearch.Location = New System.Drawing.Point(272, 128)
        Me.btnTraySearch.Name = "btnTraySearch"
        Me.btnTraySearch.Size = New System.Drawing.Size(24, 20)
        Me.btnTraySearch.TabIndex = 40
        Me.btnTraySearch.Text = "..."
        Me.btnTraySearch.UseVisualStyleBackColor = False
        '
        'txtTrayID
        '
        Me.txtTrayID.BackColor = System.Drawing.Color.White
        Me.txtTrayID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTrayID.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTrayID.Location = New System.Drawing.Point(160, 128)
        Me.txtTrayID.MaxLength = 20
        Me.txtTrayID.Name = "txtTrayID"
        Me.txtTrayID.Size = New System.Drawing.Size(112, 20)
        Me.txtTrayID.TabIndex = 39
        Me.txtTrayID.Tag = "NE;"
        '
        'txtOldTray
        '
        Me.txtOldTray.BackColor = System.Drawing.Color.White
        Me.txtOldTray.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOldTray.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOldTray.Location = New System.Drawing.Point(160, 24)
        Me.txtOldTray.MaxLength = 20
        Me.txtOldTray.Name = "txtOldTray"
        Me.txtOldTray.ReadOnly = True
        Me.txtOldTray.Size = New System.Drawing.Size(112, 20)
        Me.txtOldTray.TabIndex = 35
        Me.txtOldTray.Tag = "NE;"
        '
        'txtTrayDescription
        '
        Me.txtTrayDescription.BackColor = System.Drawing.Color.White
        Me.txtTrayDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTrayDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTrayDescription.Location = New System.Drawing.Point(160, 160)
        Me.txtTrayDescription.MaxLength = 50
        Me.txtTrayDescription.Name = "txtTrayDescription"
        Me.txtTrayDescription.ReadOnly = True
        Me.txtTrayDescription.Size = New System.Drawing.Size(240, 20)
        Me.txtTrayDescription.TabIndex = 36
        Me.txtTrayDescription.Tag = "NE;"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(24, 160)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 20)
        Me.Label3.TabIndex = 38
        Me.Label3.Text = "Tray Description"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(24, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(96, 20)
        Me.Label4.TabIndex = 37
        Me.Label4.Text = "Old Tray"
        '
        'txtInventoryCode
        '
        Me.txtInventoryCode.BackColor = System.Drawing.Color.White
        Me.txtInventoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInventoryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryCode.Location = New System.Drawing.Point(160, 56)
        Me.txtInventoryCode.MaxLength = 20
        Me.txtInventoryCode.Name = "txtInventoryCode"
        Me.txtInventoryCode.Size = New System.Drawing.Size(112, 20)
        Me.txtInventoryCode.TabIndex = 31
        Me.txtInventoryCode.Tag = "NE;"
        '
        'txtItemDescription
        '
        Me.txtItemDescription.BackColor = System.Drawing.Color.White
        Me.txtItemDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemDescription.Location = New System.Drawing.Point(160, 88)
        Me.txtItemDescription.MaxLength = 50
        Me.txtItemDescription.Name = "txtItemDescription"
        Me.txtItemDescription.ReadOnly = True
        Me.txtItemDescription.Size = New System.Drawing.Size(240, 20)
        Me.txtItemDescription.TabIndex = 32
        Me.txtItemDescription.Tag = "NE;"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(24, 88)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 20)
        Me.Label2.TabIndex = 34
        Me.Label2.Text = "Item Description"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(24, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 20)
        Me.Label1.TabIndex = 33
        Me.Label1.Text = "Inventory Code"
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(356, 364)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 35
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.Location = New System.Drawing.Point(274, 364)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 32
        Me.btnSave.Text = "Save(F7)"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(192, 364)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(75, 23)
        Me.btnNew.TabIndex = 33
        Me.btnNew.Text = "Clear (F6)"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'pnlInventory
        '
        Me.pnlInventory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlInventory.Controls.Add(Me.txtBarCode)
        Me.pnlInventory.Controls.Add(Me.btnPanelClose)
        Me.pnlInventory.Controls.Add(Me.chkSelectAll)
        Me.pnlInventory.Controls.Add(Me.btnPanelOK)
        Me.pnlInventory.Controls.Add(Me.grdInventory)
        Me.pnlInventory.Location = New System.Drawing.Point(8, 102)
        Me.pnlInventory.Name = "pnlInventory"
        Me.pnlInventory.Size = New System.Drawing.Size(431, 256)
        Me.pnlInventory.TabIndex = 136
        Me.pnlInventory.Visible = False
        '
        'txtBarCode
        '
        Me.txtBarCode.BackColor = System.Drawing.Color.White
        Me.txtBarCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBarCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBarCode.Location = New System.Drawing.Point(96, 224)
        Me.txtBarCode.MaxLength = 20
        Me.txtBarCode.Name = "txtBarCode"
        Me.txtBarCode.Size = New System.Drawing.Size(112, 20)
        Me.txtBarCode.TabIndex = 140
        Me.txtBarCode.Tag = "NE;"
        '
        'btnPanelClose
        '
        Me.btnPanelClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPanelClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPanelClose.Image = CType(resources.GetObject("btnPanelClose.Image"), System.Drawing.Image)
        Me.btnPanelClose.Location = New System.Drawing.Point(320, 224)
        Me.btnPanelClose.Name = "btnPanelClose"
        Me.btnPanelClose.Size = New System.Drawing.Size(92, 23)
        Me.btnPanelClose.TabIndex = 139
        Me.btnPanelClose.Text = "Close"
        Me.btnPanelClose.UseVisualStyleBackColor = False
        '
        'chkSelectAll
        '
        Me.chkSelectAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSelectAll.Location = New System.Drawing.Point(8, 224)
        Me.chkSelectAll.Name = "chkSelectAll"
        Me.chkSelectAll.Size = New System.Drawing.Size(80, 24)
        Me.chkSelectAll.TabIndex = 138
        Me.chkSelectAll.Text = "Select All"
        '
        'btnPanelOK
        '
        Me.btnPanelOK.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPanelOK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPanelOK.Image = CType(resources.GetObject("btnPanelOK.Image"), System.Drawing.Image)
        Me.btnPanelOK.Location = New System.Drawing.Point(224, 224)
        Me.btnPanelOK.Name = "btnPanelOK"
        Me.btnPanelOK.Size = New System.Drawing.Size(92, 23)
        Me.btnPanelOK.TabIndex = 136
        Me.btnPanelOK.Text = "OK"
        Me.btnPanelOK.UseVisualStyleBackColor = False
        '
        'grdInventory
        '
        Me.grdInventory.Location = New System.Drawing.Point(8, 3)
        Me.grdInventory.Name = "grdInventory"
        Me.grdInventory.OcxState = CType(resources.GetObject("grdInventory.OcxState"), System.Windows.Forms.AxHost.State)
        Me.grdInventory.Size = New System.Drawing.Size(400, 204)
        Me.grdInventory.TabIndex = 135
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(3, -4)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 137
        Me.pic.TabStop = False
        '
        'frmTrayTransfer
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(450, 399)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.pnlInventory)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmTrayTransfer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Tray Transfer"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlInventory.ResumeLayout(False)
        Me.pnlInventory.PerformLayout()
        CType(Me.grdInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim bIsCustomerTray As Boolean
    Dim objClsTray As New clsTray
    Dim objClsInventory As New clsInventory
    Dim sInventoryCode As String
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        subClearMe()
    End Sub

    Private Sub btnTraySearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTraySearch.Click
        Try
            Dim frmHelpTray As New frmHelpSearch(0, 0, "tblTraymaster", "L", 4, "TrayName", "", "TrayId", "TrayId", 80, "TrayName", "TrayName", 180, "SalesManName", "SalesManName", 80, "IsCustomerTray", "IsCustomerTray", 80)
            frmHelpTray.ShowDialog()
            txtTrayID.Text = frmHelpTray.Var1.Trim
            txtTrayDescription.Text = frmHelpTray.Var2.Trim
            bIsCustomerTray = IIf(frmHelpTray.Var3.Trim = "Y", True, False)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subClearMe()
        Try
            txtInventoryCode.Clear()
            txtInventoryCode.ReadOnly = False
            txtItemDescription.Clear()
            txtOldTray.Clear()
            txtTrayID.Clear()
            txtTrayDescription.Clear()
            txtRemarks.Clear()
            bIsCustomerTray = Nothing
            txtOldTray.Focus()
            grdInventory.MaxRows = 0
            chkSelectAll.Checked = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim i As Integer
        Try
            Cursor = Cursors.WaitCursor
            If fncValidateMe() Then
                '''If objClsTray.TrayTransfer(txtTrayID.Text.Trim, txtInventoryCode.Text.Trim, txtOldTray.Text.Trim, txtRemarks.Text.Trim) Then
                '''    MessageBox.Show("Tray Transferred successfully.")
                '''    subClearMe()
                '''Else
                '''    MessageBox.Show("Error in updation.")
                '''End If


                For i = 1 To grdInventory.MaxRows
                    If Trim(GetText(grdInventory, i, 1)) = 1 Then
                        If objClsTray.TrayTransfer(txtTrayID.Text.Trim, Trim(GetText(grdInventory, i, 2)), txtOldTray.Text.Trim, txtRemarks.Text.Trim) Then

                            If objClsTray.RFIDItemStatusUpdate(txtTrayID.Text.Trim, Trim(GetText(grdInventory, i, 2))) Then
                            Else
                                MessageBox.Show("Error in updation.")
                                Exit Sub
                            End If


                        Else
                            MessageBox.Show("Error in updation.")
                            Exit Sub
                        End If
                    End If

                Next
                MessageBox.Show("Tray Transferred successfully.")
                subClearMe()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Function fncValidateMe()
        Dim bStatus As Boolean
        Try
            bStatus = False
            '''If txtInventoryCode.Text.Trim = "" Then
            '''    MessageBox.Show("Please enter Inventorycode to Transfer.")
            '''    txtInventoryCode.Focus()
            '''    Exit Function
            '''End If

            If Trim(sInventoryCode) = "" Then
                MessageBox.Show("Please enter Inventorycode to Transfer.")
                txtInventoryCode.Focus()
                Exit Function
            End If
            If txtTrayID.Text.Trim = "" Then
                MessageBox.Show("Please select New tray to Transfer.")
                txtTrayID.Focus()
                Exit Function
            End If
            '''If bIsCustomerTray Then
            '''    If txtRemarks.Text.Trim = "" Then
            '''        MessageBox.Show("Please enter remarks.")
            '''        txtRemarks.Focus()
            '''        Exit Function
            '''    End If
            '''End If
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncValidateMe = bStatus
        End Try
    End Function


    Private Sub frmTrayTransfer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F7
                If btnSave.Enabled Then btnSave_Click(sender, e)
            Case Keys.F6
                subClearMe()
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

    Private Sub frmTrayTransfer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub subGetInventoryDetail()
        Dim dsInv As New DataSet
        Try
            Cursor = Cursors.WaitCursor
            If txtInventoryCode.Text.Trim <> "" Then
                dsInv = objClsInventory.GetInventoryDetail(txtInventoryCode.Text.Trim)
                If dsInv.Tables(0).Rows.Count > 0 Then
                    If dsInv.Tables(0).Rows(0)("QtyOnHand") <> 0 Then
                        txtItemDescription.Text = Trim(dsInv.Tables(0).Rows(0)("Description") & "")
                        txtOldTray.Text = Trim(dsInv.Tables(0).Rows(0)("TrayId") & "")
                        txtTrayID.Focus()
                        txtInventoryCode.ReadOnly = True
                    Else
                        MessageBox.Show("Inventory already sold")
                        subClearMe()
                        txtInventoryCode.Focus()
                        Exit Sub
                    End If
                Else
                    MessageBox.Show("Inventory does not exists")
                    subClearMe()
                    txtInventoryCode.Focus()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub txtInventoryCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInventoryCode.LostFocus
        ''subGetInventoryDetail()
    End Sub

    Private Sub txtInventoryCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInventoryCode.TextChanged

    End Sub

    Private Sub btnOldTraySearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOldTraySearch.Click
        Try
            Dim frmHelpTray As New frmHelpSearch(0, 0, "tblTraymaster", "L", 4, "TrayName", "", "TrayId", "TrayId", 80, "TrayName", "TrayName", 180, "SalesManName", "SalesManName", 80, "IsCustomerTray", "IsCustomerTray", 80)
            frmHelpTray.ShowDialog()
            txtOldTray.Text = frmHelpTray.Var1.Trim
            '''txtTrayDescription.Text = frmHelpTray.Var2.Trim
            '''bIsCustomerTray = IIf(frmHelpTray.Var3.Trim = "Y", True, False)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subGetInventoryByTray()
        Try
            Dim icnt As Long
            Dim sDate As Date
            Dim dsPur As New DataSet
            'Dim SBll As Purchase
            'SBll = New Purchase
            pnlInventory.Visible = True
            dsPur = objClsInventory.GetInventoryDetailByTray(txtOldTray.Text.Trim)
            If dsPur.Tables(0).Rows.Count > 0 Then
                grdInventory.MaxRows = 0
                'txtPayable.Text = ""
                For icnt = 0 To dsPur.Tables(0).Rows.Count - 1
                    grdInventory.MaxRows = grdInventory.MaxRows + 1
                    grdInventory.Row = grdInventory.MaxRows
                    Call SetText(grdInventory, grdInventory.Row, 1, 0)
                    Call SetText(grdInventory, grdInventory.Row, 2, Trim(dsPur.Tables(0).Rows(icnt)("InventoryCode") & ""))
                    Call SetText(grdInventory, grdInventory.Row, 3, Trim(dsPur.Tables(0).Rows(icnt)("Description") & ""))
                    Call SetText(grdInventory, grdInventory.Row, 4, Val(dsPur.Tables(0).Rows(icnt)("Weight") & ""))
                Next icnt
                grdInventory.Row = 1
                grdInventory.Col = 1
                grdInventory.Action = 0
            Else
                '''grdUnpaidBill.MaxRows = 0
                '''MessageBox.Show("No Balance for this Supplier")
                '''txtsupplierCode.Text = ""
                '''txtSupplierName.Text = ""
                '''txtsupplierCode.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub btnInventorySearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInventorySearch.Click
        If Trim(txtOldTray.Text) <> "" Then
            subGetInventoryByTray()
            If txtBarCode.Enabled Then txtBarCode.Focus()
        End If
    End Sub

    Private Sub btnPanelClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPanelClose.Click
        pnlInventory.Visible = False
    End Sub
    Private Sub chkSelectAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSelectAll.CheckedChanged
        Dim i As Long
        For i = 1 To grdInventory.MaxRows
            'If Trim(GetText(grdInvoice, i, 1)) = 1 Then
            '    Call SetText(grdInvoice, i, 1, 0)
            'Else
            If chkSelectAll.Checked = True Then
                Call SetText(grdInventory, i, 1, 1)
            Else
                Call SetText(grdInventory, i, 1, 0)
            End If
            'End If
        Next

    End Sub

    Private Sub grdInventory_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_ClickEvent) Handles grdInventory.ClickEvent
        Dim i As Long
        ' If grdInvoice.Col = 1 Then
        If Trim(GetText(grdInventory, grdInventory.ActiveRow, 1)) = 0 Or Trim(GetText(grdInventory, grdInventory.ActiveRow, 1)) = "" Then
            'If grdInvoice.ActiveRow = grdInvoice.MaxRows Then
            '    Call SetText(grdInvoice, grdInvoice.ActiveRow, 1, 0)
            'Else
            '    Call SetText(grdInvoice, grdInvoice.ActiveRow, 1, 1)
            'End If
            Call SetText(grdInventory, grdInventory.ActiveRow, 1, 1)

        Else
            Call SetText(grdInventory, grdInventory.ActiveRow, 1, 0)

        End If
    End Sub

    Private Sub btnPanelOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPanelOK.Click
        Dim i As Long
        '  SInvoiceNo = ""
        sInventoryCode = ""

        For i = 1 To grdInventory.MaxRows
            If Trim(GetText(grdInventory, i, 1)) = 1 Then
                ''SInvoiceNo = SInvoiceNo & fncFormatSQLText(GetText(grdInvoice, i, 2)) & ","
                sInventoryCode = sInventoryCode & "," & fncFormatSQLText(GetText(grdInventory, i, 2))
                '''  sInvNo = sInvNo & "," & fncFormatSQLText(GetText(grdInventory, i, 2))
            End If
        Next
        'SInvoiceNo = Strings.Left(SInvoiceNo, Len(SInvoiceNo) - 1)

        sInventoryCode = Strings.Right(sInventoryCode, Len(sInventoryCode) - 1)


        pnlInventory.Visible = False
    End Sub


    Private Sub txtBarCode_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBarCode.Leave
        Dim i As Long

        For i = 1 To grdInventory.MaxRows
            'If Trim(GetText(grdInvoice, i, 1)) = 1 Then
            '    Call SetText(grdInvoice, i, 1, 0)
            'Else
            If Trim(GetText(grdInventory, i, 2)) = Trim(txtBarCode.Text) Then
                Call SetText(grdInventory, i, 1, 1)
                'Else
                ''    Call SetText(grdInventory, i, 1, 0)
            End If
            'End If
        Next

        txtBarCode.Text = ""
        If txtBarCode.Enabled Then txtBarCode.Focus()
    End Sub

    Private Sub Panel1_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub pic_Click(sender As System.Object, e As System.EventArgs)
    End Sub

    Private Sub pic_Click_1(sender As System.Object, e As System.EventArgs) Handles pic.Click

    End Sub
End Class
