Public Class frmCategoryML
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
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents ErrP As System.Windows.Forms.ErrorProvider
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCategoryCode As System.Windows.Forms.TextBox
    Friend WithEvents txtCategoryName As System.Windows.Forms.TextBox
    Friend WithEvents txtClassCode As System.Windows.Forms.TextBox
    Friend WithEvents btnClassSearch As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents grdCategory As AxFPSpread.AxvaSpread
    Friend WithEvents txtClassName As System.Windows.Forms.TextBox
    Friend WithEvents txtToInventoryCode As System.Windows.Forms.TextBox
    Friend WithEvents txtFromInventoryCode As System.Windows.Forms.TextBox
    Friend WithEvents txtNextInventoryCode As System.Windows.Forms.TextBox
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtInventoryPrefix As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmCategoryML))
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnNew = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.ErrP = New System.Windows.Forms.ErrorProvider
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtInventoryPrefix = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtToInventoryCode = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtFromInventoryCode = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.btnClassSearch = New System.Windows.Forms.Button
        Me.txtClassCode = New System.Windows.Forms.TextBox
        Me.txtClassName = New System.Windows.Forms.TextBox
        Me.txtRemarks = New System.Windows.Forms.TextBox
        Me.txtNextInventoryCode = New System.Windows.Forms.TextBox
        Me.txtCategoryCode = New System.Windows.Forms.TextBox
        Me.txtCategoryName = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.grdCategory = New AxFPSpread.AxvaSpread
        Me.pic = New System.Windows.Forms.PictureBox
        Me.Panel1.SuspendLayout()
        CType(Me.grdCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.Location = New System.Drawing.Point(696, 400)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.TabIndex = 12
        Me.btnDelete.Text = "Delete(F8)"
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(536, 400)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.TabIndex = 11
        Me.btnNew.Text = "New(F6)"
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(776, 400)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.TabIndex = 13
        Me.btnClose.Text = "Close(Esc)"
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.Location = New System.Drawing.Point(616, 400)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.TabIndex = 9
        Me.btnSave.Text = "Save(F7)"
        '
        'ErrP
        '
        Me.ErrP.ContainerControl = Me
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txtInventoryPrefix)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.txtToInventoryCode)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.txtFromInventoryCode)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.btnClassSearch)
        Me.Panel1.Controls.Add(Me.txtClassCode)
        Me.Panel1.Controls.Add(Me.txtClassName)
        Me.Panel1.Controls.Add(Me.txtRemarks)
        Me.Panel1.Controls.Add(Me.txtNextInventoryCode)
        Me.Panel1.Controls.Add(Me.txtCategoryCode)
        Me.Panel1.Controls.Add(Me.txtCategoryName)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(496, 80)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(355, 296)
        Me.Panel1.TabIndex = 30
        '
        'txtInventoryPrefix
        '
        Me.txtInventoryPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInventoryPrefix.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryPrefix.Location = New System.Drawing.Point(132, 232)
        Me.txtInventoryPrefix.MaxLength = 4
        Me.txtInventoryPrefix.Name = "txtInventoryPrefix"
        Me.txtInventoryPrefix.Size = New System.Drawing.Size(76, 20)
        Me.txtInventoryPrefix.TabIndex = 7
        Me.txtInventoryPrefix.Tag = "ENN;"
        Me.txtInventoryPrefix.Text = ""
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(16, 232)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(112, 23)
        Me.Label9.TabIndex = 41
        Me.Label9.Text = "Inventory Prefix"
        '
        'txtToInventoryCode
        '
        Me.txtToInventoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToInventoryCode.Enabled = False
        Me.txtToInventoryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToInventoryCode.Location = New System.Drawing.Point(132, 168)
        Me.txtToInventoryCode.MaxLength = 6
        Me.txtToInventoryCode.Name = "txtToInventoryCode"
        Me.txtToInventoryCode.Size = New System.Drawing.Size(102, 20)
        Me.txtToInventoryCode.TabIndex = 5
        Me.txtToInventoryCode.Tag = "ENN;"
        Me.txtToInventoryCode.Text = ""
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(16, 168)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(104, 23)
        Me.Label8.TabIndex = 40
        Me.Label8.Text = "To Inventory Code"
        '
        'txtFromInventoryCode
        '
        Me.txtFromInventoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFromInventoryCode.Enabled = False
        Me.txtFromInventoryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromInventoryCode.Location = New System.Drawing.Point(132, 136)
        Me.txtFromInventoryCode.MaxLength = 6
        Me.txtFromInventoryCode.Name = "txtFromInventoryCode"
        Me.txtFromInventoryCode.Size = New System.Drawing.Size(102, 20)
        Me.txtFromInventoryCode.TabIndex = 4
        Me.txtFromInventoryCode.Tag = "ENN;"
        Me.txtFromInventoryCode.Text = ""
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(16, 136)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(112, 23)
        Me.Label7.TabIndex = 38
        Me.Label7.Text = "From Inventory Code"
        '
        'btnClassSearch
        '
        Me.btnClassSearch.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnClassSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClassSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClassSearch.Location = New System.Drawing.Point(244, 72)
        Me.btnClassSearch.Name = "btnClassSearch"
        Me.btnClassSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnClassSearch.TabIndex = 4
        Me.btnClassSearch.TabStop = False
        Me.btnClassSearch.Text = "..."
        '
        'txtClassCode
        '
        Me.txtClassCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtClassCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClassCode.Location = New System.Drawing.Point(132, 72)
        Me.txtClassCode.MaxLength = 20
        Me.txtClassCode.Name = "txtClassCode"
        Me.txtClassCode.Size = New System.Drawing.Size(112, 20)
        Me.txtClassCode.TabIndex = 2
        Me.txtClassCode.Tag = "NE;"
        Me.txtClassCode.Text = ""
        '
        'txtClassName
        '
        Me.txtClassName.BackColor = System.Drawing.Color.White
        Me.txtClassName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtClassName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClassName.Location = New System.Drawing.Point(132, 103)
        Me.txtClassName.MaxLength = 50
        Me.txtClassName.Name = "txtClassName"
        Me.txtClassName.ReadOnly = True
        Me.txtClassName.Size = New System.Drawing.Size(208, 20)
        Me.txtClassName.TabIndex = 3
        Me.txtClassName.Tag = ""
        Me.txtClassName.Text = ""
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(132, 264)
        Me.txtRemarks.MaxLength = 100
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(208, 20)
        Me.txtRemarks.TabIndex = 8
        Me.txtRemarks.Text = ""
        '
        'txtNextInventoryCode
        '
        Me.txtNextInventoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNextInventoryCode.Enabled = False
        Me.txtNextInventoryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNextInventoryCode.Location = New System.Drawing.Point(132, 200)
        Me.txtNextInventoryCode.MaxLength = 6
        Me.txtNextInventoryCode.Name = "txtNextInventoryCode"
        Me.txtNextInventoryCode.Size = New System.Drawing.Size(102, 20)
        Me.txtNextInventoryCode.TabIndex = 6
        Me.txtNextInventoryCode.Tag = "ENN;"
        Me.txtNextInventoryCode.Text = ""
        '
        'txtCategoryCode
        '
        Me.txtCategoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCategoryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCategoryCode.Location = New System.Drawing.Point(132, 10)
        Me.txtCategoryCode.MaxLength = 20
        Me.txtCategoryCode.Name = "txtCategoryCode"
        Me.txtCategoryCode.Size = New System.Drawing.Size(112, 20)
        Me.txtCategoryCode.TabIndex = 0
        Me.txtCategoryCode.Tag = "NE;"
        Me.txtCategoryCode.Text = ""
        '
        'txtCategoryName
        '
        Me.txtCategoryName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCategoryName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCategoryName.Location = New System.Drawing.Point(132, 41)
        Me.txtCategoryName.MaxLength = 50
        Me.txtCategoryName.Name = "txtCategoryName"
        Me.txtCategoryName.Size = New System.Drawing.Size(208, 20)
        Me.txtCategoryName.TabIndex = 1
        Me.txtCategoryName.Tag = "NE;"
        Me.txtCategoryName.Text = ""
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(16, 264)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(96, 23)
        Me.Label6.TabIndex = 33
        Me.Label6.Text = "Remarks"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(16, 200)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(112, 23)
        Me.Label5.TabIndex = 32
        Me.Label5.Text = "Next Inventory Code"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(16, 103)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(96, 24)
        Me.Label4.TabIndex = 28
        Me.Label4.Text = "Class Name"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 72)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 23)
        Me.Label3.TabIndex = 27
        Me.Label3.Text = "Class Code"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 23)
        Me.Label2.TabIndex = 26
        Me.Label2.Text = "Category Name"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 23)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "Category Code"
        '
        'grdCategory
        '
        Me.grdCategory.Location = New System.Drawing.Point(11, 80)
        Me.grdCategory.Name = "grdCategory"
        Me.grdCategory.OcxState = CType(resources.GetObject("grdCategory.OcxState"), System.Windows.Forms.AxHost.State)
        Me.grdCategory.Size = New System.Drawing.Size(472, 296)
        Me.grdCategory.TabIndex = 29
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(0, 0)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(210, 75)
        Me.pic.TabIndex = 112
        Me.pic.TabStop = False
        '
        'frmCategoryML
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(858, 439)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.grdCategory)
        Me.Controls.Add(Me.btnDelete)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmCategoryML"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Category"
        Me.Panel1.ResumeLayout(False)
        CType(Me.grdCategory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Obrevations"
    ' string variables starts with "s"
    ' dobule and money variables starts with "d"
    ' boolean variables starts with "b"
    ' counter variables ends with "Cnt"
    ' dataset variables starts with "ds"
#End Region

    Const CategoryCode = 1
    Const CategoryName = 2
    Const ClassCode = 3
    Const FromInventoryCode = 4
    Const ToInventoryCode = 5
    Const NextInventoryCode = 6
    Const Remarks = 7
    Dim objClsCategory As New ClsCategory
    Dim objClsClass As New ClsClass
    Dim objCategory As New ClsCategory


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnClassSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClassSearch.Click
        Try
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblClass", "L", 4, "ClassCode", "", "ClassCode", "ClassCode", 80, "ClassName", "ClassName", 180, "Rate", "Rate", 60, "Remarks", "Remarks", 80)
            frmHelpClass.ShowDialog()
            txtClassCode.Text = frmHelpClass.Var1.Trim
            txtClassName.Text = frmHelpClass.Var2.Trim
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmCategory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        subGridSetting(grdCategory)
        GetCategoryDetails()
        ShowCategoryDetail(grdCategory.ActiveRow)
        grdCategory.Focus()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        ClearMe()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If ValidateMe() Then
                Dim sFInventoryCode, sTInventorycode, sNInventoryCode As String
                Dim dsCategory As New DataSet
                Dim dm As DataManager
                Dim dr As DataRow()
                Dim sLen As Integer
                Dim ssql As String
                Dim iCnt As Long
                Dim sFrom As String
                ssql = "select * from tblcategory where Substring(FromInventorycode,5,6)=" & fncFormatSQLText(txtFromInventoryCode.Text)
                ssql = ssql & " and substring(ToInventorycode,5,6) =" & fncFormatSQLText(txtToInventoryCode.Text)
                ssql = ssql & " and InventoryPrefix =" & fncFormatSQLText(txtInventoryPrefix.Text)
                dr = dm.RowReturnProjExecNonQryWithDataRow(ssql)
                If dr.Length > 0 Then
                    MsgBox("Inventory Range Already Exist")
                    Exit Sub
                End If

                'If Len(txtFromInventoryCode.Text) < 10 Then
                If Len(txtInventoryPrefix.Text) = 1 Then
                    sFInventoryCode = txtInventoryPrefix.Text + "000" + txtFromInventoryCode.Text.Trim
                    sTInventorycode = txtInventoryPrefix.Text + "000" + txtToInventoryCode.Text.Trim
                    sNInventoryCode = txtInventoryPrefix.Text + "000" + txtNextInventoryCode.Text.Trim
                ElseIf Len(txtInventoryPrefix.Text) = 2 Then
                    sFInventoryCode = txtInventoryPrefix.Text + "00" + txtFromInventoryCode.Text.Trim
                    sTInventorycode = txtInventoryPrefix.Text + "00" + txtToInventoryCode.Text.Trim
                    sNInventoryCode = txtInventoryPrefix.Text + "00" + txtNextInventoryCode.Text.Trim
                ElseIf Len(txtInventoryPrefix.Text) = 3 Then
                    sFInventoryCode = txtInventoryPrefix.Text + "0" + txtFromInventoryCode.Text.Trim
                    sTInventorycode = txtInventoryPrefix.Text + "0" + txtToInventoryCode.Text.Trim
                    sNInventoryCode = txtInventoryPrefix.Text + "0" + txtNextInventoryCode.Text.Trim
                ElseIf Len(txtInventoryPrefix.Text) = 4 Then
                    sFInventoryCode = txtInventoryPrefix.Text + txtFromInventoryCode.Text.Trim
                    sTInventorycode = txtInventoryPrefix.Text + txtToInventoryCode.Text.Trim
                    sNInventoryCode = txtInventoryPrefix.Text + txtNextInventoryCode.Text.Trim
                End If


                If Len(txtFromInventoryCode.Text) < 10 Then
                    If objClsCategory.CategorySaveML(txtCategoryCode.Text.Trim, txtCategoryName.Text.Trim, txtClassCode.Text.Trim, txtInventoryPrefix.Text.Trim, sFInventoryCode, sTInventorycode, sNInventoryCode, txtRemarks.Text.Trim) Then
                        MessageBox.Show("Category detail saved successfully.")
                        ClearMe()
                        GetCategoryDetails()
                        grdCategory.Focus()
                        ShowCategoryDetail(grdCategory.ActiveRow)
                    Else
                        MessageBox.Show("Error in Save")
                    End If
                Else
                    If objClsCategory.CategorySaveML(txtCategoryCode.Text.Trim, txtCategoryName.Text.Trim, txtClassCode.Text.Trim, txtInventoryPrefix.Text.Trim, txtFromInventoryCode.Text.Trim, txtToInventoryCode.Text.Trim, txtNextInventoryCode.Text.Trim, txtRemarks.Text.Trim) Then
                        MessageBox.Show("Category detail saved successfully.")
                        ClearMe()
                        GetCategoryDetails()
                        grdCategory.Focus()
                        ShowCategoryDetail(grdCategory.ActiveRow)
                    Else
                        MessageBox.Show("Error in Save")
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim dr As DataRow()
        Dim ssql As String
        Dim dm As DataManager
        Try
            If grdCategory.MaxRows > 0 And grdCategory.ActiveRow > 0 Then
                Dim sCategoryCode As String
                sCategoryCode = GetText(grdCategory, grdCategory.ActiveRow, CategoryCode)
                If MessageBox.Show("Do you want to delete " & GetText(grdCategory, grdCategory.ActiveRow, CategoryCode) & " Category?", "GoldPOS", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    ssql = "select * from tblInventory where Categorycode=" & fncFormatSQLText(sCategoryCode)
                    dr = dm.RowReturnProjExecNonQryWithDataRow(ssql)
                    If dr.Length > 0 Then
                        MsgBox("Deletion Not Allowed When Inventory Exists for this Category")
                        Exit Sub
                    End If
                    If objClsCategory.DeleteCategroy(sCategoryCode) Then
                        MessageBox.Show("Category detail deleted.")
                        ClearMe()
                        GetCategoryDetails()
                        grdCategory.Focus()
                        ShowCategoryDetail(grdCategory.ActiveRow)
                    Else
                        MessageBox.Show("Error in deleting Category detail.")
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
        End Select
    End Sub

    Private Sub txtCategoryCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtCategoryCode.Validating
        If txtCategoryCode.Text.Trim = "" Then
            ErrP.SetError(txtCategoryCode, "Should not be empty.")
            txtCategoryCode.Focus()
        Else
            ErrP.Dispose()
        End If
    End Sub

    Private Sub txtCategoryName_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtCategoryName.Validating
        If txtCategoryName.Text.Trim = "" Then
            ErrP.SetError(txtCategoryName, "Should not be empty.")
            txtCategoryName.Focus()
        Else
            ErrP.Dispose()
        End If
    End Sub

    Private Sub txtClassCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtClassCode.Validating
        If txtClassCode.Text.Trim = "" Then
            ErrP.SetError(txtClassCode, "Should not be empty.")
            txtClassCode.Focus()
        Else
            ErrP.Dispose()
        End If
    End Sub

    Private Sub txtFromInventoryCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtFromInventoryCode.Validating
        If txtFromInventoryCode.Text.Trim = "" Then
            ErrP.SetError(txtFromInventoryCode, "Should not be empty.")
            txtFromInventoryCode.Focus()
        End If
    End Sub

    Private Sub txtToInventoryCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtToInventoryCode.Validating
        If txtToInventoryCode.Text.Trim = "" Then
            ErrP.SetError(txtToInventoryCode, "Should not be empty.")
            txtToInventoryCode.Focus()

        End If
    End Sub

    Private Sub txtNextInventoryCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtNextInventoryCode.Validating
        If txtNextInventoryCode.Text.Trim = "" Then
            ErrP.SetError(txtNextInventoryCode, "Should not be empty.")
            txtNextInventoryCode.Focus()

        End If
    End Sub

    Private Sub txtClassCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtClassCode.LostFocus
        Dim sClassName As String
        Try
            If txtClassCode.Text.Trim <> "" Then
                sClassName = objClsClass.GetClassName(txtClassCode.Text.Trim)
                If Not IsNothing(sClassName.Trim) Or sClassName.Trim.Length > 0 Then
                    txtClassName.Text = sClassName
                Else
                    txtClassCode.Clear()
                    txtClassName.Clear()
                    txtClassCode.Focus()
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub GetCategoryDetails()
        Try
            Dim dsCategory As New DataSet
            Dim sLen As Integer
            Dim iCnt As Long
            dsCategory = objClsCategory.GetCategoryML()
            grdCategory.MaxRows = 0
            If Not IsNothing(dsCategory) Then
                If dsCategory.Tables("Category").Rows.Count > 0 Then
                    ''grdCategory.MaxRows = 0
                    For iCnt = 0 To dsCategory.Tables("Category").Rows.Count - 1
                        grdCategory.MaxRows = grdCategory.MaxRows + 1
                        SetText(grdCategory, iCnt + 1, CategoryCode, Trim(dsCategory.Tables("Category").Rows(iCnt)("CategoryCode") & ""))
                        SetText(grdCategory, iCnt + 1, CategoryName, Trim(dsCategory.Tables("Category").Rows(iCnt)("CategoryName") & ""))
                        SetText(grdCategory, iCnt + 1, ClassCode, Trim(dsCategory.Tables("Category").Rows(iCnt)("ClassCode") & ""))
                        sLen = Len(Trim(dsCategory.Tables("Category").Rows(iCnt)("InventoryPrefix") & ""))
                        SetText(grdCategory, iCnt + 1, FromInventoryCode, Trim(dsCategory.Tables("Category").Rows(iCnt)("FromInventoryCode") & ""))
                        SetText(grdCategory, iCnt + 1, ToInventoryCode, Trim(dsCategory.Tables("Category").Rows(iCnt)("ToInventoryCode") & ""))

                        SetText(grdCategory, iCnt + 1, NextInventoryCode, Trim(dsCategory.Tables("Category").Rows(iCnt)("NextInventoryCode") & ""))
                        SetText(grdCategory, iCnt + 1, Remarks, Trim(dsCategory.Tables("Category").Rows(iCnt)("Remarks") & ""))
                    Next
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ShowCategoryDetail(ByVal Row As Integer)
        Try
            btnDelete.Enabled = True
            If Row > 0 Then
                txtCategoryCode.Text = GetText(grdCategory, Row, CategoryCode)
                txtCategoryCode.ReadOnly = True
                txtCategoryName.Text = GetText(grdCategory, Row, CategoryName)
                txtClassCode.Text = GetText(grdCategory, Row, ClassCode)
                txtInventoryPrefix.Text = objCategory.GetInventoryPrefix(txtCategoryCode.Text.Trim).Trim
                txtInventoryPrefix.ReadOnly = True
                txtClassName.Text = objClsClass.GetClassName(txtClassCode.Text.Trim).Trim
                txtFromInventoryCode.Text = GetText(grdCategory, Row, FromInventoryCode)
                txtFromInventoryCode.MaxLength = 10
                txtFromInventoryCode.Enabled = False
                txtFromInventoryCode.ReadOnly = True
                txtToInventoryCode.Text = GetText(grdCategory, Row, ToInventoryCode)
                txtToInventoryCode.MaxLength = 10
                txtFromInventoryCode.Enabled = False
                txtToInventoryCode.ReadOnly = True
                txtNextInventoryCode.Text = GetText(grdCategory, Row, NextInventoryCode)
                txtNextInventoryCode.MaxLength = 10
                txtRemarks.Text = GetText(grdCategory, Row, Remarks)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtClassCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtClassCode.KeyDown
        If e.KeyCode = Keys.F1 Then btnClassSearch_Click(sender, e)
    End Sub

    Private Sub grdCategory_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_ClickEvent) Handles grdCategory.ClickEvent
        ShowCategoryDetail(e.row)
    End Sub

    Private Sub grdCategory_LeaveRow(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_LeaveRowEvent) Handles grdCategory.LeaveRow
        ShowCategoryDetail(e.newRow)
    End Sub
    Private Sub ClearMe()
        txtCategoryCode.Clear()
        txtCategoryName.Clear()
        txtClassCode.Clear()
        txtClassName.Clear()
        txtInventoryPrefix.Clear()
        txtFromInventoryCode.Clear()
        txtFromInventoryCode.MaxLength = 6
        txtFromInventoryCode.Text = "000001"
        txtToInventoryCode.Clear()
        txtToInventoryCode.MaxLength = 6
        txtToInventoryCode.Text = "999999"
        txtNextInventoryCode.Clear()
        txtNextInventoryCode.MaxLength = 6
        txtRemarks.Clear()
        btnDelete.Enabled = False
        txtCategoryCode.Enabled = True
        txtCategoryCode.ReadOnly = False
        txtFromInventoryCode.Enabled = True
        txtFromInventoryCode.ReadOnly = True
        txtToInventoryCode.Enabled = True
        txtToInventoryCode.ReadOnly = True
        txtInventoryPrefix.ReadOnly = False
        txtCategoryCode.Focus()


    End Sub

#Region "Validation"

    Private Function ValidateMe() As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            If txtCategoryCode.Text.Trim = "" Then
                MessageBox.Show("CategoryCode should not be blank.")
                txtCategoryCode.Focus()
                Exit Function
            End If
            If txtCategoryName.Text.Trim = "" Then
                MessageBox.Show("CategoryName should not be blank.")
                txtCategoryName.Focus()
                Exit Function
            End If
            If txtClassCode.Text.Trim = "" Then
                MessageBox.Show("ClassCode should not be blank.")
                txtClassCode.Focus()
                Exit Function
            End If
            'check valid classcode
            If IsNothing(objClsClass.GetClassName(txtClassCode.Text.Trim)) Then
                MessageBox.Show("Pleae enter valid ClassCode.")
                txtClassCode.Clear()
                txtClassCode.Focus()
                Exit Function
            End If
            If txtFromInventoryCode.Text.Trim = "" Then
                MessageBox.Show("From Inventory code should not be blank.")
                txtFromInventoryCode.Focus()
                Exit Function

            End If
            If txtToInventoryCode.Text.Trim = "" Then
                MessageBox.Show("To Inventory code should not be blank.")
                txtToInventoryCode.Focus()
                Exit Function
            End If
            If txtInventoryPrefix.Text.Trim = "" Then
                MessageBox.Show("Inventory Prefix  should not be blank.")
                txtInventoryPrefix.Focus()
                Exit Function
            End If


            '''If txtNextInventoryCode.Text.Trim = "" Then
            '''    MessageBox.Show("Next Inventory code should not be blank.")
            '''    txtNextInventoryCode.Focus()
            '''    Exit Function
            '''ElseIf Not IsNumeric(txtNextInventoryCode.Text.Trim) Then
            '''    MessageBox.Show("Next Inventory code should be Numeric.")
            '''    txtNextInventoryCode.Focus()
            '''    Exit Function
            '''ElseIf Not (Val(txtNextInventoryCode.Text) >= Val(txtFromInventoryCode.Text) And Val(txtNextInventoryCode.Text) <= Val(txtToInventoryCode.Text)) Then
            '''    MessageBox.Show("Next inventorycode should be greaterthan From InventoryCode and Less than To InventoryCode.")
            '''    txtNextInventoryCode.Focus()
            '''    Exit Function
            '''End If
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            bStatus = False
        Finally
            ValidateMe = bStatus
        End Try

    End Function

#End Region


    Private Sub txtCategoryCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCategoryCode.LostFocus
        Try
            Dim dsCategory As New DataSet
            Dim sLen As Integer
            Dim iCnt As Long
            Dim i As Long
            dsCategory = objClsCategory.GetCategorywithCode(txtCategoryCode.Text.Trim)
            If Not IsNothing(dsCategory) Then
                If dsCategory.Tables("Category").Rows.Count > 0 Then
                    txtCategoryCode.Text = Trim(dsCategory.Tables("Category").Rows(0)("CategoryCode") & "")
                    txtCategoryName.Text = Trim(dsCategory.Tables("Category").Rows(0)("CategoryName") & "")
                    txtCategoryCode.Text = Trim(dsCategory.Tables("Category").Rows(0)("CategoryCode") & "")
                    txtClassCode.Text = Trim(dsCategory.Tables("Category").Rows(0)("ClassCode") & "")
                    txtClassName.Text = objClsClass.GetClassName(txtClassCode.Text.Trim).Trim
                    txtFromInventoryCode.Text = Trim(dsCategory.Tables("Category").Rows(0)("FromInventoryCode") & "")
                    txtToInventoryCode.Text = Trim(dsCategory.Tables("Category").Rows(0)("ToInventoryCode") & "")
                    txtNextInventoryCode.Text = Trim(dsCategory.Tables("Category").Rows(0)("NextInventoryCode") & "")
                    txtInventoryPrefix.Text = Trim(dsCategory.Tables("Category").Rows(0)("InventoryPrefix") & "")
                    'End If
                    For i = 1 To grdCategory.MaxRows
                        If Trim(GetText(grdCategory, i, 1)) = txtCategoryCode.Text.Trim Then
                            grdCategory.Row = i
                            grdCategory.Col = 1
                            grdCategory.Action = FPSpread.ActionConstants.ActionActiveCell
                            Exit For
                        End If
                    Next
                    txtCategoryCode.Enabled = False
                    txtCategoryName.Focus()
                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subCategoryDeleteCheck()
        Dim Dr As DataRow()
        Dim sSql As String
        Dim dm As DataManager
        'For TotalSales
        sSql = "select InventoryCode from tblInventory where categorycode in(select categorycode from tblCategory)"
        Dr = dm.RowReturnProjExecNonQryWithDataRow(sSql)
        If Dr.Length > 0 Then
            If Dr(0)("InventoryCode" & "") = Trim(txtFromInventoryCode.Text.Trim) Or Dr(0)("InventoryCode" & "") = Trim(txtToInventoryCode.Text.Trim) Then

            End If
        End If
    End Sub

    Private Sub grdCategory_Advance(ByVal sender As System.Object, ByVal e As AxFPSpread._DSpreadEvents_AdvanceEvent) Handles grdCategory.Advance

    End Sub
End Class
