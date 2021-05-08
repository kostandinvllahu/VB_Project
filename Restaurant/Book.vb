Imports System.Data.SqlClient
Public Class Book
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim i As Integer
    Private Sub Book_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\HotelVB.mdf;Integrated Security=True;Connect Timeout=30"
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
        fillClientcombo()
    End Sub

    Public Sub fillClientcombo()
        Dim adapter As New SqlDataAdapter("select name from clients", con)
        Dim table As New DataTable()
        adapter.Fill(table)
        ComboBox1.DataSource = table
        ' ComboBox1.ValueMember = "Id"
        ' ComboBox1.DisplayMember = "name"
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        fillClientcombo()
    End Sub
End Class