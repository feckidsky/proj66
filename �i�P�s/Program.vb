Imports 進銷存.Database.StructureBase

Public Module Program
    Public DB As New Database.Access
    Public Supplies As Supplier()
    Public Personnels As Personnel()
    Public Goodsies As Goods()
    Public Mobiles As Mobile()

   


    Public Sub InitialProgram()
        Supplies = DB.ReadSupplier
        Personnels = DB.ReadPersonnel
        Goodsies = DB.ReadGoods
        Mobiles = DB.ReadMobile
    End Sub

    Public Function NewSupplier() As Supplier
        Dim data As Supplier = Nothing
        data.Label = "SU" & Now.ToString("yyMMddHHmmss")
        Return data
    End Function

    Public Function NewPersonnel() As Personnel
        Dim data As Personnel = Nothing
        data.Label = "P" & Now.ToString("yyMMddHHmmss")
        Return data
    End Function

    Public Function NewCustomer() As Customer
        Dim data As Customer = Nothing
        data.Label = "C" & Now.ToString("yyMMddHHmmss")
        Return data
    End Function

    Public Function NewGoods() As Goods
        Dim data As Goods = Nothing
        data.Label = "G" & Now.ToString("yyMMddHHmmss")
        Return data
    End Function

    Public Function NewMobile() As Mobile
        Dim data As Mobile = Nothing
        data.Label = "M" & Now.ToString("yyMMddHHmmss")
        Return data
    End Function

    Public Function NewStock() As Stock
        Dim data As Stock = Nothing
        data.Label = "ST" & Now.ToString("yyMMddHHmmss")
        data.Date = Today
        Return data
    End Function

    Public Function NewSales() As Sales
        Dim data As Sales = Nothing
        data.Label = "SA" & Now.ToString("yyMMddHHmmss")
        data.Date = Today
        Return data
    End Function

    Public Function NewOrder() As Order
        Dim data As Order = Nothing
        data.Label = "O" & Now.ToString("yyMMddHHmmss")
        data.Date = Today
        Return data
    End Function
End Module
