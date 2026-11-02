Public Class frmFixCostJobSheet
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
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnJobSheetNoSearch As System.Windows.Forms.Button
    Friend WithEvents txtJobSheetNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtSupplier As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cboClass As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents txtIYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtIMM As System.Windows.Forms.TextBox
    Friend WithEvents txtIDD As System.Windows.Forms.TextBox
    Friend WithEvents txtNewCost As System.Windows.Forms.TextBox
    Friend WithEvents txtOldCost As System.Windows.Forms.TextBox
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtCashValue As System.Windows.Forms.TextBox
    Friend WithEvents txtGoldValue As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFixCostJobSheet))
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtNewCost = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtOldCost = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboClass = New System.Windows.Forms.ComboBox()
        Me.txtSupplier = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtIYYYY = New System.Windows.Forms.TextBox()
        Me.txtIMM = New System.Windows.Forms.TextBox()
        Me.txtIDD = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnJobSheetNoSearch = New System.Windows.Forms.Button()
        Me.txtJobSheetNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtGoldValue = New System.Windows.Forms.TextBox()
        Me.txtCashValue = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(248, 459)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 13
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.txtCashValue)
        Me.Panel1.Controls.Add(Me.txtGoldValue)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.txtNewCost)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.txtOldCost)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.cboClass)
        Me.Panel1.Controls.Add(Me.txtSupplier)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtIYYYY)
        Me.Panel1.Controls.Add(Me.txtIMM)
        Me.Panel1.Controls.Add(Me.txtIDD)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.txtInvoiceNo)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.btnJobSheetNoSearch)
        Me.Panel1.Controls.Add(Me.txtJobSheetNo)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(12, 108)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(336, 330)
        Me.Panel1.TabIndex = 36
        '
        'txtNewCost
        '
        Me.txtNewCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewCost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewCost.Location = New System.Drawing.Point(151, 224)
        Me.txtNewCost.MaxLength = 20
        Me.txtNewCost.Name = "txtNewCost"
        Me.txtNewCost.Size = New System.Drawing.Size(160, 20)
        Me.txtNewCost.TabIndex = 57
        Me.txtNewCost.Tag = "NE;"
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(23, 223)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(112, 16)
        Me.Label9.TabIndex = 58
        Me.Label9.Text = "New Cost Per Gram"
        '
        'txtOldCost
        '
        Me.txtOldCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOldCost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOldCost.Location = New System.Drawing.Point(151, 191)
        Me.txtOldCost.MaxLength = 20
        Me.txtOldCost.Name = "txtOldCost"
        Me.txtOldCost.Size = New System.Drawing.Size(160, 20)
        Me.txtOldCost.TabIndex = 55
        Me.txtOldCost.Tag = "NE;"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(23, 191)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(112, 16)
        Me.Label8.TabIndex = 56
        Me.Label8.Text = "Old Cost Per Gram"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(23, 159)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(96, 16)
        Me.Label6.TabIndex = 54
        Me.Label6.Text = "Class"
        '
        'cboClass
        '
        Me.cboClass.BackColor = System.Drawing.Color.White
        Me.cboClass.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboClass.Location = New System.Drawing.Point(151, 156)
        Me.cboClass.Name = "cboClass"
        Me.cboClass.Size = New System.Drawing.Size(160, 22)
        Me.cboClass.TabIndex = 53
        '
        'txtSupplier
        '
        Me.txtSupplier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplier.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplier.Location = New System.Drawing.Point(151, 123)
        Me.txtSupplier.MaxLength = 20
        Me.txtSupplier.Name = "txtSupplier"
        Me.txtSupplier.Size = New System.Drawing.Size(160, 20)
        Me.txtSupplier.TabIndex = 51
        Me.txtSupplier.Tag = "NE;"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(23, 127)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 16)
        Me.Label5.TabIndex = 52
        Me.Label5.Text = "Supplier"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(207, 88)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(8, 16)
        Me.Label3.TabIndex = 50
        Me.Label3.Text = "-"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(175, 88)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(8, 16)
        Me.Label4.TabIndex = 49
        Me.Label4.Text = "-"
        '
        'txtIYYYY
        '
        Me.txtIYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIYYYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIYYYY.Location = New System.Drawing.Point(223, 90)
        Me.txtIYYYY.MaxLength = 4
        Me.txtIYYYY.Name = "txtIYYYY"
        Me.txtIYYYY.Size = New System.Drawing.Size(32, 20)
        Me.txtIYYYY.TabIndex = 47
        Me.txtIYYYY.Tag = "ENN;"
        '
        'txtIMM
        '
        Me.txtIMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIMM.Location = New System.Drawing.Point(183, 90)
        Me.txtIMM.MaxLength = 2
        Me.txtIMM.Name = "txtIMM"
        Me.txtIMM.Size = New System.Drawing.Size(20, 20)
        Me.txtIMM.TabIndex = 46
        Me.txtIMM.Tag = "ENN;"
        '
        'txtIDD
        '
        Me.txtIDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIDD.Location = New System.Drawing.Point(151, 90)
        Me.txtIDD.MaxLength = 2
        Me.txtIDD.Name = "txtIDD"
        Me.txtIDD.Size = New System.Drawing.Size(20, 20)
        Me.txtIDD.TabIndex = 45
        Me.txtIDD.Tag = "ENN;"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(23, 88)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(88, 23)
        Me.Label7.TabIndex = 48
        Me.Label7.Text = "Invoice Date"
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvoiceNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceNo.Location = New System.Drawing.Point(151, 57)
        Me.txtInvoiceNo.MaxLength = 20
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(160, 20)
        Me.txtInvoiceNo.TabIndex = 39
        Me.txtInvoiceNo.Tag = "NE;"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(23, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 16)
        Me.Label2.TabIndex = 40
        Me.Label2.Text = "Invoice No"
        '
        'btnJobSheetNoSearch
        '
        Me.btnJobSheetNoSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnJobSheetNoSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnJobSheetNoSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnJobSheetNoSearch.Location = New System.Drawing.Point(287, 24)
        Me.btnJobSheetNoSearch.Name = "btnJobSheetNoSearch"
        Me.btnJobSheetNoSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnJobSheetNoSearch.TabIndex = 37
        Me.btnJobSheetNoSearch.Text = "..."
        Me.btnJobSheetNoSearch.UseVisualStyleBackColor = False
        '
        'txtJobSheetNo
        '
        Me.txtJobSheetNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJobSheetNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJobSheetNo.Location = New System.Drawing.Point(151, 24)
        Me.txtJobSheetNo.MaxLength = 20
        Me.txtJobSheetNo.Name = "txtJobSheetNo"
        Me.txtJobSheetNo.Size = New System.Drawing.Size(136, 20)
        Me.txtJobSheetNo.TabIndex = 36
        Me.txtJobSheetNo.Tag = "NE;"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(23, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 16)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "JobSheet No"
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(36, 459)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 40)
        Me.btnSave.TabIndex = 37
        Me.btnSave.Text = "Save(F7)"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.Location = New System.Drawing.Point(142, 459)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(100, 40)
        Me.btnClear.TabIndex = 38
        Me.btnClear.Text = "Clear(F6)"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(3, 2)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 569
        Me.pic.TabStop = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(20, 251)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(59, 13)
        Me.Label10.TabIndex = 476260
        Me.Label10.Text = "Payable"
        '
        'txtGoldValue
        '
        Me.txtGoldValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldValue.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldValue.Location = New System.Drawing.Point(151, 275)
        Me.txtGoldValue.MaxLength = 20
        Me.txtGoldValue.Name = "txtGoldValue"
        Me.txtGoldValue.Size = New System.Drawing.Size(160, 20)
        Me.txtGoldValue.TabIndex = 476261
        Me.txtGoldValue.Tag = "NE;"
        '
        'txtCashValue
        '
        Me.txtCashValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCashValue.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCashValue.Location = New System.Drawing.Point(151, 301)
        Me.txtCashValue.MaxLength = 20
        Me.txtCashValue.Name = "txtCashValue"
        Me.txtCashValue.Size = New System.Drawing.Size(160, 20)
        Me.txtCashValue.TabIndex = 476262
        Me.txtCashValue.Tag = "NE;"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(23, 277)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(36, 13)
        Me.Label11.TabIndex = 476263
        Me.Label11.Text = "Gold"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(23, 303)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(38, 13)
        Me.Label12.TabIndex = 476264
        Me.Label12.Text = "Cash"
        '
        'frmFixCostJobSheet
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(367, 522)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnClose)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmFixCostJobSheet"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "JobSheet Costing"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region
    Dim objClsJobsheet As New ClsFixCostJobSheet
    Dim dm As New DataManager

    Private Sub frmFixCostJobSheet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnJobSheetNoSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJobSheetNoSearch.Click
        Dim frmHelpJohSheet As New frmHelpSearch(0, 0, "tblJobSheetHeader", "L", 4, "JobSheetNo", "FixStatus='N'", "JobSheetNo", "JobSheetNo", 80, "CostPerGram", "CostPerGram", 80, "SupplierCode", "SupplierCode", 100, "PurchaseBillNo", "PurchaseBillNo", 80)
        Try
            frmHelpJohSheet.ShowDialog()
            txtJobSheetNo.Text = frmHelpJohSheet.Var1.Trim
            subGetJobSheetDetail()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            frmHelpJohSheet = Nothing
        End Try

    End Sub

    Private Sub txtJobSheetNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtJobSheetNo.KeyDown
        If e.KeyCode = Keys.F1 Then btnJobSheetNoSearch_Click(sender, e)
    End Sub

    Private Sub subGetJobSheetDetail()
        Dim dsJSD As New DataSet
        Try
            If txtJobSheetNo.Text.Trim <> "" Then
                dsJSD = objClsJobsheet.GetJobSheetDetail(txtJobSheetNo.Text.Trim)
                If dsJSD.Tables(0).Rows.Count > 0 Then
                    txtInvoiceNo.Text = Trim(dsJSD.Tables(0).Rows(0)("PurchaseBillNo") & "")
                    txtIDD.Text = Format(CDate(Trim(dsJSD.Tables(0).Rows(0)("PurchaseBillDate") & "")), "dd")
                    txtIMM.Text = Format(CDate(Trim(dsJSD.Tables(0).Rows(0)("PurchaseBillDate") & "")), "MM")
                    txtIYYYY.Text = Format(CDate(Trim(dsJSD.Tables(0).Rows(0)("PurchaseBillDate") & "")), "yyyy")
                    txtSupplier.Text = Trim(dsJSD.Tables(0).Rows(0)("SupplierCode") & "")
                    subLoadClassCombo()
                Else
                    MessageBox.Show("Jobsheet No not found.")
                    txtJobSheetNo.Text = ""
                    If txtJobSheetNo.Enabled = True Then txtJobSheetNo.Focus()
                End If
            Else
                cboClass.SelectedIndex = -1
                txtOldCost.Clear()
                txtNewCost.Clear()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmFixCostJobSheet_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F6
                btnClear_Click(sender, e)
            Case Keys.F7
                btnSave_Click(sender, e)
            Case Keys.Escape
                btnClose_Click(sender, e)
            Case Keys.Return
                SendKeys.Send("{Tab}")
            Case Keys.Down
                SendKeys.Send("{Tab}")
            Case Keys.Up
                SendKeys.Send("+{Tab}")
        End Select
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        subSave()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        subClearMe()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub subClearMe()
        txtJobSheetNo.Clear()
        txtInvoiceNo.Clear()
        txtIDD.Clear()
        txtIMM.Clear()
        txtIYYYY.Clear()
        cboClass.SelectedIndex = -1
        txtOldCost.Clear()
        txtNewCost.Clear()
    End Sub

    Private Sub subLoadClassCombo()
        Dim dsClass As New DataSet
        Dim iCnt As Integer
        Dim dvclass As New DataView
        Dim sQry As String
        Try
            dsClass = objClsJobsheet.GetJobSheetClass(txtJobSheetNo.Text.Trim)
            If dsClass.Tables(0).Rows.Count > 0 Then
                For iCnt = 0 To dsClass.Tables(0).Rows.Count - 1
                    cboClass.Items.Add(Trim(dsClass.Tables(0).Rows(iCnt)("ClassName")))
                Next
                cboClass.Items.Insert(0, "All")
            Else
                cboClass.Items.Clear()
                txtOldCost.Clear()
                txtNewCost.Clear()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub txtJobSheetNo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtJobSheetNo.LostFocus
        subGetJobSheetDetail()
    End Sub

    Private Sub cboClass_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboClass.SelectedIndexChanged
        subGetOldCostPerGram()
    End Sub

    Private Sub subGetOldCostPerGram()
        Dim dsCost As New DataSet
        Dim sQry As String
        Try
            If cboClass.SelectedItem <> "All" Then
                sQry = "Select CostPerGram from tblJobSheetDetail where JobSheetNo = " & fncFormatSQLText(Trim$(txtJobSheetNo.Text)) & _
                " And CategoryCode in (Select CategoryCode from tblCategory where ClassCode = (Select ClassCode from tblClass where ClassName = " & _
                fncFormatSQLText(Trim(cboClass.SelectedItem)) & "))"
            Else
                sQry = "Select CostPerGram from tblJobSheetHeader where JobSheetNo = " & fncFormatSQLText(Trim$(txtJobSheetNo.Text))
            End If
            dsCost = objClsJobsheet.GetOldCostPerGram(sQry)
            If dsCost.Tables(0).Rows.Count > 0 Then
                txtOldCost.Text = Format(CDbl(dsCost.Tables(0).Rows(0)("CostPerGram")), "#0.00")
                txtNewCost.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subSave()
        Dim sClassCode As String
        Try
            If fncValidateMe() Then
                sClassCode = objClsJobsheet.GetClassCode(cboClass.SelectedItem)
                If objClsJobsheet.UpdateCost(txtJobSheetNo.Text.Trim, sClassCode, txtOldCost.Text.Trim, txtNewCost.Text.Trim, Val(txtGoldValue.Text), Val(txtCashValue.Text)) Then
                    MessageBox.Show("Cost updated successfully!")
                    subClearMe()
                Else
                    MessageBox.Show("Error in Save.")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function fncValidateMe() As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            If txtJobSheetNo.Text.Trim = "" Then
                MessageBox.Show("JobsheeNo should not be blank.")
                txtJobSheetNo.Focus()
                Exit Function
            End If
            If txtNewCost.Text.Trim = "" Then
                MessageBox.Show("New cost should not be blank.")
                txtNewCost.Focus()
                Exit Function
            ElseIf Not IsNumeric(txtNewCost.Text.Trim) Then
                MessageBox.Show("New cost should be numeric.")
                txtNewCost.Focus()
                Exit Function
            End If
            If Trim(cboClass.SelectedItem) = "" Then
                MessageBox.Show("Please select the classCode.")
                cboClass.Focus()
                Exit Function
            End If
            'If objClsJobsheet.IsBillClosed(txtJobSheetNo.Text.Trim) Then
            '    MessageBox.Show("Bill already closed. Cannot Fix Cost.")
            '    Exit Function
            'End If
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            bStatus = False
        Finally
            fncValidateMe = bStatus
        End Try

    End Function

End Class
