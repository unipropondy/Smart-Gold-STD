Public Class frmRefiner
    Inherits System.Windows.Forms.Form
    Dim Dm As New DataManager
    Dim objClsRefiner As New ClsRefiner
    Const RefinerCODE = 1
    Const RefinerNAME = 2
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents grdRefiner As System.Windows.Forms.DataGridView
    Friend WithEvents cRefCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cRefName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cTypeCol As System.Windows.Forms.DataGridViewTextBoxColumn
    Const Type = 3

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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cboType As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtRefinerName As System.Windows.Forms.TextBox
    Friend WithEvents txtRefinerCode As System.Windows.Forms.TextBox
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdUpdate As System.Windows.Forms.Button
    Friend WithEvents errP As System.Windows.Forms.ErrorProvider
    Friend WithEvents cmdDelete As System.Windows.Forms.Button
    Friend WithEvents cmdNew As System.Windows.Forms.Button
    Friend WithEvents LblRefinercode As System.Windows.Forms.Label
    Friend WithEvents lblRefinername As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRefiner))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cboType = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtRefinerName = New System.Windows.Forms.TextBox()
        Me.txtRefinerCode = New System.Windows.Forms.TextBox()
        Me.lblRefinername = New System.Windows.Forms.Label()
        Me.LblRefinercode = New System.Windows.Forms.Label()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.errP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdNew = New System.Windows.Forms.Button()
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.grdRefiner = New System.Windows.Forms.DataGridView()
        Me.cRefCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cRefName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cTypeCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1.SuspendLayout()
        CType(Me.errP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdRefiner, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.GroupBox1.Controls.Add(Me.cboType)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtRefinerName)
        Me.GroupBox1.Controls.Add(Me.txtRefinerCode)
        Me.GroupBox1.Controls.Add(Me.lblRefinername)
        Me.GroupBox1.Controls.Add(Me.LblRefinercode)
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(680, 88)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(430, 142)
        Me.GroupBox1.TabIndex = 864
        Me.GroupBox1.TabStop = False
        '
        'cboType
        '
        Me.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboType.Items.AddRange(New Object() {"GOLD", "SILVER", "PLATINUM", "COIN"})
        Me.cboType.Location = New System.Drawing.Point(101, 32)
        Me.cboType.Name = "cboType"
        Me.cboType.Size = New System.Drawing.Size(155, 23)
        Me.cboType.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(8, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 23)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Type"
        '
        'txtRefinerName
        '
        Me.txtRefinerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefinerName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRefinerName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefinerName.Location = New System.Drawing.Point(101, 104)
        Me.txtRefinerName.MaxLength = 50
        Me.txtRefinerName.Name = "txtRefinerName"
        Me.txtRefinerName.Size = New System.Drawing.Size(319, 22)
        Me.txtRefinerName.TabIndex = 3
        '
        'txtRefinerCode
        '
        Me.txtRefinerCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefinerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefinerCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRefinerCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefinerCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtRefinerCode.Location = New System.Drawing.Point(101, 64)
        Me.txtRefinerCode.MaxLength = 20
        Me.txtRefinerCode.Name = "txtRefinerCode"
        Me.txtRefinerCode.Size = New System.Drawing.Size(155, 22)
        Me.txtRefinerCode.TabIndex = 2
        '
        'lblRefinername
        '
        Me.lblRefinername.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRefinername.ForeColor = System.Drawing.Color.Black
        Me.lblRefinername.Location = New System.Drawing.Point(8, 104)
        Me.lblRefinername.Name = "lblRefinername"
        Me.lblRefinername.Size = New System.Drawing.Size(96, 23)
        Me.lblRefinername.TabIndex = 26
        Me.lblRefinername.Text = "Refiner Name"
        '
        'LblRefinercode
        '
        Me.LblRefinercode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRefinercode.ForeColor = System.Drawing.Color.Black
        Me.LblRefinercode.Location = New System.Drawing.Point(8, 68)
        Me.LblRefinercode.Name = "LblRefinercode"
        Me.LblRefinercode.Size = New System.Drawing.Size(96, 23)
        Me.LblRefinercode.TabIndex = 25
        Me.LblRefinercode.Text = "Refiner code"
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.Location = New System.Drawing.Point(1010, 325)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(100, 40)
        Me.cmdClose.TabIndex = 861
        Me.cmdClose.Text = "Close (Esc)"
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'cmdUpdate
        '
        Me.cmdUpdate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdUpdate.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUpdate.Location = New System.Drawing.Point(797, 325)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.Size = New System.Drawing.Size(100, 40)
        Me.cmdUpdate.TabIndex = 858
        Me.cmdUpdate.Text = "Update (F7)"
        Me.cmdUpdate.UseVisualStyleBackColor = False
        '
        'errP
        '
        Me.errP.ContainerControl = Me
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdDelete.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.Location = New System.Drawing.Point(904, 325)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(100, 40)
        Me.cmdDelete.TabIndex = 860
        Me.cmdDelete.Text = "Delete (F8)"
        Me.cmdDelete.UseVisualStyleBackColor = False
        '
        'cmdNew
        '
        Me.cmdNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdNew.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdNew.Location = New System.Drawing.Point(691, 325)
        Me.cmdNew.Name = "cmdNew"
        Me.cmdNew.Size = New System.Drawing.Size(100, 40)
        Me.cmdNew.TabIndex = 859
        Me.cmdNew.Text = "New (F6)"
        Me.cmdNew.UseVisualStyleBackColor = False
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(-5, -10)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 92)
        Me.pic.TabIndex = 865
        Me.pic.TabStop = False
        '
        'grdRefiner
        '
        Me.grdRefiner.AllowUserToAddRows = False
        Me.grdRefiner.AllowUserToDeleteRows = False
        Me.grdRefiner.AllowUserToResizeColumns = False
        Me.grdRefiner.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdRefiner.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdRefiner.ColumnHeadersHeight = 25
        Me.grdRefiner.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cRefCode, Me.cRefName, Me.cTypeCol})
        Me.grdRefiner.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdRefiner.Location = New System.Drawing.Point(5, 93)
        Me.grdRefiner.Name = "grdRefiner"
        Me.grdRefiner.RowHeadersVisible = False
        Me.grdRefiner.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdRefiner.Size = New System.Drawing.Size(661, 275)
        Me.grdRefiner.TabIndex = 866
        '
        'cRefCode
        '
        Me.cRefCode.HeaderText = "Refiner Code"
        Me.cRefCode.Name = "cRefCode"
        Me.cRefCode.Width = 200
        '
        'cRefName
        '
        Me.cRefName.HeaderText = "Refiner Name"
        Me.cRefName.Name = "cRefName"
        Me.cRefName.Width = 350
        '
        'cTypeCol
        '
        Me.cTypeCol.HeaderText = "Type"
        Me.cTypeCol.Name = "cTypeCol"
        '
        'frmRefiner
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1112, 380)
        Me.Controls.Add(Me.grdRefiner)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.cmdUpdate)
        Me.Controls.Add(Me.cmdDelete)
        Me.Controls.Add(Me.cmdNew)
        Me.Controls.Add(Me.GroupBox1)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmRefiner"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Refiner"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.errP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdRefiner, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmRefiner_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'subGridSetting(grdRefiner)
        cboType.SelectedIndex = 0
        Dm.SetMasterDataSet(" Select * from tblRefiner", "tblRefiner")
        DispRefinerDetails("")
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNew.Click
        clearall()
        txtRefinerCode.ReadOnly = False
        txtRefinerCode.Focus()
    End Sub
    Private Sub clearall()
        txtRefinerCode.Text = ""
        txtRefinerName.Text = ""
        cboType.Text = "GOLD"
    End Sub

    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        Dim dr As DataRow()
        If grdRefiner.RowCount > 0 Then
            If MsgBox("Do you want to Delete ?" & Trim(fnGetdgvText(grdRefiner, grdRefiner.CurrentRow.Index, 0)), MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If objClsRefiner.RefinerDelete(txtRefinerCode.Text.Trim) Then
                    clearall()
                    DispRefinerDetails("")
                Else
                    MsgBox("No such user!", MsgBoxStyle.Exclamation)
                End If
            End If
        End If
        Exit Sub
    End Sub
    Public Sub DispRefinerDetails(ByVal sCode As String)
        Dim ds As DataSet
        Dim iCnt As Integer
        Try
            ds = objClsRefiner.GetRefinerDetail(sCode)
            grdRefiner.RowCount = 0
            If ds.Tables(0).Rows.Count > 0 Then
                For iCnt = 0 To ds.Tables(0).Rows.Count - 1
                    grdRefiner.Rows.Add()
                    fnSetdgvText(grdRefiner, iCnt, 0, ds.Tables(0).Rows(iCnt)("RefinerCODE") & "")
                    fnSetdgvText(grdRefiner, iCnt, 1, ds.Tables(0).Rows(iCnt)("Refinername") & "")
                    fnSetdgvText(grdRefiner, iCnt, 2, ds.Tables(0).Rows(iCnt)("Type") & "")

                Next
            End If
            If grdRefiner.RowCount > 0 Then
                txtRefinerCode.Text = Trim(fnGetdgvText(grdRefiner, 0, 0))
                txtRefinerName.Text = Trim(fnGetdgvText(grdRefiner, 0, 1))
                cboType.Text = Trim(fnGetdgvText(grdRefiner, 0, 2))
                txtRefinerCode.ReadOnly = True
                txtRefinerCode.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            ds = Nothing
        End Try
    End Sub
    Public Function fnScreenValidate() As Boolean
        fnScreenValidate = False
        If Len(Trim(txtRefinerCode.Text)) = 0 Then
            MsgBox("Please enter Refiner Code !", MsgBoxStyle.Critical)
            txtRefinerCode.Focus()
            Exit Function
        End If

        If Len(Trim(txtRefinerName.Text)) = 0 Then
            MsgBox("Please enter Refiner Name !", MsgBoxStyle.Critical)
            txtRefinerName.Focus()
            Exit Function
        End If
        If Len(Trim(cboType.Text)) = 0 Then
            MsgBox("Please enter Type !", MsgBoxStyle.Critical)
            cboType.Focus()
            Exit Function
        End If

        fnScreenValidate = True
        Exit Function
    End Function
    Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click
        If fnScreenValidate() = True Then
            If objClsRefiner.fncSaveRefiner(txtRefinerCode.Text.Trim, txtRefinerName.Text.Trim, cboType.Text.Trim, sUserID) Then
                clearall()
                DispRefinerDetails("")
                MsgBox("Saved Successfully!", MsgBoxStyle.Information)
            Else
                MsgBox("Error in Save/Update !", MsgBoxStyle.Exclamation)
            End If
        End If
        Exit Sub
    End Sub

    Private Sub cboType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboType.SelectedIndexChanged
        If cboType.Text.Trim = "COIN" Then
            LblRefinercode.Text = "Coin Code"
            lblRefinername.Text = "Coin Name"
        Else
            LblRefinercode.Text = "Refiner Code"
            lblRefinername.Text = "Refiner Name"

        End If
    End Sub

    Private Sub frmRefiner_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                cmdClose_Click(sender, e)
            Case Keys.Enter
                SendKeys.Send("{Tab}")
            Case Keys.F6
                cmdNew_Click(sender, e)
            Case Keys.F7
                cmdUpdate_Click(sender, e)
            Case Keys.F8
                cmdDelete_Click(sender, e)
        End Select
    End Sub

    Private Sub grdRefiner_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdRefiner.CellClick
        txtRefinerCode.Text = Trim(fnGetdgvText(grdRefiner, grdRefiner.CurrentRow.Index, 0))
        txtRefinerName.Text = Trim(fnGetdgvText(grdRefiner, grdRefiner.CurrentRow.Index, 1))
        cboType.Text = Trim(fnGetdgvText(grdRefiner, grdRefiner.CurrentRow.Index, 2))
        txtRefinerCode.ReadOnly = True
    End Sub
End Class