Imports System
Imports System.IO
Public Class frmCatalouge
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
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents txtCategoryCode As System.Windows.Forms.TextBox
    Friend WithEvents txtCategoryName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents chkGoldLoss As System.Windows.Forms.CheckBox
    Friend WithEvents PicCatalouge As System.Windows.Forms.PictureBox
    Friend WithEvents txtCatalogueCode As System.Windows.Forms.TextBox
    Friend WithEvents txtCatalogueName As System.Windows.Forms.TextBox
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnCatagorySearch As System.Windows.Forms.Button
    Friend WithEvents txtCatalogueSearch As System.Windows.Forms.TextBox
    Friend WithEvents grdCatalogue As System.Windows.Forms.DataGridView
    Friend WithEvents grpImage As System.Windows.Forms.GroupBox
    Friend WithEvents btnBrowseImage As System.Windows.Forms.Button
    Friend WithEvents picImage As System.Windows.Forms.PictureBox
    Friend WithEvents cmdAddlDescOk As System.Windows.Forms.Button
    Friend WithEvents chkImage As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtReOrderLevel As System.Windows.Forms.TextBox
    Friend WithEvents cCatalogueCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCatalogueName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCategoryCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cGoldLoss As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cRemarks As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cImage As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents cReOrder As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCatalouge))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnCatagorySearch = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.chkGoldLoss = New System.Windows.Forms.CheckBox()
        Me.txtCatalogueCode = New System.Windows.Forms.TextBox()
        Me.txtCatalogueName = New System.Windows.Forms.TextBox()
        Me.txtCategoryCode = New System.Windows.Forms.TextBox()
        Me.txtCategoryName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PicCatalouge = New System.Windows.Forms.PictureBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.txtCatalogueSearch = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.grdCatalogue = New System.Windows.Forms.DataGridView()
        Me.cCatalogueCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cCatalogueName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cCategoryCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cGoldLoss = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cRemarks = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cImage = New System.Windows.Forms.DataGridViewImageColumn()
        Me.cReOrder = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grpImage = New System.Windows.Forms.GroupBox()
        Me.btnBrowseImage = New System.Windows.Forms.Button()
        Me.picImage = New System.Windows.Forms.PictureBox()
        Me.cmdAddlDescOk = New System.Windows.Forms.Button()
        Me.chkImage = New System.Windows.Forms.CheckBox()
        Me.txtReOrderLevel = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        CType(Me.PicCatalouge, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCatalogue, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpImage.SuspendLayout()
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.btnCatagorySearch)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.chkGoldLoss)
        Me.Panel1.Controls.Add(Me.txtCatalogueCode)
        Me.Panel1.Controls.Add(Me.txtCatalogueName)
        Me.Panel1.Controls.Add(Me.txtCategoryCode)
        Me.Panel1.Controls.Add(Me.txtCategoryName)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.txtRemarks)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Location = New System.Drawing.Point(636, 166)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(537, 218)
        Me.Panel1.TabIndex = 25
        '
        'btnCatagorySearch
        '
        Me.btnCatagorySearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCatagorySearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCatagorySearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCatagorySearch.Location = New System.Drawing.Point(509, 69)
        Me.btnCatagorySearch.Name = "btnCatagorySearch"
        Me.btnCatagorySearch.Size = New System.Drawing.Size(23, 21)
        Me.btnCatagorySearch.TabIndex = 5
        Me.btnCatagorySearch.Text = "..."
        Me.btnCatagorySearch.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(312, 141)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(125, 23)
        Me.Label5.TabIndex = 555
        Me.Label5.Text = "ReOrder Level"
        Me.Label5.Visible = False
        '
        'chkGoldLoss
        '
        Me.chkGoldLoss.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkGoldLoss.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkGoldLoss.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGoldLoss.Location = New System.Drawing.Point(6, 139)
        Me.chkGoldLoss.Name = "chkGoldLoss"
        Me.chkGoldLoss.Size = New System.Drawing.Size(140, 21)
        Me.chkGoldLoss.TabIndex = 7
        Me.chkGoldLoss.Text = "Active"
        '
        'txtCatalogueCode
        '
        Me.txtCatalogueCode.BackColor = System.Drawing.Color.White
        Me.txtCatalogueCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCatalogueCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCatalogueCode.Location = New System.Drawing.Point(133, 11)
        Me.txtCatalogueCode.MaxLength = 50
        Me.txtCatalogueCode.Name = "txtCatalogueCode"
        Me.txtCatalogueCode.Size = New System.Drawing.Size(399, 21)
        Me.txtCatalogueCode.TabIndex = 2
        Me.txtCatalogueCode.Tag = "NE;"
        '
        'txtCatalogueName
        '
        Me.txtCatalogueName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCatalogueName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCatalogueName.Location = New System.Drawing.Point(133, 40)
        Me.txtCatalogueName.MaxLength = 50
        Me.txtCatalogueName.Name = "txtCatalogueName"
        Me.txtCatalogueName.Size = New System.Drawing.Size(399, 21)
        Me.txtCatalogueName.TabIndex = 3
        Me.txtCatalogueName.Tag = ""
        '
        'txtCategoryCode
        '
        Me.txtCategoryCode.BackColor = System.Drawing.Color.White
        Me.txtCategoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCategoryCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCategoryCode.Location = New System.Drawing.Point(133, 69)
        Me.txtCategoryCode.MaxLength = 20
        Me.txtCategoryCode.Name = "txtCategoryCode"
        Me.txtCategoryCode.Size = New System.Drawing.Size(378, 21)
        Me.txtCategoryCode.TabIndex = 4
        Me.txtCategoryCode.Tag = "NE;"
        '
        'txtCategoryName
        '
        Me.txtCategoryName.BackColor = System.Drawing.Color.White
        Me.txtCategoryName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCategoryName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCategoryName.Location = New System.Drawing.Point(133, 98)
        Me.txtCategoryName.MaxLength = 50
        Me.txtCategoryName.Name = "txtCategoryName"
        Me.txtCategoryName.ReadOnly = True
        Me.txtCategoryName.Size = New System.Drawing.Size(399, 21)
        Me.txtCategoryName.TabIndex = 6
        Me.txtCategoryName.Tag = "NE;"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(7, 110)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(176, 24)
        Me.Label4.TabIndex = 41
        Me.Label4.Text = "Category Name"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(7, 77)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(176, 23)
        Me.Label1.TabIndex = 40
        Me.Label1.Text = "Category Code"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(7, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(176, 23)
        Me.Label2.TabIndex = 39
        Me.Label2.Text = "Catalouge Name"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(7, 11)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(176, 23)
        Me.Label7.TabIndex = 38
        Me.Label7.Text = "Catalouge Code"
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(133, 173)
        Me.txtRemarks.MaxLength = 100
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(399, 21)
        Me.txtRemarks.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(7, 170)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(176, 23)
        Me.Label6.TabIndex = 33
        Me.Label6.Text = "Remarks"
        '
        'PicCatalouge
        '
        Me.PicCatalouge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PicCatalouge.Location = New System.Drawing.Point(930, 12)
        Me.PicCatalouge.Name = "PicCatalouge"
        Me.PicCatalouge.Size = New System.Drawing.Size(144, 111)
        Me.PicCatalouge.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PicCatalouge.TabIndex = 43
        Me.PicCatalouge.TabStop = False
        Me.PicCatalouge.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.btnRefresh)
        Me.Panel2.Controls.Add(Me.txtCatalogueSearch)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Location = New System.Drawing.Point(4, 108)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(457, 52)
        Me.Panel2.TabIndex = 28
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRefresh.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.Location = New System.Drawing.Point(345, 5)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(100, 40)
        Me.btnRefresh.TabIndex = 1
        Me.btnRefresh.Text = "Refresh(F5)"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'txtCatalogueSearch
        '
        Me.txtCatalogueSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCatalogueSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCatalogueSearch.Location = New System.Drawing.Point(130, 9)
        Me.txtCatalogueSearch.MaxLength = 20
        Me.txtCatalogueSearch.Name = "txtCatalogueSearch"
        Me.txtCatalogueSearch.Size = New System.Drawing.Size(209, 20)
        Me.txtCatalogueSearch.TabIndex = 0
        Me.txtCatalogueSearch.Tag = "NE;"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(148, 23)
        Me.Label3.TabIndex = 29
        Me.Label3.Text = "Catalogue Name"
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnNew.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Location = New System.Drawing.Point(710, 390)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(100, 40)
        Me.btnNew.TabIndex = 10
        Me.btnNew.Text = "New(F6)"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(1067, 390)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 12
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(829, 390)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 40)
        Me.btnSave.TabIndex = 9
        Me.btnSave.Text = "Save(F7)"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDelete.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(948, 390)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(100, 40)
        Me.btnDelete.TabIndex = 11
        Me.btnDelete.Text = "Delete(F8)"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(4, 2)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 84
        Me.pic.TabStop = False
        '
        'grdCatalogue
        '
        Me.grdCatalogue.AllowUserToAddRows = False
        Me.grdCatalogue.AllowUserToDeleteRows = False
        Me.grdCatalogue.AllowUserToResizeColumns = False
        Me.grdCatalogue.AllowUserToResizeRows = False
        Me.grdCatalogue.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdCatalogue.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdCatalogue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdCatalogue.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cCatalogueCode, Me.cCatalogueName, Me.cCategoryCode, Me.cGoldLoss, Me.cRemarks, Me.cImage, Me.cReOrder})
        Me.grdCatalogue.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdCatalogue.Location = New System.Drawing.Point(4, 166)
        Me.grdCatalogue.Name = "grdCatalogue"
        Me.grdCatalogue.RowHeadersVisible = False
        Me.grdCatalogue.Size = New System.Drawing.Size(626, 218)
        Me.grdCatalogue.TabIndex = 85
        '
        'cCatalogueCode
        '
        Me.cCatalogueCode.HeaderText = "Catalogue Code"
        Me.cCatalogueCode.Name = "cCatalogueCode"
        Me.cCatalogueCode.Width = 200
        '
        'cCatalogueName
        '
        Me.cCatalogueName.HeaderText = "Catalogue Name"
        Me.cCatalogueName.Name = "cCatalogueName"
        Me.cCatalogueName.Width = 200
        '
        'cCategoryCode
        '
        Me.cCategoryCode.HeaderText = "Category Code"
        Me.cCategoryCode.Name = "cCategoryCode"
        Me.cCategoryCode.Width = 200
        '
        'cGoldLoss
        '
        Me.cGoldLoss.HeaderText = "Gold Loss"
        Me.cGoldLoss.Name = "cGoldLoss"
        Me.cGoldLoss.Visible = False
        '
        'cRemarks
        '
        Me.cRemarks.HeaderText = "Remarks"
        Me.cRemarks.Name = "cRemarks"
        Me.cRemarks.Visible = False
        '
        'cImage
        '
        Me.cImage.HeaderText = "Image"
        Me.cImage.Name = "cImage"
        Me.cImage.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cImage.Visible = False
        '
        'cReOrder
        '
        Me.cReOrder.HeaderText = "ReOrder Level"
        Me.cReOrder.Name = "cReOrder"
        Me.cReOrder.Width = 140
        '
        'grpImage
        '
        Me.grpImage.Controls.Add(Me.btnBrowseImage)
        Me.grpImage.Controls.Add(Me.picImage)
        Me.grpImage.Controls.Add(Me.cmdAddlDescOk)
        Me.grpImage.Location = New System.Drawing.Point(76, 103)
        Me.grpImage.Name = "grpImage"
        Me.grpImage.Size = New System.Drawing.Size(406, 350)
        Me.grpImage.TabIndex = 553
        Me.grpImage.TabStop = False
        Me.grpImage.Visible = False
        '
        'btnBrowseImage
        '
        Me.btnBrowseImage.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnBrowseImage.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowseImage.Location = New System.Drawing.Point(30, 309)
        Me.btnBrowseImage.Name = "btnBrowseImage"
        Me.btnBrowseImage.Size = New System.Drawing.Size(58, 25)
        Me.btnBrowseImage.TabIndex = 771
        Me.btnBrowseImage.Text = "Browse"
        Me.btnBrowseImage.UseVisualStyleBackColor = False
        '
        'picImage
        '
        Me.picImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picImage.Location = New System.Drawing.Point(32, 15)
        Me.picImage.Name = "picImage"
        Me.picImage.Size = New System.Drawing.Size(342, 286)
        Me.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picImage.TabIndex = 770
        Me.picImage.TabStop = False
        '
        'cmdAddlDescOk
        '
        Me.cmdAddlDescOk.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdAddlDescOk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAddlDescOk.ForeColor = System.Drawing.Color.Black
        Me.cmdAddlDescOk.Location = New System.Drawing.Point(313, 309)
        Me.cmdAddlDescOk.Name = "cmdAddlDescOk"
        Me.cmdAddlDescOk.Size = New System.Drawing.Size(61, 23)
        Me.cmdAddlDescOk.TabIndex = 45
        Me.cmdAddlDescOk.Text = "OK"
        Me.cmdAddlDescOk.UseVisualStyleBackColor = False
        '
        'chkImage
        '
        Me.chkImage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkImage.ForeColor = System.Drawing.Color.Black
        Me.chkImage.Location = New System.Drawing.Point(4, 390)
        Me.chkImage.Name = "chkImage"
        Me.chkImage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkImage.Size = New System.Drawing.Size(66, 18)
        Me.chkImage.TabIndex = 554
        Me.chkImage.Text = "Image"
        '
        'txtReOrderLevel
        '
        Me.txtReOrderLevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReOrderLevel.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReOrderLevel.Location = New System.Drawing.Point(1074, 306)
        Me.txtReOrderLevel.MaxLength = 100
        Me.txtReOrderLevel.Name = "txtReOrderLevel"
        Me.txtReOrderLevel.Size = New System.Drawing.Size(95, 21)
        Me.txtReOrderLevel.TabIndex = 556
        Me.txtReOrderLevel.Visible = False
        '
        'frmCatalouge
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(7, 14)
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(1185, 465)
        Me.Controls.Add(Me.txtReOrderLevel)
        Me.Controls.Add(Me.chkImage)
        Me.Controls.Add(Me.grpImage)
        Me.Controls.Add(Me.grdCatalogue)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.PicCatalouge)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCatalouge"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Catalogue"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PicCatalouge, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCatalogue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpImage.ResumeLayout(False)
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region
    Dim objClsCatalogue As New ClsCatalogue
    Dim objClsCategory As New ClsCategory
    Const CatalogueCode = 1
    Const CatalogueName = 2
    Const CategoryCode = 3
    Const GoldLoss = 4
    Const Remarks = 5
    Const CatImage = 6
    Const ReOrderLevel = 7

    Private Sub frmCatalouge_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'subGridSetting(grdCatalogue)
        subGetCatalogueDetails()
        'subShowCatalogueDetail(grdCatalogue.ActiveRow)
        grdCatalogue.Focus()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtCategoryCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCategoryCode.KeyDown
        If e.KeyCode = Keys.F1 Then btnCatagorySearch_Click(sender, e)
    End Sub

    Private Sub btnCatagorySearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCatagorySearch.Click
        Dim frmHelpCategory As New frmHelpSearch(0, 0, "tblCategory", "L", 4, "CategoryCode", "", "CategoryCode", "CategoryCode", 80, "CategoryName", "CategoryName", 180, "ClassCode", "ClassCode", 60, "Remarks", "Remarks", 80)
        frmHelpCategory.ShowDialog()
        txtCategoryCode.Text = frmHelpCategory.Var1.Trim
        txtCategoryName.Text = frmHelpCategory.Var2.Trim
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        subGetCatalogueDetails()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        subClearMe()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        subSave()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        subDelete()
    End Sub

    Private Sub grdCatalogue_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_ClickEvent)
        subShowCatalogueDetail(e.row)
    End Sub

    Private Sub grdCatalogue_LeaveRow(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_LeaveRowEvent)
        subShowCatalogueDetail(e.newRow)
    End Sub
    Public Sub Byte2ImageNEW(ByRef NewImage As Image, ByVal ByteArr() As Byte)
        Dim ImageStream As System.IO.MemoryStream
        Try
            If ByteArr.GetUpperBound(0) > 0 Then
                ImageStream = New MemoryStream(ByteArr)
                NewImage = System.Drawing.Image.FromStream(ImageStream)
            Else
                NewImage = Nothing
            End If
        Catch ex As Exception
            NewImage = Nothing
        End Try
    End Sub
    Public Sub Byte2Image(ByVal grd As Object, ByVal ByteArr() As Byte, ByVal iRow As Integer)
        Dim ImageStream As System.IO.MemoryStream
        Dim NewImage As Image
        Try
            If ByteArr.GetUpperBound(0) > 0 Then
                ImageStream = New MemoryStream(ByteArr)
                NewImage = Image.FromStream(ImageStream)
                grd.Col = 5
                grd.Row = iRow
                grd.TypePictPicture = NewImage
            Else
                NewImage = Nothing
            End If
        Catch ex As Exception
            NewImage = Nothing
        End Try
    End Sub

    Private Sub subGetCatalogueDetails()
        Try
            Dim dsCatalogue As New DataSet
            Dim iCnt As Long
            dsCatalogue = objClsCatalogue.GetCatalogue(txtCatalogueSearch.Text.Trim)
            If Not IsNothing(dsCatalogue) Then
                If dsCatalogue.Tables("Catalogue").Rows.Count > 0 Then
                    grdCatalogue.RowCount = 0
                    For iCnt = 0 To dsCatalogue.Tables("Catalogue").Rows.Count - 1
                        grdCatalogue.Rows.Add()
                        fnSetdgvText(grdCatalogue, iCnt, 0, Trim(dsCatalogue.Tables("Catalogue").Rows(iCnt)("CatalogueCode") & ""))
                        fnSetdgvText(grdCatalogue, iCnt, 1, Trim(dsCatalogue.Tables("Catalogue").Rows(iCnt)("CatalogueName") & ""))

                        fnSetdgvText(grdCatalogue, iCnt, 2, Trim(dsCatalogue.Tables("Catalogue").Rows(iCnt)("CategoryCode") & ""))
                        fnSetdgvText(grdCatalogue, iCnt, 3, Trim(dsCatalogue.Tables("Catalogue").Rows(iCnt)("GoldLoss") & ""))

                        fnSetdgvText(grdCatalogue, iCnt, 4, Trim(dsCatalogue.Tables("Catalogue").Rows(iCnt)("Remarks") & ""))
                        '    Byte2Image(grdCatalogue, dsCatalogue.Tables("Catalogue").Rows(iCnt)("CatalogueImage"), iCnt)
                        'Byte2ImageNEW(picImage.Image, dsCatalogue.Tables("Catalogue").Rows(iCnt)("CatalogueImage"))
                        fnSetdgvText(grdCatalogue, iCnt, 6, Trim(dsCatalogue.Tables("Catalogue").Rows(iCnt)("ReOrderLevel") & ""))
                    Next
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subShowCatalogueDetail(ByVal iRow As Integer)
        Dim sFileName As String
        Dim FSO As New Scripting.FileSystemObject
        Try
            btnDelete.Enabled = True
            If iRow > 0 Then
                txtCatalogueCode.Text = GetText(grdCatalogue, iRow, CatalogueCode)
                txtCatalogueCode.ReadOnly = True
                txtCatalogueName.Text = GetText(grdCatalogue, iRow, CatalogueName)
                txtCategoryCode.Text = GetText(grdCatalogue, iRow, CategoryCode)
                txtCategoryName.Text = objClsCategory.GetCategoryName(txtCategoryCode.Text.Trim).Trim
                If GetText(grdCatalogue, iRow, GoldLoss) = "Y" Then
                    chkGoldLoss.Checked = True
                Else
                    chkGoldLoss.Checked = False
                End If
                txtRemarks.Text = GetText(grdCatalogue, iRow, Remarks)
                txtReOrderLevel.Text = GetText(grdCatalogue, iRow, ReOrderLevel)
                sFileName = Application.StartupPath & "\Images\" & Trim(txtCatalogueCode.Text) & ".gif"
                PicCatalouge.Visible = False
                If FSO.FileExists(sFileName) Then
                    PicCatalouge.Image = Image.FromFile(sFileName)
                Else
                    sFileName = Application.StartupPath & "\Images\No-Image.gif"
                    If FSO.FileExists(sFileName) Then
                        PicCatalouge.Image = Image.FromFile(sFileName)
                    Else
                        PicCatalouge.Visible = False
                    End If
                End If
                FSO = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subClearMe()
        Try
            txtCatalogueCode.ReadOnly = False
            txtCatalogueCode.Clear()
            txtCatalogueName.Clear()
            txtCategoryCode.Clear()
            txtCategoryName.Clear()
            chkGoldLoss.Checked = False
            txtRemarks.Clear()
            txtReOrderLevel.Clear()
            btnDelete.Enabled = False
            txtCatalogueCode.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subSave()
        Dim NewImage As Image
        Dim ByteArr() As Byte
        Try
            If subValidateMe() Then
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

                gsConBatchPosting.ConnectionString = gsCon
                gsConBatchPosting.Open()
                gsTrans = gsConBatchPosting.BeginTransaction


                If objClsCatalogue.CatalogueSave(txtCatalogueCode.Text.Trim, txtCatalogueName.Text.Trim, txtCategoryCode.Text.Trim, IIf(chkGoldLoss.Checked = True, "Y", "N"), txtRemarks.Text.Trim, ByteArr, picImage.Name, Val(txtReOrderLevel.Text.Trim)) Then
                    MessageBox.Show("Catalogue detail saved sucssfully.")
                    gsTrans.Commit()
                    gsConBatchPosting.Close()
                    subClearMe()
                    subGetCatalogueDetails()
                    grdCatalogue.Focus()
                Else
                    MessageBox.Show("Error in Save")
                    gsTrans.Rollback()
                    gsConBatchPosting.Close()
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subDelete()
        Try
            If grdCatalogue.RowCount > 0 And grdCatalogue.CurrentRow.Index > 0 Then
                Dim sCatalogueCode As String
                sCatalogueCode = fnGetdgvText(grdCatalogue, grdCatalogue.CurrentRow.Index, CatalogueCode)
                If fncGetCatalogueTran(sCatalogueCode) = True Then
                    MsgBox("Can Not Delete Catalogue , Beacuse This Catalogue Is Having Some Inventory", MsgBoxStyle.Information)
                    Exit Sub
                Else
                    If MessageBox.Show("Do you want to delete " & fnGetdgvText(grdCatalogue, grdCatalogue.CurrentRow.Index, CatalogueCode) & " Catalogue?", "GoldPOS", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        If objClsCatalogue.CatalogueDelete(sCatalogueCode) Then
                            MessageBox.Show("Catalogue detail deleted.")
                            subClearMe()
                            subGetCatalogueDetails()
                            grdCatalogue.Focus()
                            'subShowCatalogueDetail(grdCatalogue.ActiveRow)
                        Else
                            MessageBox.Show("Error in deleting Catalogue detail.")
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function fncGetCatalogueTran(ByVal sCode As String) As Boolean
        Dim DvCustomer() As DataRow
        Dim DM As New DataManager
        fncGetCatalogueTran = False
        DvCustomer = DM.RowReturnProjExecNonQryWithDataRow("Select * from tblInventory where CatalogueCode=" & fncFormatSQLText(sCode))
        If DvCustomer.Length > 0 Then
            fncGetCatalogueTran = True
        Else
            fncGetCatalogueTran = False
        End If
    End Function

    Private Function subValidateMe() As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            If txtCatalogueCode.Text.Trim = "" Then
                MessageBox.Show("CatalogueCode should not be blank.")
                txtCatalogueCode.Focus()
                Exit Function
            End If
            If txtCatalogueName.Text.Trim = "" Then
                MessageBox.Show("CatalogueName should not be blank.")
                txtCatalogueName.Focus()
                Exit Function
            End If
            If txtCategoryCode.Text.Trim = "" Then
                MessageBox.Show("CategoryCode should not be blank.")
                txtCategoryCode.Focus()
                Exit Function
            End If
            'check valid category code
            If IsNothing(objClsCategory.GetCategoryName(txtCategoryCode.Text.Trim)) Then
                MessageBox.Show("Pleae enter valid CatagoryCode.")
                txtCategoryCode.Clear()
                txtCategoryCode.Focus()
                Exit Function
            End If
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            bStatus = False
        Finally
            subValidateMe = bStatus
        End Try
    End Function

    Private Sub frmCatalouge_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
            Case Keys.F5
                btnRefresh_Click(sender, e)
                'Case Keys.Up
                'SendKeys.Send("+{Tab}")
        End Select
    End Sub

    Private Sub chkGoldLoss_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkGoldLoss.CheckedChanged

    End Sub

    Private Sub txtCategoryCode_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtCategoryCode.TextChanged

    End Sub

    Private Sub grdCatalogue_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCatalogue.CellContentClick

    End Sub

    Private Sub grdCatalogue_Click(sender As Object, e As System.EventArgs) Handles grdCatalogue.Click
        GetDetailsFromGrid()
    End Sub
    Public Function GetDetailsFromGrid()
        Dim sGoldLoss As String
        Dim dsCatalogue As New DataSet
        Dim iCnt As Long
        btnDelete.Enabled = True
        txtCatalogueCode.Text = fnGetdgvText(grdCatalogue, grdCatalogue.CurrentRow.Index, cCatalogueCode.Index)
        txtCatalogueName.Text = fnGetdgvText(grdCatalogue, grdCatalogue.CurrentRow.Index, cCatalogueName.Index)

        txtCategoryCode.Text = fnGetdgvText(grdCatalogue, grdCatalogue.CurrentRow.Index, cCategoryCode.Index)
        sGoldLoss = fnGetdgvText(grdCatalogue, grdCatalogue.CurrentRow.Index, cGoldLoss.Index)

        If sGoldLoss = "Y" Then
            chkGoldLoss.Checked = True
        Else
            chkGoldLoss.Checked = False
        End If
        txtCategoryName.Text = objClsCategory.GetCategoryName(txtCategoryCode.Text.Trim).Trim
        txtRemarks.Text = fnGetdgvText(grdCatalogue, grdCatalogue.CurrentRow.Index, cRemarks.Index)
        txtReOrderLevel.Text = fnGetdgvText(grdCatalogue, grdCatalogue.CurrentRow.Index, cReOrder.Index)

        dsCatalogue = objClsCatalogue.GetCatalogue(txtCatalogueName.Text.Trim)
        If Not IsNothing(dsCatalogue) Then
            If dsCatalogue.Tables("Catalogue").Rows.Count > 0 Then
                Byte2ImageNEW(picImage.Image, dsCatalogue.Tables("Catalogue").Rows(0)("CatalogueImage"))
            End If
        End If
    End Function

    Private Sub chkImage_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkImage.CheckedChanged
        If chkImage.Checked = True Then
            grpImage.Visible = True
        Else
            grpImage.Visible = False
        End If
    End Sub

    Private Sub cmdAddlDescOk_Click(sender As System.Object, e As System.EventArgs) Handles cmdAddlDescOk.Click
        grpImage.Visible = False
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
    Private Sub ScaleImage(ByVal p As PictureBox, ByVal i As Bitmap)
        If i.Height > p.Height Then
            Dim diff As Integer = i.Height - p.Height
            Dim Resized As Bitmap = New Bitmap(i, New Size(i.Width - diff, i.Height - diff))
            i = Resized
        End If
        If i.Width > p.Width Then
            Dim diff As Integer = i.Width - p.Width
            Dim Resized As Bitmap = New Bitmap(i, New Size(i.Width - diff, i.Height - diff))
            i = Resized
        End If
    End Sub
End Class

