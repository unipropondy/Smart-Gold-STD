Public Class frmCountry
    Inherits System.Windows.Forms.Form
    Dim Dm As New DataManager
    Dim objClsCountry As New ClsCountry
    Const CountryCODE = 1
    Const CountryNAME = 2
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents grdCountry As System.Windows.Forms.DataGridView
    Friend WithEvents cCountryCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCountryName As System.Windows.Forms.DataGridViewTextBoxColumn
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
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdDelete As System.Windows.Forms.Button
    Friend WithEvents cmdUpdate As System.Windows.Forms.Button
    Friend WithEvents errP As System.Windows.Forms.ErrorProvider
    Friend WithEvents cmdNew As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboType As System.Windows.Forms.ComboBox
    Friend WithEvents txtCountryName As System.Windows.Forms.TextBox
    Friend WithEvents txtCountryCode As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCountry))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cboType = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCountryName = New System.Windows.Forms.TextBox()
        Me.txtCountryCode = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.errP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.cmdNew = New System.Windows.Forms.Button()
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.grdCountry = New System.Windows.Forms.DataGridView()
        Me.cCountryCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cCountryName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cTypeCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1.SuspendLayout()
        CType(Me.errP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCountry, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.GroupBox1.Controls.Add(Me.cboType)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtCountryName)
        Me.GroupBox1.Controls.Add(Me.txtCountryCode)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(464, 80)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(271, 156)
        Me.GroupBox1.TabIndex = 857
        Me.GroupBox1.TabStop = False
        '
        'cboType
        '
        Me.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboType.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboType.Items.AddRange(New Object() {"GOLD", "SILVER", "PLATINUM", "COIN"})
        Me.cboType.Location = New System.Drawing.Point(101, 32)
        Me.cboType.Name = "cboType"
        Me.cboType.Size = New System.Drawing.Size(155, 21)
        Me.cboType.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(8, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 23)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Type"
        '
        'txtCountryName
        '
        Me.txtCountryName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCountryName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCountryName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCountryName.Location = New System.Drawing.Point(101, 104)
        Me.txtCountryName.MaxLength = 50
        Me.txtCountryName.Name = "txtCountryName"
        Me.txtCountryName.Size = New System.Drawing.Size(155, 21)
        Me.txtCountryName.TabIndex = 3
        '
        'txtCountryCode
        '
        Me.txtCountryCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtCountryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCountryCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCountryCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCountryCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtCountryCode.Location = New System.Drawing.Point(101, 64)
        Me.txtCountryCode.MaxLength = 20
        Me.txtCountryCode.Name = "txtCountryCode"
        Me.txtCountryCode.Size = New System.Drawing.Size(155, 21)
        Me.txtCountryCode.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(8, 104)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 23)
        Me.Label3.TabIndex = 26
        Me.Label3.Text = "Country Name"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(8, 68)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 23)
        Me.Label5.TabIndex = 25
        Me.Label5.Text = "Country code"
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.Location = New System.Drawing.Point(633, 272)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(100, 40)
        Me.cmdClose.TabIndex = 7
        Me.cmdClose.Text = "Close (Esc)"
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdDelete.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.Location = New System.Drawing.Point(525, 273)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(100, 40)
        Me.cmdDelete.TabIndex = 6
        Me.cmdDelete.Text = "Delete (F8)"
        Me.cmdDelete.UseVisualStyleBackColor = False
        '
        'cmdUpdate
        '
        Me.cmdUpdate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdUpdate.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUpdate.Location = New System.Drawing.Point(417, 273)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.Size = New System.Drawing.Size(100, 40)
        Me.cmdUpdate.TabIndex = 4
        Me.cmdUpdate.Text = "Update (F7)"
        Me.cmdUpdate.UseVisualStyleBackColor = False
        '
        'errP
        '
        Me.errP.ContainerControl = Me
        '
        'cmdNew
        '
        Me.cmdNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdNew.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdNew.Location = New System.Drawing.Point(309, 272)
        Me.cmdNew.Name = "cmdNew"
        Me.cmdNew.Size = New System.Drawing.Size(100, 40)
        Me.cmdNew.TabIndex = 5
        Me.cmdNew.Text = "New (F6)"
        Me.cmdNew.UseVisualStyleBackColor = False
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(3, -10)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 92)
        Me.pic.TabIndex = 858
        Me.pic.TabStop = False
        '
        'grdCountry
        '
        Me.grdCountry.AllowUserToAddRows = False
        Me.grdCountry.AllowUserToDeleteRows = False
        Me.grdCountry.AllowUserToResizeColumns = False
        Me.grdCountry.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdCountry.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdCountry.ColumnHeadersHeight = 25
        Me.grdCountry.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cCountryCode, Me.cCountryName, Me.cTypeCol})
        Me.grdCountry.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdCountry.Location = New System.Drawing.Point(3, 85)
        Me.grdCountry.Name = "grdCountry"
        Me.grdCountry.RowHeadersVisible = False
        Me.grdCountry.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdCountry.Size = New System.Drawing.Size(455, 174)
        Me.grdCountry.TabIndex = 859
        '
        'cCountryCode
        '
        Me.cCountryCode.HeaderText = "Country Code"
        Me.cCountryCode.Name = "cCountryCode"
        Me.cCountryCode.Width = 150
        '
        'cCountryName
        '
        Me.cCountryName.HeaderText = "Country Name"
        Me.cCountryName.Name = "cCountryName"
        Me.cCountryName.Width = 200
        '
        'cTypeCol
        '
        Me.cTypeCol.HeaderText = "Type"
        Me.cTypeCol.Name = "cTypeCol"
        '
        'frmCountry
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(760, 325)
        Me.Controls.Add(Me.grdCountry)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.cmdDelete)
        Me.Controls.Add(Me.cmdUpdate)
        Me.Controls.Add(Me.cmdNew)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCountry"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Country"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.errP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCountry, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub frmCountry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'subGridSetting(grdCountry)
        cboType.SelectedIndex = 0
        Dm.SetMasterDataSet(" Select * from tblCountry", "tblCountry")
        DispCountryDetails("")
        'txtCountryCode.ReadOnly = True
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNew.Click
        clearall()
        txtCountryCode.ReadOnly = False
        txtCountryCode.Focus()
    End Sub

    Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click
        If fnScreenValidate() = True Then
            If objClsCountry.fncSaveCountry(txtCountryCode.Text.Trim, txtCountryName.Text.Trim, cboType.Text.Trim, sUserID) Then
                clearall()
                DispCountryDetails("")
                MsgBox("Saved Successfully!", MsgBoxStyle.Information)
            Else
                MsgBox("Error in Save/Update !", MsgBoxStyle.Exclamation)
            End If
        End If
        Exit Sub
    End Sub
    Private Sub clearall()
        txtCountryCode.Text = ""
        txtCountryName.Text = ""
        cboType.Text = "GOLD"
    End Sub

    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        Dim dr As DataRow()
        If grdCountry.RowCount > 0 Then
            If MsgBox("Do you want to Delete ?" & Trim(fnGetdgvText(grdCountry, grdCountry.CurrentRow.Index, CountryCODE)), MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If objClsCountry.CountryDelete(txtCountryCode.Text.Trim) Then
                    clearall()
                    DispCountryDetails("")
                Else
                    MsgBox("No such user!", MsgBoxStyle.Exclamation)
                End If
            End If
        End If
        Exit Sub
    End Sub
    Public Sub DispCountryDetails(ByVal sCode As String)
        Dim ds As DataSet
        Dim iCnt As Integer
        Try
            ds = objClsCountry.GetCountryDetail(sCode)
            grdCountry.RowCount = 0
            If ds.Tables(0).Rows.Count > 0 Then
                For iCnt = 0 To ds.Tables(0).Rows.Count - 1
                    grdCountry.Rows.Add()
                    fnSetdgvText(grdCountry, iCnt, 0, ds.Tables(0).Rows(iCnt)("countrycode") & "")
                    fnSetdgvText(grdCountry, iCnt, 1, ds.Tables(0).Rows(iCnt)("countryname") & "")
                    fnSetdgvText(grdCountry, iCnt, 2, ds.Tables(0).Rows(iCnt)("Type") & "")

                Next
            End If
            If grdCountry.RowCount > 0 Then
                txtCountryCode.Text = Trim(fnGetdgvText(grdCountry, 0, 0))
                txtCountryName.Text = Trim(fnGetdgvText(grdCountry, 0, 1))
                cboType.Text = Trim(fnGetdgvText(grdCountry, 0, 2))
                txtCountryCode.ReadOnly = True
                txtCountryCode.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            ds = Nothing
        End Try
    End Sub

    Public Function fnScreenValidate() As Boolean
        fnScreenValidate = False
        If Len(Trim(txtCountryCode.Text)) = 0 Then
            MsgBox("Please enter Country Code !", MsgBoxStyle.Critical)
            txtCountryCode.Focus()
            Exit Function
        End If

        If Len(Trim(txtCountryName.Text)) = 0 Then
            MsgBox("Please enter Country Name !", MsgBoxStyle.Critical)
            txtCountryName.Focus()
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
    Private Sub frmCountry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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

    Private Sub grdCountry_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCountry.CellClick
        txtCountryCode.Text = fnGetdgvText(grdCountry, grdCountry.CurrentRow.Index, 0)
        txtCountryName.Text = fnGetdgvText(grdCountry, grdCountry.CurrentRow.Index, 1)
        cboType.Text = Trim(fnGetdgvText(grdCountry, grdCountry.CurrentRow.Index, 2))
    End Sub

    Private Sub grdCountry_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCountry.CellContentClick

    End Sub

    Private Sub grdCountry_Click(sender As Object, e As System.EventArgs) Handles grdCountry.Click
       
    End Sub

    Private Sub grdCountry_DoubleClick(sender As Object, e As System.EventArgs) Handles grdCountry.DoubleClick
       
    End Sub
End Class
