Public Class frmLocation

    Inherits System.Windows.Forms.Form
    Const LocationCode = 1
    Const LocationName = 2
    Const LocationType = 3
    Const Add1 = 4
    Const Add2 = 5
    Const Add3 = 6
    Const Add4 = 7
    Const Phone = 8
    Const Fax = 9
    Const PersonIncharge = 10
    Const Sortcode = 11
    Dim objLocation As New clsLocation
    Friend WithEvents grdLocation As System.Windows.Forms.DataGridView
    Friend WithEvents cLocationCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cLocationName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cLocationType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents sAddress1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cAddress2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cAddress3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cAddress4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cTel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cFax As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cAdminName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSortCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Dim dm As New DataManager
    Friend WithEvents cAdd1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Dim objCompany As New clsCompany
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtLocationCode As System.Windows.Forms.TextBox
    Friend WithEvents txtLocationName As System.Windows.Forms.TextBox
    Friend WithEvents lblLocationCode As System.Windows.Forms.Label
    Friend WithEvents lblLocationName As System.Windows.Forms.Label
    Friend WithEvents txtAddress1 As System.Windows.Forms.TextBox
    Friend WithEvents lblAddress1 As System.Windows.Forms.Label
    Friend WithEvents txtTelePhone As System.Windows.Forms.TextBox
    Friend WithEvents lblTelePhone As System.Windows.Forms.Label
    Friend WithEvents txtFax As System.Windows.Forms.TextBox
    Friend WithEvents lblFax As System.Windows.Forms.Label
    Friend WithEvents lblPersonInCharge As System.Windows.Forms.Label
    Friend WithEvents txtAddress2 As System.Windows.Forms.TextBox
    Friend WithEvents txtPersonIncharge As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress3 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress4 As System.Windows.Forms.TextBox
    Friend WithEvents txtSortcode As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboLocation As System.Windows.Forms.ComboBox
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLocation))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.txtLocationCode = New System.Windows.Forms.TextBox()
        Me.txtLocationName = New System.Windows.Forms.TextBox()
        Me.lblLocationCode = New System.Windows.Forms.Label()
        Me.lblLocationName = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtAddress1 = New System.Windows.Forms.TextBox()
        Me.lblAddress1 = New System.Windows.Forms.Label()
        Me.txtAddress2 = New System.Windows.Forms.TextBox()
        Me.txtTelePhone = New System.Windows.Forms.TextBox()
        Me.lblTelePhone = New System.Windows.Forms.Label()
        Me.txtFax = New System.Windows.Forms.TextBox()
        Me.lblFax = New System.Windows.Forms.Label()
        Me.lblPersonInCharge = New System.Windows.Forms.Label()
        Me.txtPersonIncharge = New System.Windows.Forms.TextBox()
        Me.txtAddress3 = New System.Windows.Forms.TextBox()
        Me.txtAddress4 = New System.Windows.Forms.TextBox()
        Me.txtSortcode = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboLocation = New System.Windows.Forms.ComboBox()
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.grdLocation = New System.Windows.Forms.DataGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cLocationCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cLocationName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cLocationType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cAdd1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cAddress2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cAddress3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cAddress4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cTel = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cFax = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cAdminName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cSortCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.Black
        Me.btnClose.Location = New System.Drawing.Point(551, 19)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 16
        Me.btnClose.Text = "Close (Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDelete.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.ForeColor = System.Drawing.Color.Black
        Me.btnDelete.Location = New System.Drawing.Point(446, 19)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(100, 40)
        Me.btnDelete.TabIndex = 14
        Me.btnDelete.Tag = "D"
        Me.btnDelete.Text = "&Delete (F8)"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.Black
        Me.btnSave.Location = New System.Drawing.Point(340, 19)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 40)
        Me.btnSave.TabIndex = 13
        Me.btnSave.Tag = "U"
        Me.btnSave.Text = "&Save (F7)"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnNew
        '
        Me.btnNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnNew.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.ForeColor = System.Drawing.Color.Black
        Me.btnNew.Location = New System.Drawing.Point(234, 19)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(100, 40)
        Me.btnNew.TabIndex = 15
        Me.btnNew.Tag = "A"
        Me.btnNew.Text = "&New (F6)"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'txtLocationCode
        '
        Me.txtLocationCode.AcceptsTab = True
        Me.txtLocationCode.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtLocationCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLocationCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtLocationCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocationCode.ForeColor = System.Drawing.Color.Black
        Me.txtLocationCode.Location = New System.Drawing.Point(482, 110)
        Me.txtLocationCode.MaxLength = 4
        Me.txtLocationCode.Name = "txtLocationCode"
        Me.txtLocationCode.Size = New System.Drawing.Size(195, 21)
        Me.txtLocationCode.TabIndex = 2
        Me.txtLocationCode.Tag = "NE"
        '
        'txtLocationName
        '
        Me.txtLocationName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLocationName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtLocationName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocationName.Location = New System.Drawing.Point(482, 134)
        Me.txtLocationName.MaxLength = 50
        Me.txtLocationName.Name = "txtLocationName"
        Me.txtLocationName.Size = New System.Drawing.Size(195, 21)
        Me.txtLocationName.TabIndex = 3
        Me.txtLocationName.Tag = "NE"
        '
        'lblLocationCode
        '
        Me.lblLocationCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationCode.ForeColor = System.Drawing.Color.Black
        Me.lblLocationCode.Location = New System.Drawing.Point(370, 110)
        Me.lblLocationCode.Name = "lblLocationCode"
        Me.lblLocationCode.Size = New System.Drawing.Size(115, 18)
        Me.lblLocationCode.TabIndex = 476180
        Me.lblLocationCode.Text = "Location Code"
        Me.lblLocationCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLocationName
        '
        Me.lblLocationName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationName.ForeColor = System.Drawing.Color.Black
        Me.lblLocationName.Location = New System.Drawing.Point(370, 134)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(115, 16)
        Me.lblLocationName.TabIndex = 476181
        Me.lblLocationName.Text = "Location Name"
        Me.lblLocationName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(-103, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(103, 17)
        Me.Label1.TabIndex = 476179
        Me.Label1.Text = "SearchBy Text"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtAddress1
        '
        Me.txtAddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAddress1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress1.Location = New System.Drawing.Point(482, 185)
        Me.txtAddress1.MaxLength = 50
        Me.txtAddress1.Name = "txtAddress1"
        Me.txtAddress1.Size = New System.Drawing.Size(195, 21)
        Me.txtAddress1.TabIndex = 5
        Me.txtAddress1.Tag = "NE"
        '
        'lblAddress1
        '
        Me.lblAddress1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddress1.ForeColor = System.Drawing.Color.Black
        Me.lblAddress1.Location = New System.Drawing.Point(370, 185)
        Me.lblAddress1.Name = "lblAddress1"
        Me.lblAddress1.Size = New System.Drawing.Size(115, 16)
        Me.lblAddress1.TabIndex = 476185
        Me.lblAddress1.Text = "Address"
        Me.lblAddress1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtAddress2
        '
        Me.txtAddress2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAddress2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress2.Location = New System.Drawing.Point(482, 209)
        Me.txtAddress2.MaxLength = 50
        Me.txtAddress2.Name = "txtAddress2"
        Me.txtAddress2.Size = New System.Drawing.Size(195, 21)
        Me.txtAddress2.TabIndex = 6
        Me.txtAddress2.Tag = "NE"
        '
        'txtTelePhone
        '
        Me.txtTelePhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTelePhone.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTelePhone.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTelePhone.Location = New System.Drawing.Point(482, 281)
        Me.txtTelePhone.MaxLength = 50
        Me.txtTelePhone.Name = "txtTelePhone"
        Me.txtTelePhone.Size = New System.Drawing.Size(195, 21)
        Me.txtTelePhone.TabIndex = 9
        Me.txtTelePhone.Tag = "NE"
        '
        'lblTelePhone
        '
        Me.lblTelePhone.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTelePhone.ForeColor = System.Drawing.Color.Black
        Me.lblTelePhone.Location = New System.Drawing.Point(370, 278)
        Me.lblTelePhone.Name = "lblTelePhone"
        Me.lblTelePhone.Size = New System.Drawing.Size(115, 18)
        Me.lblTelePhone.TabIndex = 476191
        Me.lblTelePhone.Text = "TelePhone"
        Me.lblTelePhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtFax
        '
        Me.txtFax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFax.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFax.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFax.Location = New System.Drawing.Point(482, 305)
        Me.txtFax.MaxLength = 50
        Me.txtFax.Name = "txtFax"
        Me.txtFax.Size = New System.Drawing.Size(195, 21)
        Me.txtFax.TabIndex = 10
        Me.txtFax.Tag = "NE"
        '
        'lblFax
        '
        Me.lblFax.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFax.ForeColor = System.Drawing.Color.Black
        Me.lblFax.Location = New System.Drawing.Point(370, 302)
        Me.lblFax.Name = "lblFax"
        Me.lblFax.Size = New System.Drawing.Size(115, 18)
        Me.lblFax.TabIndex = 476193
        Me.lblFax.Text = "Fax"
        Me.lblFax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPersonInCharge
        '
        Me.lblPersonInCharge.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPersonInCharge.ForeColor = System.Drawing.Color.Black
        Me.lblPersonInCharge.Location = New System.Drawing.Point(370, 326)
        Me.lblPersonInCharge.Name = "lblPersonInCharge"
        Me.lblPersonInCharge.Size = New System.Drawing.Size(115, 24)
        Me.lblPersonInCharge.TabIndex = 476195
        Me.lblPersonInCharge.Text = "Admin Name"
        Me.lblPersonInCharge.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPersonIncharge
        '
        Me.txtPersonIncharge.AcceptsTab = True
        Me.txtPersonIncharge.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtPersonIncharge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPersonIncharge.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPersonIncharge.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPersonIncharge.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPersonIncharge.Location = New System.Drawing.Point(482, 329)
        Me.txtPersonIncharge.MaxLength = 50
        Me.txtPersonIncharge.Name = "txtPersonIncharge"
        Me.txtPersonIncharge.Size = New System.Drawing.Size(195, 21)
        Me.txtPersonIncharge.TabIndex = 11
        Me.txtPersonIncharge.Tag = "NE"
        '
        'txtAddress3
        '
        Me.txtAddress3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAddress3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress3.Location = New System.Drawing.Point(482, 233)
        Me.txtAddress3.MaxLength = 50
        Me.txtAddress3.Name = "txtAddress3"
        Me.txtAddress3.Size = New System.Drawing.Size(195, 21)
        Me.txtAddress3.TabIndex = 7
        Me.txtAddress3.Tag = "NE"
        '
        'txtAddress4
        '
        Me.txtAddress4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAddress4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress4.Location = New System.Drawing.Point(482, 257)
        Me.txtAddress4.MaxLength = 50
        Me.txtAddress4.Name = "txtAddress4"
        Me.txtAddress4.Size = New System.Drawing.Size(195, 21)
        Me.txtAddress4.TabIndex = 8
        Me.txtAddress4.Tag = "NE"
        '
        'txtSortcode
        '
        Me.txtSortcode.AcceptsTab = True
        Me.txtSortcode.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtSortcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSortcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSortcode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSortcode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSortcode.Location = New System.Drawing.Point(482, 353)
        Me.txtSortcode.MaxLength = 50
        Me.txtSortcode.Name = "txtSortcode"
        Me.txtSortcode.Size = New System.Drawing.Size(195, 21)
        Me.txtSortcode.TabIndex = 12
        Me.txtSortcode.Tag = "NE"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(370, 356)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(115, 16)
        Me.Label2.TabIndex = 476199
        Me.Label2.Text = "Sort Code"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(370, 164)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(115, 16)
        Me.Label3.TabIndex = 476200
        Me.Label3.Text = "Location Type"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboLocation
        '
        Me.cboLocation.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLocation.Items.AddRange(New Object() {"HQ", "OUTLET"})
        Me.cboLocation.Location = New System.Drawing.Point(482, 158)
        Me.cboLocation.Name = "cboLocation"
        Me.cboLocation.Size = New System.Drawing.Size(195, 21)
        Me.cboLocation.TabIndex = 4
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.InitialImage = Nothing
        Me.pic.Location = New System.Drawing.Point(0, 0)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 476201
        Me.pic.TabStop = False
        '
        'grdLocation
        '
        Me.grdLocation.AllowUserToAddRows = False
        Me.grdLocation.AllowUserToDeleteRows = False
        Me.grdLocation.AllowUserToResizeColumns = False
        Me.grdLocation.AllowUserToResizeRows = False
        Me.grdLocation.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdLocation.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdLocation.ColumnHeadersHeight = 30
        Me.grdLocation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdLocation.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cLocationCode, Me.cLocationName, Me.cLocationType, Me.cAdd1, Me.cAddress2, Me.cAddress3, Me.cAddress4, Me.cTel, Me.cFax, Me.cAdminName, Me.cSortCode})
        Me.grdLocation.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdLocation.Location = New System.Drawing.Point(12, 106)
        Me.grdLocation.Name = "grdLocation"
        Me.grdLocation.RowHeadersVisible = False
        Me.grdLocation.Size = New System.Drawing.Size(352, 268)
        Me.grdLocation.TabIndex = 476202
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnClose)
        Me.GroupBox1.Controls.Add(Me.btnDelete)
        Me.GroupBox1.Controls.Add(Me.btnSave)
        Me.GroupBox1.Controls.Add(Me.btnNew)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Location = New System.Drawing.Point(12, 377)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(664, 72)
        Me.GroupBox1.TabIndex = 476203
        Me.GroupBox1.TabStop = False
        '
        'cLocationCode
        '
        Me.cLocationCode.HeaderText = "Location Code"
        Me.cLocationCode.Name = "cLocationCode"
        '
        'cLocationName
        '
        Me.cLocationName.HeaderText = "Location Name"
        Me.cLocationName.Name = "cLocationName"
        Me.cLocationName.Width = 250
        '
        'cLocationType
        '
        Me.cLocationType.HeaderText = "LocationType"
        Me.cLocationType.Name = "cLocationType"
        Me.cLocationType.Visible = False
        '
        'cAdd1
        '
        Me.cAdd1.HeaderText = "Address1"
        Me.cAdd1.Name = "cAdd1"
        Me.cAdd1.Visible = False
        '
        'cAddress2
        '
        Me.cAddress2.HeaderText = "Address2"
        Me.cAddress2.Name = "cAddress2"
        Me.cAddress2.Visible = False
        '
        'cAddress3
        '
        Me.cAddress3.HeaderText = "Address3"
        Me.cAddress3.Name = "cAddress3"
        Me.cAddress3.Visible = False
        '
        'cAddress4
        '
        Me.cAddress4.HeaderText = "Address4"
        Me.cAddress4.Name = "cAddress4"
        Me.cAddress4.Visible = False
        '
        'cTel
        '
        Me.cTel.HeaderText = "Telephone"
        Me.cTel.Name = "cTel"
        Me.cTel.Visible = False
        '
        'cFax
        '
        Me.cFax.HeaderText = "Fax"
        Me.cFax.Name = "cFax"
        Me.cFax.Visible = False
        '
        'cAdminName
        '
        Me.cAdminName.HeaderText = "AdminName"
        Me.cAdminName.Name = "cAdminName"
        Me.cAdminName.Visible = False
        '
        'cSortCode
        '
        Me.cSortCode.HeaderText = "SortCode"
        Me.cSortCode.Name = "cSortCode"
        Me.cSortCode.Visible = False
        '
        'frmLocation
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(689, 454)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grdLocation)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.cboLocation)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtSortcode)
        Me.Controls.Add(Me.txtAddress4)
        Me.Controls.Add(Me.txtAddress3)
        Me.Controls.Add(Me.txtPersonIncharge)
        Me.Controls.Add(Me.txtFax)
        Me.Controls.Add(Me.txtTelePhone)
        Me.Controls.Add(Me.txtAddress2)
        Me.Controls.Add(Me.txtAddress1)
        Me.Controls.Add(Me.txtLocationCode)
        Me.Controls.Add(Me.txtLocationName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblPersonInCharge)
        Me.Controls.Add(Me.lblFax)
        Me.Controls.Add(Me.lblTelePhone)
        Me.Controls.Add(Me.lblAddress1)
        Me.Controls.Add(Me.lblLocationCode)
        Me.Controls.Add(Me.lblLocationName)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmLocation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Location"
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdLocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub
    Private Sub frmSalesMan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'subGridSetting(grdLocation)
        'subLoadLocation()
        GetLocationDetails()
        'ShowLocationDetail(grdLocation.ActiveRow)
        grdLocation.Focus()
    End Sub
    Private Sub GetLocationDetails()
        Try
            Dim dsClass As New DataSet
            Dim iCnt As Long
            dsClass = objLocation.GetLocation
            If Not IsNothing(dsClass) Then
                If dsClass.Tables("Location").Rows.Count > 0 Then
                    grdLocation.RowCount = 0
                    For iCnt = 0 To dsClass.Tables("Location").Rows.Count - 1
                        grdLocation.Rows.Add()
                        fnSetdgvText(grdLocation, iCnt, 0, Trim(dsClass.Tables("Location").Rows(iCnt)("LocationCode") & ""))
                        fnSetdgvText(grdLocation, iCnt, 1, Trim(dsClass.Tables("Location").Rows(iCnt)("LocationName") & ""))

                        fnSetdgvText(grdLocation, iCnt, 2, Trim(dsClass.Tables("Location").Rows(iCnt)("LocationType") & ""))
                        fnSetdgvText(grdLocation, iCnt, 3, Trim(dsClass.Tables("Location").Rows(iCnt)("Address1") & ""))

                        fnSetdgvText(grdLocation, iCnt, 4, Trim(dsClass.Tables("Location").Rows(iCnt)("Address2") & ""))
                        fnSetdgvText(grdLocation, iCnt, 5, Trim(dsClass.Tables("Location").Rows(iCnt)("Address3") & ""))


                        fnSetdgvText(grdLocation, iCnt, 6, Trim(dsClass.Tables("Location").Rows(iCnt)("Address4") & ""))
                        fnSetdgvText(grdLocation, iCnt, 7, Trim(dsClass.Tables("Location").Rows(iCnt)("Phone") & ""))


                        fnSetdgvText(grdLocation, iCnt, 8, Trim(dsClass.Tables("Location").Rows(iCnt)("Fax") & ""))
                        fnSetdgvText(grdLocation, iCnt, 9, Trim(dsClass.Tables("Location").Rows(iCnt)("PersonIncharge") & ""))

                        fnSetdgvText(grdLocation, iCnt, 10, Trim(dsClass.Tables("Location").Rows(iCnt)("SortCode") & ""))

                    Next
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Function GetDetailsFromGrid()
        Dim sLocationCode, sLocationName, sLocationtype, sAddress1, sAddress2, sAddress3, sAddress4, sTelephone, sFax, sAdminName, sSortCode As String

        btnDelete.Enabled = True

        txtLocationCode.Text = fnGetdgvText(grdLocation, grdLocation.CurrentRow.Index, cLocationCode.Index)
        txtLocationName.Text = fnGetdgvText(grdLocation, grdLocation.CurrentRow.Index, cLocationName.Index)

        cboLocation.Text = fnGetdgvText(grdLocation, grdLocation.CurrentRow.Index, cLocationType.Index)
        txtAddress1.Text = fnGetdgvText(grdLocation, grdLocation.CurrentRow.Index, cadd1.Index)

        txtAddress2.Text = fnGetdgvText(grdLocation, grdLocation.CurrentRow.Index, cAddress2.Index)
        txtAddress3.Text = fnGetdgvText(grdLocation, grdLocation.CurrentRow.Index, cAddress3.Index)

        txtAddress4.Text = fnGetdgvText(grdLocation, grdLocation.CurrentRow.Index, cAddress4.Index)
        txtTelePhone.Text = fnGetdgvText(grdLocation, grdLocation.CurrentRow.Index, cTel.Index)

        txtFax.Text = fnGetdgvText(grdLocation, grdLocation.CurrentRow.Index, cFax.Index)
        txtPersonIncharge.Text = fnGetdgvText(grdLocation, grdLocation.CurrentRow.Index, cAdminName.Index)


        txtSortcode.Text = fnGetdgvText(grdLocation, grdLocation.CurrentRow.Index, cSortCode.Index)

    End Function
#Region "Validation"
    Private Function ValidateMe() As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            If txtLocationCode.Text.Trim = "" Then
                MessageBox.Show("LocationCode should not be blank.")
                txtLocationCode.Focus()
                Exit Function
            End If
            If txtLocationName.Text.Trim = "" Then
                MessageBox.Show("LocationName should not be blank.")
                txtLocationName.Focus()
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
        Try
            If ValidateMe() Then
                If objLocation.LocationSave(txtLocationCode.Text.Trim, txtLocationName.Text.Trim, cboLocation.Text.Trim, txtAddress1.Text.Trim, txtAddress2.Text.Trim, _
                   txtAddress3.Text.Trim, txtAddress4.Text.Trim, txtTelePhone.Text.Trim, txtFax.Text.Trim, txtPersonIncharge.Text.Trim, Val(txtSortcode.Text.Trim)) Then
                    MessageBox.Show("Location detail saved sucssfully.")
                    ClearMe()
                    GetLocationDetails()
                    grdLocation.Focus()
                    'ShowLocationDetail(grdLocation.ActiveRow)
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
            If grdLocation.RowCount > 0 And grdLocation.CurrentRow.Index > 0 Then
                Dim sLocationCode As String
                sLocationCode = fnGetdgvText(grdLocation, grdLocation.CurrentRow.Index, LocationCode)
                If MessageBox.Show("Do you want to delete " & fnGetdgvText(grdLocation, grdLocation.CurrentRow.Index, LocationCode) & " Location?", "GoldPOS", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    If objLocation.DeleteLocation(sLocationCode) Then
                        MessageBox.Show("Location Detail Deleted.")
                        ClearMe()
                        GetLocationDetails()
                        grdLocation.Focus()
                        'ShowLocationDetail(grdLocation.ActiveRow)
                    Else
                        MessageBox.Show("Error in Deleting Location Detail.")
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
    'Private Sub GetLocationDetails()
    '    Try
    '        Dim dsSalesman As New DataSet
    '        Dim iCnt As Long
    '        dsSalesman = objLocation.GetLocation
    '        If Not IsNothing(dsSalesman) Then
    '            If dsSalesman.Tables("Location").Rows.Count > 0 Then
    '                grdLocation.MaxRows = 0
    '                For iCnt = 0 To dsSalesman.Tables("Location").Rows.Count - 1
    '                    grdLocation.MaxRows = grdLocation.MaxRows + 1
    '                    SetText(grdLocation, iCnt + 1, LocationCode, Trim(dsSalesman.Tables("Location").Rows(iCnt)("LocationCode") & ""))
    '                    SetText(grdLocation, iCnt + 1, LocationName, Trim(dsSalesman.Tables("Location").Rows(iCnt)("LocationName") & ""))
    '                    SetText(grdLocation, iCnt + 1, LocationType, Trim(dsSalesman.Tables("Location").Rows(iCnt)("LocationType") & ""))
    '                    SetText(grdLocation, iCnt + 1, Add1, Trim(dsSalesman.Tables("Location").Rows(iCnt)("Address1") & ""))
    '                    SetText(grdLocation, iCnt + 1, Add2, Trim(dsSalesman.Tables("Location").Rows(iCnt)("Address2") & ""))
    '                    SetText(grdLocation, iCnt + 1, Add3, Trim(dsSalesman.Tables("Location").Rows(iCnt)("Address3") & ""))
    '                    SetText(grdLocation, iCnt + 1, Add4, Trim(dsSalesman.Tables("Location").Rows(iCnt)("Address4") & ""))
    '                    SetText(grdLocation, iCnt + 1, Phone, Trim(dsSalesman.Tables("Location").Rows(iCnt)("Phone") & ""))
    '                    SetText(grdLocation, iCnt + 1, Fax, Trim(dsSalesman.Tables("Location").Rows(iCnt)("Fax") & ""))
    '                    SetText(grdLocation, iCnt + 1, PersonIncharge, Trim(dsSalesman.Tables("Location").Rows(iCnt)("PersonInCharge") & ""))
    '                    SetText(grdLocation, iCnt + 1, Sortcode, Trim(dsSalesman.Tables("Location").Rows(iCnt)("SortCode") & ""))
    '                Next
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Sub
    'Private Sub ShowLocationDetail(ByVal Row As Integer)
    '    Try
    '        btnDelete.Enabled = True
    '        If Row > 0 Then
    '            txtLocationCode.Text = GetText(grdLocation, Row, LocationCode)
    '            txtLocationCode.Enabled = False
    '            txtLocationName.Text = GetText(grdLocation, Row, LocationName)
    '            cboLocation.Text = GetText(grdLocation, Row, LocationType)
    '            txtAddress1.Text = GetText(grdLocation, Row, Add1)
    '            txtAddress2.Text = GetText(grdLocation, Row, Add2)
    '            txtAddress3.Text = GetText(grdLocation, Row, Add3)
    '            txtAddress4.Text = GetText(grdLocation, Row, Add4)
    '            txtTelePhone.Text = GetText(grdLocation, Row, Phone)
    '            txtFax.Text = GetText(grdLocation, Row, Fax)
    '            txtPersonIncharge.Text = GetText(grdLocation, Row, PersonIncharge)
    '            txtSortcode.Text = GetText(grdLocation, Row, Sortcode)
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Sub
    Private Sub grdLocation_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_ClickEvent)
        'ShowLocationDetail(e.row)
    End Sub

    Private Sub grdLocation_LeaveRow(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_LeaveRowEvent)
        'ShowLocationDetail(e.newRow)
    End Sub
    Private Sub ClearMe()
        txtLocationCode.Clear()
        txtLocationName.Clear()
        txtAddress1.Clear()
        txtAddress2.Clear()
        txtAddress3.Clear()
        txtAddress4.Clear()
        txtTelePhone.Clear()
        txtFax.Clear()
        txtPersonIncharge.Clear()
        txtSortcode.Clear()
        btnDelete.Enabled = False
        txtLocationCode.Enabled = True
        txtLocationCode.Focus()
    End Sub
    Private Sub txtLocationCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLocationCode.LostFocus
        Try
            Dim i As Long
            Dim dsLocation As New DataSet
            Dim iCnt As Long
            dsLocation = objLocation.GetLocationwithCode(txtLocationCode.Text.Trim)
            If Not IsNothing(dsLocation) Then
                If dsLocation.Tables("Location").Rows.Count > 0 Then
                    txtLocationCode.Text = Trim(dsLocation.Tables("Location").Rows(0)("LocationCode") & "")
                    txtLocationName.Text = Trim(dsLocation.Tables("Location").Rows(0)("LocationName") & "")
                    txtAddress1.Text = Trim(dsLocation.Tables("Location").Rows(0)("Address1") & "")
                    txtAddress2.Text = Trim(dsLocation.Tables("Location").Rows(0)("Address2") & "")
                    txtAddress3.Text = Trim(dsLocation.Tables("Location").Rows(0)("Address3") & "")
                    txtAddress4.Text = Trim(dsLocation.Tables("Location").Rows(0)("Address4") & "")
                    txtTelePhone.Text = Trim(dsLocation.Tables("Location").Rows(0)("Phone") & "")
                    txtFax.Text = Trim(dsLocation.Tables("Location").Rows(0)("Fax") & "")
                    txtPersonIncharge.Text = Trim(dsLocation.Tables("Location").Rows(0)("PersonInCharge") & "")
                    txtSortcode.Text = Trim(dsLocation.Tables("Location").Rows(0)("SortCode") & "")
                End If
                'For i = 1 To grdLocation.MaxRows
                '    If Trim(GetText(grdLocation, i, 1)) = txtLocationCode.Text.Trim Then
                '        grdLocation.Row = i
                '        grdLocation.Col = 1
                '        grdLocation.Action = FPSpread.ActionConstants.ActionActiveCell
                '        Exit For
                '    End If
                'Next
                txtLocationCode.Enabled = False
                txtLocationName.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub grdLocation_Advance(sender As System.Object, e As AxFPSpread._DSpreadEvents_AdvanceEvent)

    End Sub

    Private Sub grdLocation_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdLocation.CellContentClick

    End Sub

    Private Sub grdLocation_Click(sender As Object, e As System.EventArgs) Handles grdLocation.Click
        GetDetailsFromGrid()
    End Sub
End Class
