Imports System.Runtime.InteropServices
Imports 進銷存.Database


Public Class winPeople

    Public Enum Kind
        Supplier = 0
        Customer = 1
        Personnel
    End Enum

    Public Mode As Kind

    <StructLayout(LayoutKind.Explicit)> _
    Structure Data
        <FieldOffset(0)> Dim Supplier As Supplier
        <FieldOffset(0)> Dim Customer As Customer
        <FieldOffset(0)> Dim Personnel As Personnel
    End Structure

    Dim myData As Data


    Public Sub AddSupplier(ByVal Data As Supplier)
        Mode = Kind.Supplier
        UpdateText(Data)
        MyBase.ShowDialog()
    End Sub

    Public Sub AddCustomer(ByVal Data As Customer)
        Mode = Kind.Customer
        UpdateText(Data)
        MyBase.ShowDialog()
    End Sub

    Public Sub AddPersonnel(ByVal Data As Personnel)
        Mode = Kind.Personnel
        UpdateText(Data)
        MyBase.ShowDialog()
    End Sub

    Public Sub UpdateText(ByVal obj As Object)
        Select Case Mode
            Case Kind.Supplier : Me.Text = "供應商"
            Case Kind.Personnel : Me.Text = "員工"
            Case Kind.Customer : Me.Text = "客戶"
        End Select
        myData.Supplier = obj
        txtLabel.Text = myData.Supplier.Label
        txtName.Text = myData.Supplier.Name
        txtAddr.Text = myData.Supplier.Addr
        txtTel1.Text = myData.Supplier.Tel1
        txtTel2.Text = myData.Supplier.Tel2
        txtNote.Text = myData.Supplier.Note
    End Sub

    Public Function GetData() As Data
        Dim data As Data = Nothing
        data.Supplier.Label = txtLabel.Text
        data.Supplier.Name = txtName.Text
        data.Supplier.Addr = txtAddr.Text
        data.Supplier.Tel1 = txtTel1.Text
        data.Supplier.Tel2 = txtTel2.Text
        data.Supplier.Note = txtNote.Text
        Return data
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAdd.Click
        myData = GetData()

        Select Case Mode
            Case Kind.Customer
                DB.AddCustomer(myData.Customer)
                UpdateText(GetNewCustomer)
            Case Kind.Personnel
                DB.AddPersonnel(myData.Personnel)
                UpdateText(GetNewPersonnel)
            Case Kind.Supplier
                DB.AddSupplier(myData.Supplier)
                UpdateText(GetNewSupplier)
        End Select

    End Sub

End Class
