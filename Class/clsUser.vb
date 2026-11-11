Public Class clsUser

    '----  Global user info

    Public UserId As String
    Public Description As String
    Public Initials As String
    Public UserPassword As String
    Public CompanyCode As String
    Public GroupCode As String
    Public EmpId As String
    Public ItemFrom As String
    Public ItemTo As String

    '----  Global defaults

    Public CurrencyCode As String
    Public StoreCode As String
    Public PricingCode As String
    Public DiscountPricingCode As String
    Public ShipCode As String
    Public GstPercentage As String
    Public ServiceCharge As String
    Public GidNo As String
    Public FOCPricingCode As String

    Public Email As String
    Public TranType As String
    Public PoLimit As Double
    Public Designation As String
    Public Phone As String
    Public Fax As String
    Public Address1 As String
    Public Address2 As String
    Public BillMessage1 As String
    Public BillMessage2 As String
    Public GSTREG As String
    Public CompanyName As String


    Public sLocationAddress1 As String
    Public sLocationAddress2 As String
    Public sLocationAddress3 As String
    Public sLocationAddress4 As String
    Public sLocationAddress5 As String
    Public sLocationAddress6 As String

    Public UpdateGst As Integer
    Public PONo As Long
    Public gSelect As String
    Public InventoryPrefix As Boolean
    Public InventoryRangeBasedOn As String
    Public Member As Boolean
    Public AllowMultipleDiscount As Boolean
    Public Rental As Boolean

    Public Barcode As String


    Public Function fncGetUserByIdAndPassword(ByVal sConn As String, ByVal sDsName As String, ByVal sUserId As String, ByVal sPassword As String) As DataSet
        Dim dsUser As DataSet
        Dim sSql As String
        sSql = "Select * From tblUserMaster Where UserCode = " & fncFormatSQLText(sUserId)
        sSql = sSql & " And Password = " & fncFormatSQLText(sPassword)
        dsUser = fncGetDatasetWithQuery(sConn, sDsName, sSql)
        fncGetUserByIdAndPassword = dsUser
        dsUser = Nothing
    End Function
End Class
