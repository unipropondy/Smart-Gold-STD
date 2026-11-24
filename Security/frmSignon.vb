Imports System.Reflection.Assembly

Public Class frmSignon
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
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents imgList As System.Windows.Forms.ImageList
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblSoftwareLicense As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtUserID As System.Windows.Forms.TextBox
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdSignin As System.Windows.Forms.Button
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents lblError As System.Windows.Forms.Label
    Friend WithEvents fraLicense As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSignon))
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.imgList = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdSignin = New System.Windows.Forms.Button()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.txtUserID = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblSoftwareLicense = New System.Windows.Forms.Label()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.lblError = New System.Windows.Forms.Label()
        Me.fraLicense = New System.Windows.Forms.Panel()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraLicense.SuspendLayout()
        Me.SuspendLayout()
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(0, 0)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 200)
        Me.pic.TabIndex = 9
        Me.pic.TabStop = False
        '
        'imgList
        '
        Me.imgList.ImageStream = CType(resources.GetObject("imgList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgList.TransparentColor = System.Drawing.Color.Transparent
        Me.imgList.Images.SetKeyName(0, "")
        Me.imgList.Images.SetKeyName(1, "")
        Me.imgList.Images.SetKeyName(2, "")
        Me.imgList.Images.SetKeyName(3, "")
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Panel1.Controls.Add(Me.cmdCancel)
        Me.Panel1.Controls.Add(Me.cmdSignin)
        Me.Panel1.Controls.Add(Me.txtPassword)
        Me.Panel1.Controls.Add(Me.txtUserID)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(290, 60)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(290, 140)
        Me.Panel1.TabIndex = 17
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.Location = New System.Drawing.Point(168, 96)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(90, 30)
        Me.cmdCancel.TabIndex = 14
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'cmdSignin
        '
        Me.cmdSignin.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdSignin.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdSignin.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSignin.Location = New System.Drawing.Point(64, 96)
        Me.cmdSignin.Name = "cmdSignin"
        Me.cmdSignin.Size = New System.Drawing.Size(90, 30)
        Me.cmdSignin.TabIndex = 13
        Me.cmdSignin.Text = "Signin"
        Me.cmdSignin.UseVisualStyleBackColor = False
        '
        'txtPassword
        '
        Me.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPassword.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword.Location = New System.Drawing.Point(112, 56)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(145, 21)
        Me.txtPassword.TabIndex = 12
        '
        'txtUserID
        '
        Me.txtUserID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUserID.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserID.Location = New System.Drawing.Point(112, 26)
        Me.txtUserID.Name = "txtUserID"
        Me.txtUserID.Size = New System.Drawing.Size(145, 21)
        Me.txtUserID.TabIndex = 11
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(24, 66)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 16)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Password :"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(24, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 20)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "User Name"
        '
        'lblSoftwareLicense
        '
        Me.lblSoftwareLicense.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSoftwareLicense.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSoftwareLicense.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSoftwareLicense.ForeColor = System.Drawing.Color.White
        Me.lblSoftwareLicense.Location = New System.Drawing.Point(290, 0)
        Me.lblSoftwareLicense.Name = "lblSoftwareLicense"
        Me.lblSoftwareLicense.Size = New System.Drawing.Size(290, 60)
        Me.lblSoftwareLicense.TabIndex = 8
        Me.lblSoftwareLicense.Text = "Company Name"
        Me.lblSoftwareLicense.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureBox3
        '
        Me.PictureBox3.BackColor = System.Drawing.Color.White
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(-5, 270)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(488, 19)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox3.TabIndex = 18
        Me.PictureBox3.TabStop = False
        Me.PictureBox3.Visible = False
        '
        'lblError
        '
        Me.lblError.Location = New System.Drawing.Point(24, 16)
        Me.lblError.Name = "lblError"
        Me.lblError.Size = New System.Drawing.Size(464, 28)
        Me.lblError.TabIndex = 19
        '
        'fraLicense
        '
        Me.fraLicense.Controls.Add(Me.lblError)
        Me.fraLicense.Location = New System.Drawing.Point(41, 206)
        Me.fraLicense.Name = "fraLicense"
        Me.fraLicense.Size = New System.Drawing.Size(505, 50)
        Me.fraLicense.TabIndex = 20
        Me.fraLicense.Visible = False
        '
        'frmSignon
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(574, 198)
        Me.Controls.Add(Me.fraLicense)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lblSoftwareLicense)
        Me.Controls.Add(Me.pic)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSignon"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sign-In :: Unipro Software Pte Ltd. All Rights Reserved"
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraLicense.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Dim objTerminal As New clsTerminal
    Dim objLocation As New clsLocation
    Dim bMessageShown As Boolean
    Private Sub cmdSignin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSignin.Click
        Dim sSql$
        On Error GoTo ErrorSub
        Dim DM As New DataManager
        Dim ds As New DataSet
        Dim dsPOSCompany, dsAccLoc As DataSet
        Dim objCustomer As New clsCustomer
        ' getServerDate()
        If Trim(txtUserID.Text) = "" Then
            txtUserID.Focus()
            sLogInFlag = ""
            Exit Sub
        End If

        sWorkStation = GetComputerName()
        subGetTerminal()

        objCustomer.Customer180ExpiryUpdate()

        objCustomer.Customer270ExpiryUpdate()

        Dim SBL1 As New clsUserMaster
        ds = SBL1.UserLogin(Trim(txtUserID.Text), Trim(txtPassword.Text))

        If ds.Tables("getUser").Rows.Count > 0 Then
            Dim SBLL1 As New clsCompany1
            Dim dsCompany As New DataSet
            dsCompany = SBLL1.GetCompanyList
            Objuser.Address1 = Trim(dsCompany.Tables("getCompanyList").Rows(0)("Address1") & "")
            Objuser.Address2 = Trim(dsCompany.Tables("getCompanyList").Rows(0)("Address2") & "")
            Objuser.Phone = Trim(dsCompany.Tables("getCompanyList").Rows(0)("Phone") & "")
            Objuser.Fax = Trim(dsCompany.Tables("getCompanyList").Rows(0)("Fax") & "")
            Objuser.BillMessage1 = Trim(dsCompany.Tables("getCompanyList").Rows(0)("Message1") & "")
            Objuser.BillMessage2 = Trim(dsCompany.Tables("getCompanyList").Rows(0)("Message2") & "")
            Objuser.GSTREG = Trim(dsCompany.Tables("getCompanyList").Rows(0)("GSTREGNO") & "")
            Objuser.CompanyName = Trim(dsCompany.Tables("getCompanyList").Rows(0)("Name") & "")
            Objuser.GstPercentage = Trim(dsCompany.Tables("getCompanyList").Rows(0)("GstPerc") & "")
            Objuser.ServiceCharge = Val(Trim(dsCompany.Tables("getCompanyList").Rows(0)("ServiceCharge") & ""))
            Objuser.CompanyName = Trim(dsCompany.Tables("getCompanyList").Rows(0)("Name") & "")
            sCompanyCode = Trim(dsCompany.Tables("getCompanyList").Rows(0)("CompanyID") & "")
            lblSoftwareLicense.Text = Objuser.CompanyName
            If dsCompany.Tables("getCompanyList").Rows(0)("Member") = True Then
                Objuser.Member = True
            Else
                Objuser.Member = False
            End If
            If dsCompany.Tables("getCompanyList").Rows(0)("AllowMultipleDiscount") = True Then
                Objuser.AllowMultipleDiscount = True
            Else
                Objuser.AllowMultipleDiscount = False
            End If
            If dsCompany.Tables("getCompanyList").Rows(0)("Rental") = True Then
                Objuser.Rental = True
            Else
                Objuser.Rental = False
            End If

            gthisLocation = Trim(dsCompany.Tables("getCompanyList").Rows(0)("LocationCode") & "")
            sLocationCode = Trim(dsCompany.Tables("getCompanyList").Rows(0)("LocationCode") & "")
            gMainlocation = Trim(dsCompany.Tables("getCompanyList").Rows(0)("MainLocationCode") & "")
            subGetLocationType(sLocationCode)
            sCompanyShortCode = Trim(dsCompany.Tables("getCompanyList").Rows(0)("CompanyShortCode") & "")
            sDefaultCurrency = Trim(dsCompany.Tables("getCompanyList").Rows(0)("DefaultCurrency") & "")
            sPurchaseCostingGL = Trim(dsCompany.Tables("getCompanyList").Rows(0)("PurchaseCostingGL") & "")
            dPurWtVarPerc = Trim(dsCompany.Tables("getCompanyList").Rows(0)("PurWtVarPerc") & "")
            '''Add By Durai For Member module & Accounts Module

            sMemberModule = Trim(dsCompany.Tables("getCompanyList").Rows(0)("MemberModule") & "")
            sAccountsModule = Trim(dsCompany.Tables("getCompanyList").Rows(0)("AccountsModule") & "")

            ''Thariq On Apr 2008
            sDetailDiscount = Trim(dsCompany.Tables("getCompanyList").Rows(0)("IsDetailDiscountAllowed") & "")
            ''

            sRFIDModule = Trim(dsCompany.Tables("getCompanyList").Rows(0)("RFIDMOdule") & "")
            sUserID = Trim(txtUserID.Text)
            Objuser.UserId = Trim(txtUserID.Text)
            Objuser.Description = Trim(ds.Tables("getUser").Rows(0)("UserName") & "")
            sUserGroup = Trim(ds.Tables("getUser").Rows(0)("UserGroupCode") & "")
            If ds.Tables("getUser").Rows(0)("UserGroupCode") = "N" Then
                MessageBox.Show("Please set your password.")
            End If
            Objuser.CompanyCode = 1
            sCompanyID = 1
            Objuser.GroupCode = UCase(Trim(ds.Tables("getUser").Rows(0)("UserGroupCode") & ""))
            Objuser.UserPassword = Trim(ds.Tables("getUser").Rows(0)("Password") & "")
            Objuser.StoreCode = gthisLocation
            Objuser.CurrencyCode = "BD"
            Objuser.PricingCode = "PRIC1"
            Objuser.DiscountPricingCode = ""
            Objuser.ShipCode = "LAND"
            Objuser.FOCPricingCode = "FOC"
            Objuser.CurrencyCode = "BD"



            Objuser.ItemFrom = ""
            Objuser.ItemTo = ""
            GstTaelPercentage = Val(1 + (Val(Objuser.GstPercentage) / 100))

            If sAccountsModule = "1" Then
                dsAccLoc = SBLL1.GetCompanyListAcc
                If dsAccLoc.Tables(0).Rows.Count > 0 Then
                    sLocationCodeAcc = dsAccLoc.Tables(0).Rows(0)("LocationCode")
                    sCompanyIDAcc = 1
                End If

                sSql = "Select LocationCode From tblAccountLocation Where POSLocationCode ='HQ' "
                dsAccLoc = DM.getDataSetWithSqlAdapterAcc(sSql, "AccLocn", "AccLocn")
                If dsAccLoc.Tables("AccLocn").Rows.Count > 0 Then
                    gsAccLocnCode = Trim$(dsAccLoc.Tables("AccLocn").Rows(0).Item("LocationCode") & "")
                End If
            End If

            Dim SBLL2 As New clsCompany1
            Dim dshqCompany As New DataSet
            dshqCompany = SBLL1.GetCompanyListHQ
            dOfferday = Val(Trim(dshqCompany.Tables("getCompanyList").Rows(0)("BillPrint") & ""))

            Me.Dispose(True)
            Dim form1 As New frmMain
            form1.ShowDialog()
            Me.Dispose(True)
            Exit Sub
        Else
            sLogInFlag = ""

            MsgBox("Invalid UserID or Password")
            txtUserID.Focus()
            Exit Sub
        End If
        Exit Sub
ErrorSub:
        MsgBox(Err.Description)
        Err.Clear()
    End Sub

    Private Sub subGetTerminal()
        Dim dsTerminal As New DataSet
        Try
            dsTerminal = objTerminal.GetTerminalByComputer(Trim(sWorkStation))
            If dsTerminal.Tables(0).Rows.Count > 0 Then
                sTerminalID = Trim(dsTerminal.Tables(0).Rows(0)("TerminalName") & "")
                gTerminalCode = Trim(dsTerminal.Tables(0).Rows(0)("TerminalCode") & "")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsTerminal = Nothing
        End Try
    End Sub




    Private Sub frmSignon_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub
    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        On Error GoTo ErrorSub
        Me.Dispose(True)
        sLogInFlag = ""
        Exit Sub
ErrorSub:
        MsgBox(Err.Description)
        Err.Clear()
    End Sub
    Private Sub frmSignon_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ErrorSub
        Dim bLicense As Boolean
        Dim sErrorMsg As String

        Dim ds As New DataSet
        Dim sbll1 As New clsCompany1

        ds = sbll1.GetCompanyList()
        If ds.Tables("getCompanyList").Rows.Count > 0 Then
            sCompanyShortCode = Trim(Mid(ds.Tables("getCompanyList").Rows(0)("CompanyShortCode"), 1, 3))
        End If
        If UCase(Trim(ds.Tables("GetCompanyList").Rows(0)("Name"))) = UCase(Trim(sLicense)) Then
            lblSoftwareLicense.Text = Trim(ds.Tables("GetCompanyList").Rows(0)("Name"))
        Else
            MsgBox("Illegal License .... !!! Please get the Exact License")
            End
        End If
        'check the license
        '''bLicense = fncGetCompanyLicense(sCompanyShortCode, lblSoftwareLicense.Text, sErrorMsg)
        '''If Len(Trim$(sErrorMsg)) > 0 Then
        '''    fraLicense.Visible = True
        '''    lblError.Text = sErrorMsg
        '''    If bMessageShown = False Then
        '''        MsgBox(sErrorMsg)
        '''    End If
        '''    bMessageShown = True
        '''End If
        '''If Not bLicense Then
        '''    End

        '''End If

        Exit Sub
ErrorSub:
        MsgBox(Err.Description)
        Err.Clear()

    End Sub

    Private Sub frmSignon_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrorSub
        Dim bLicense As Boolean
        Dim sErrorMsg As String

        Dim ds As New DataSet
        Dim sbll1 As New clsCompany1



        ds = sbll1.GetCompanyList()

        If ds.Tables("getCompanyList").Rows.Count > 0 Then
            sCompanyShortCode = Trim(Mid(ds.Tables("getCompanyList").Rows(0)("CompanyShortCode"), 1, 3))
        End If

        If UCase(Trim(ds.Tables("GetCompanyList").Rows(0)("Name"))) = UCase(Trim(sLicense)) Then
            lblSoftwareLicense.Text = Trim(ds.Tables("GetCompanyList").Rows(0)("Name"))
        Else
            MsgBox("Illegal License .... !!! Please get the Exact License")
            End
        End If

        Exit Sub
ErrorSub:
        MsgBox(Err.Description)
        Err.Clear()
    End Sub
    Private Sub CheckVersion()
        Dim DM As New DataManager
        Dim CurVer As String
        Dim sExeMajorVersion As String
        Dim sExeMinorVersion As String
        Dim version As Version = GetExecutingAssembly.GetName.Version
        sExeMajorVersion = version.Major.ToString().Trim()
        sExeMinorVersion = version.Minor.ToString().Trim()
        CurVer = Trim(DM.ValReturnProjExecNonQry("Select UASACVersion from tblCompany"))

        If Len(Trim(CurVer)) = 0 Then
            MsgBox("Version Configuration is not yet set !!!", MsgBoxStyle.Critical)
            End
        End If
        If CurVer <> sExeMajorVersion & "." & sExeMinorVersion Then
            If Val(Mid(CurVer, 1, InStr(CurVer, ".") - 1)) > Val(sExeMajorVersion) Or Val(Mid(CurVer, InStr(CurVer, ".") + 1)) > Val(sExeMinorVersion) Then
                MsgBox("Running Lower Version Application " & vbCrLf & "Please Update your Application ", MsgBoxStyle.Critical)
                End
            End If
            MsgBox("Application Version Conflict", MsgBoxStyle.Critical)
            End
        End If
    End Sub

    Private Sub subGetLocationType(ByVal sLocationCode As String)
        Dim dsLocation As New DataSet
        Try
            dsLocation = objLocation.GetLocationwithCode(sLocationCode)
            If dsLocation.Tables(0).Rows.Count > 0 Then
                gthisLocationType = Trim(dsLocation.Tables(0).Rows(0)("LocationType") & "")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsLocation = Nothing
        End Try
    End Sub

End Class
