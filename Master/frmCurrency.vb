Public Class frmCurrency
    Inherits System.Windows.Forms.Form
    Dim Dm As New DataManager
    Dim objClsCurrency As New clsCurrency
    Const CURRENCYCODE = 1
    Const CURRENCYNAME = 2
    Const RATE = 3
    Const REMARKS = 4
    

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
    Friend WithEvents grdCurrency As AxFPSpread.AxvaSpread
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdDelete As System.Windows.Forms.Button
    Friend WithEvents cmdUpdate As System.Windows.Forms.Button
    Friend WithEvents cmdNew As System.Windows.Forms.Button
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents txtCurrencyName As System.Windows.Forms.TextBox
    Friend WithEvents txtCurrencyCode As System.Windows.Forms.TextBox
    Friend WithEvents txtCurrencyRate As System.Windows.Forms.TextBox
    Friend WithEvents errP As System.Windows.Forms.ErrorProvider
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmCurrency))
        Me.grdCurrency = New AxFPSpread.AxvaSpread
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtCurrencyRate = New System.Windows.Forms.TextBox
        Me.txtRemarks = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtCurrencyName = New System.Windows.Forms.TextBox
        Me.txtCurrencyCode = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmdClose = New System.Windows.Forms.Button
        Me.cmdDelete = New System.Windows.Forms.Button
        Me.cmdUpdate = New System.Windows.Forms.Button
        Me.cmdNew = New System.Windows.Forms.Button
        Me.errP = New System.Windows.Forms.ErrorProvider
        Me.pic = New System.Windows.Forms.PictureBox
        CType(Me.grdCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdCurrency
        '
        Me.grdCurrency.Location = New System.Drawing.Point(20, 80)
        Me.grdCurrency.Name = "grdCurrency"
        Me.grdCurrency.OcxState = CType(resources.GetObject("grdCurrency.OcxState"), System.Windows.Forms.AxHost.State)
        Me.grdCurrency.Size = New System.Drawing.Size(420, 152)
        Me.grdCurrency.TabIndex = 86
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.GroupBox1.Controls.Add(Me.txtCurrencyRate)
        Me.GroupBox1.Controls.Add(Me.txtRemarks)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtCurrencyName)
        Me.GroupBox1.Controls.Add(Me.txtCurrencyCode)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(456, 80)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(271, 156)
        Me.GroupBox1.TabIndex = 850
        Me.GroupBox1.TabStop = False
        '
        'txtCurrencyRate
        '
        Me.txtCurrencyRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrencyRate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCurrencyRate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyRate.Location = New System.Drawing.Point(101, 84)
        Me.txtCurrencyRate.MaxLength = 30
        Me.txtCurrencyRate.Name = "txtCurrencyRate"
        Me.txtCurrencyRate.Size = New System.Drawing.Size(155, 22)
        Me.txtCurrencyRate.TabIndex = 2
        Me.txtCurrencyRate.Text = ""
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(101, 118)
        Me.txtRemarks.MaxLength = 50
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(155, 22)
        Me.txtRemarks.TabIndex = 3
        Me.txtRemarks.Text = ""
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(8, 125)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(96, 23)
        Me.Label6.TabIndex = 29
        Me.Label6.Text = "Remarks"
        '
        'txtCurrencyName
        '
        Me.txtCurrencyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrencyName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCurrencyName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyName.Location = New System.Drawing.Point(101, 50)
        Me.txtCurrencyName.MaxLength = 50
        Me.txtCurrencyName.Name = "txtCurrencyName"
        Me.txtCurrencyName.Size = New System.Drawing.Size(155, 22)
        Me.txtCurrencyName.TabIndex = 1
        Me.txtCurrencyName.Text = ""
        '
        'txtCurrencyCode
        '
        Me.txtCurrencyCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtCurrencyCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrencyCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCurrencyCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtCurrencyCode.Location = New System.Drawing.Point(101, 16)
        Me.txtCurrencyCode.MaxLength = 20
        Me.txtCurrencyCode.Name = "txtCurrencyCode"
        Me.txtCurrencyCode.Size = New System.Drawing.Size(155, 22)
        Me.txtCurrencyCode.TabIndex = 0
        Me.txtCurrencyCode.Text = ""
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(8, 89)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(96, 24)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "Currency Rate"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(8, 54)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 23)
        Me.Label3.TabIndex = 26
        Me.Label3.Text = "Currency Name"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(8, 19)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 23)
        Me.Label5.TabIndex = 25
        Me.Label5.Text = "Currency code"
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(647, 264)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(80, 24)
        Me.cmdClose.TabIndex = 7
        Me.cmdClose.Text = "Close (Esc)"
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.cmdDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.Location = New System.Drawing.Point(562, 264)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(80, 24)
        Me.cmdDelete.TabIndex = 6
        Me.cmdDelete.Text = "Delete (F8)"
        '
        'cmdUpdate
        '
        Me.cmdUpdate.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.cmdUpdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUpdate.Image = CType(resources.GetObject("cmdUpdate.Image"), System.Drawing.Image)
        Me.cmdUpdate.Location = New System.Drawing.Point(477, 264)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.Size = New System.Drawing.Size(80, 24)
        Me.cmdUpdate.TabIndex = 5
        Me.cmdUpdate.Text = "Update (F7)"
        '
        'cmdNew
        '
        Me.cmdNew.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.cmdNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdNew.Image = CType(resources.GetObject("cmdNew.Image"), System.Drawing.Image)
        Me.cmdNew.Location = New System.Drawing.Point(392, 264)
        Me.cmdNew.Name = "cmdNew"
        Me.cmdNew.Size = New System.Drawing.Size(80, 24)
        Me.cmdNew.TabIndex = 4
        Me.cmdNew.Text = "New (F6)"
        '
        'errP
        '
        Me.errP.ContainerControl = Me
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(0, 0)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(210, 75)
        Me.pic.TabIndex = 87
        Me.pic.TabStop = False
        '
        'frmCurrency
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.CancelButton = Me.cmdClose
        Me.ClientSize = New System.Drawing.Size(746, 302)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.grdCurrency)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.cmdDelete)
        Me.Controls.Add(Me.cmdUpdate)
        Me.Controls.Add(Me.cmdNew)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmCurrency"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Currency"
        CType(Me.grdCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub cmdNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNew.Click
        clearall()
        txtCurrencyCode.ReadOnly = False
        txtCurrencyCode.Focus()
       
    End Sub

    Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click
        If fnScreenValidate() = True Then
            If objClsCurrency.fncSaveCurrency(txtCurrencyCode.Text.Trim, txtCurrencyName.Text.Trim, Val(txtCurrencyRate.Text.Trim), txtRemarks.Text.Trim, sUserID) Then
                clearall()
                DispCurrencyDetails("")
                MsgBox("Saved Successfully!", MsgBoxStyle.Information)
            Else
                MsgBox("Error in Save/Update !", MsgBoxStyle.Exclamation)
            End If
        End If
        Exit Sub
    End Sub
    Private Sub clearall()
        txtCurrencyCode.Text = ""
        txtCurrencyName.Text = ""
        txtCurrencyRate.Text = ""
        txtRemarks.Text = ""
    End Sub

    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        Dim dr As DataRow()
        If grdCurrency.MaxRows > 1 Then
            If MsgBox("Do you want to Delete ?" & Trim(fnGetText(grdCurrency, grdCurrency.ActiveRow, CURRENCYCODE)), MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If objClsCurrency.CurrencyDelete(txtCurrencyCode.Text.Trim) Then
                    clearall()
                    DispCurrencyDetails("")
                Else
                    MsgBox("No such user!", MsgBoxStyle.Exclamation)
                End If
            End If
        End If
        Exit Sub
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Public Function fnScreenValidate() As Boolean
        fnScreenValidate = False
        If Len(Trim(txtCurrencyCode.Text)) = 0 Then
            MsgBox("Please enter Currency Code !", MsgBoxStyle.Critical)
            txtCurrencyCode.Focus()
            Exit Function
        End If

        If Len(Trim(txtCurrencyName.Text)) = 0 Then
            MsgBox("Please enter Currency Name !", MsgBoxStyle.Critical)
            txtCurrencyName.Focus()
            Exit Function
        End If
        If Len(Trim(txtCurrencyRate.Text)) = 0 Then
            MsgBox("Please enter Rate !", MsgBoxStyle.Critical)
            txtCurrencyRate.Focus()
            Exit Function
        Else
            If Not IsNumeric(txtCurrencyRate.Text) Then
                MsgBox("Please enter numeric value to Rate !", MsgBoxStyle.Information)
                txtCurrencyRate.Text = ""
                txtCurrencyRate.Focus()
                Exit Function
            End If
        End If
        fnScreenValidate = True
        Exit Function
    End Function

    Private Sub frmCurrency_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                cmdClose_Click(sender, e)
            Case Keys.Enter
                SendKeys.Send("{tab}")
            Case Keys.F6
                cmdNew_Click(sender, e)
            Case Keys.F7
                cmdUpdate_Click(sender, e)
            Case Keys.F8
                cmdDelete_Click(sender, e)
        End Select
    End Sub
    Public Sub DispCurrencyDetails(ByVal sCode As String)
        Dim ds As DataSet
        Dim iCnt As Integer
        Try
            ds = objClsCurrency.GetCurrencyDetail(sCode)
            grdCurrency.MaxRows = 0
            If ds.Tables(0).Rows.Count > 0 Then
                For iCnt = 0 To ds.Tables(0).Rows.Count - 1
                    grdCurrency.MaxRows += 1
                    fnSetText(grdCurrency, iCnt + 1, CURRENCYCODE, ds.Tables(0).Rows(iCnt)("currencycode") & "")
                    fnSetText(grdCurrency, iCnt + 1, CURRENCYNAME, ds.Tables(0).Rows(iCnt)("currencyname") & "")
                    fnSetText(grdCurrency, iCnt + 1, RATE, ds.Tables(0).Rows(iCnt)("currencyrate") & "")
                    fnSetText(grdCurrency, iCnt + 1, REMARKS, ds.Tables(0).Rows(iCnt)("remarks") & "")
                Next
            End If
            If grdCurrency.MaxRows > 0 Then
                txtCurrencyCode.Text = Trim(fnGetText(grdCurrency, 1, CURRENCYCODE))
                txtCurrencyName.Text = Trim(fnGetText(grdCurrency, 1, CURRENCYNAME))
                txtCurrencyRate.Text = Trim(fnGetText(grdCurrency, 1, RATE))
                txtRemarks.Text = Trim(fnGetText(grdCurrency, 1, REMARKS))
                txtCurrencyCode.ReadOnly = True
                txtCurrencyCode.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            ds = Nothing
        End Try
    End Sub

    Private Sub frmUserGroup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        subGridSetting(grdCurrency)
        Dm.SetMasterDataSet(" Select * from tblusergroup", "tblUserGroup")
        DispCurrencyDetails("")
        txtCurrencyCode.ReadOnly = True
    End Sub
    Private Sub grdCurrency_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_ClickEvent) Handles grdCurrency.ClickEvent
        If e.row > 0 Then
            txtCurrencyCode.Text = Trim(fnGetText(grdCurrency, e.row, CURRENCYCODE))
            txtCurrencyName.Text = Trim(fnGetText(grdCurrency, e.row, CURRENCYNAME))
            txtCurrencyRate.Text = Trim(fnGetText(grdCurrency, e.row, RATE))
            txtRemarks.Text = Trim(fnGetText(grdCurrency, e.row, REMARKS))
            txtCurrencyCode.ReadOnly = True
        End If
    End Sub

    Private Sub txtCurrencyCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtCurrencyCode.Validating
        If Len(Trim(txtCurrencyCode.Text)) = 0 Then
            errP.SetError(txtCurrencyCode, "Compulsary!")
            txtCurrencyCode.Focus()
        Else
            errP.SetError(txtCurrencyCode, "")
        End If
    End Sub

    Private Sub txtCurrencyName_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtCurrencyName.Validating
        If Len(Trim(txtCurrencyName.Text)) = 0 Then
            errP.SetError(txtCurrencyName, "Compulsary!")
            txtCurrencyName.Focus()
        Else
            errP.SetError(txtCurrencyName, "")
        End If
    End Sub

    Private Sub txtCurrencyRate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtCurrencyRate.Validating
        If Len(Trim(txtCurrencyRate.Text)) = 0 Then
            errP.SetError(txtCurrencyRate, "Compulsary!")
            txtCurrencyRate.Focus()
        Else
            errP.SetError(txtCurrencyRate, "")
        End If
    End Sub

    Private Sub txtCurrencyCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCurrencyCode.LostFocus
        Dim ds As New DataSet
        ds = objClsCurrency.GetCurrencyDetail(txtCurrencyCode.Text.Trim)
        If ds.Tables(0).Rows.Count > 0 Then
            txtCurrencyCode.Text = Trim(ds.Tables(0).Rows(0)("currencyCode") & "")
            txtCurrencyName.Text = Trim(ds.Tables(0).Rows(0)("currencyName") & "")
            txtCurrencyRate.Text = Trim(ds.Tables(0).Rows(0)("currencyrate") & "")
            txtRemarks.Text = Trim(ds.Tables(0).Rows(0)("remarks") & "")
            txtCurrencyCode.ReadOnly = True
        End If

    End Sub
End Class


