Public Class frmClassDisplay
    Inherits System.Windows.Forms.Form
    Dim Dm As New DataManager
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
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents grdClass As AxFPSpread.AxvaSpread
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmClassDisplay))
        Me.cmdClose = New System.Windows.Forms.Button
        Me.pic = New System.Windows.Forms.PictureBox
        Me.grdClass = New AxFPSpread.AxvaSpread
        CType(Me.grdClass, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdClose
        '
        Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdClose.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.Color.Black
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(512, 272)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(80, 23)
        Me.cmdClose.TabIndex = 476169
        Me.cmdClose.Tag = "A"
        Me.cmdClose.Text = "&Close(ESC)"
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(0, 0)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(216, 75)
        Me.pic.TabIndex = 476172
        Me.pic.TabStop = False
        '
        'grdClass
        '
        Me.grdClass.Location = New System.Drawing.Point(8, 80)
        Me.grdClass.Name = "grdClass"
        Me.grdClass.OcxState = CType(resources.GetObject("grdClass.OcxState"), System.Windows.Forms.AxHost.State)
        Me.grdClass.Size = New System.Drawing.Size(576, 176)
        Me.grdClass.TabIndex = 476166
        '
        'frmClassDisplay
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(600, 302)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.grdClass)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmClassDisplay"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Stock on Hand"
        CType(Me.grdClass, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private Sub frmClassDisplay_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        fnGridClearText(grdClass)
        DispClassDetails()
    End Sub

    Public Sub DispClassDetails()
        Dim DrD As DataRow()
        Dim DrTradeIn As DataRow()
        Dim r As Integer
        Dim s As Integer
        DrD = DataManager.RowReturnProjExecNonQryWithDataRow("Select ClassCode,Weight= Sum(Weight),TotalCost =Sum(TotalCost) from tblInventory where QtyonHand>0 Group by ClassCode Order by Weight Desc")
        fnGridClearText(grdClass)
        grdClass.MaxRows = 1
        If DrD.Length > 0 Then
            For r = 0 To DrD.Length - 1
                fnSetText(grdClass, r + 1, 1, DrD(r)("ClassCode") & "")
                fnSetText(grdClass, r + 1, 2, DrD(r)("Weight") & "")
                fnSetText(grdClass, r + 1, 3, DrD(r)("TotalCost") & "")
                grdClass.MaxRows += 1
            Next
            grdClass.MaxRows -= 1
        End If
        DrTradeIn = DataManager.RowReturnProjExecNonQryWithDataRow("Select Weight= Sum(TotalWeight),TotalCost =Sum(TotalCost) from tblTradeInHeader where (IssueFlag is Null) or (IssueFlag<> 'Y')")
        grdClass.MaxRows = grdClass.MaxRows + 1
        s = grdClass.MaxRows
        If DrTradeIn.Length > 0 Then
            fnSetText(grdClass, s, 1, "TRADE IN" & "")
            fnSetText(grdClass, s, 2, DrTradeIn(0)("Weight") & "")
            fnSetText(grdClass, s, 3, DrTradeIn(0)("TotalCost") & "")
            grdClass.MaxRows += 1
        End If
        
    End Sub
    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub
    Private Sub frmClassDisplay_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Up
                SendKeys.Send("+{Tab}")
            Case Keys.Down
                SendKeys.Send("{Tab}")
            Case Keys.Escape
                cmdClose_Click(sender, e)
            Case Keys.Enter
                SendKeys.Send("{tab}")
        End Select
    End Sub

End Class
