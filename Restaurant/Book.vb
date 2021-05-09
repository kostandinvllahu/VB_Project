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

        Dim cmd As New SqlCommand("select id, name from clients", con)
        Dim da As New SqlDataAdapter
        da.SelectCommand = cmd
        Dim table As New DataTable
        da.Fill(table)
        Dim row As DataRow = table.NewRow
        row(0) = 0
        row(1) = "Select Clients"
        table.Rows.InsertAt(row, 0)
        ComboBox1.DataSource = table
        ComboBox1.DisplayMember = "name"
        ComboBox1.ValueMember = "id"

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        TextBox1.Text = ComboBox1.SelectedIndex.ToString

        Dim Query As String = "select * name from clients where id='" & TextBox1.Text & "'"
        Dim Command As New Command(Query, con)
        READER = Command.ExecuteReader
        While READER.Read
            TextBox1.Text = READER.GetString("name")

    End Sub
End Class