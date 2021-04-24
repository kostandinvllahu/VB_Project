Imports System.Data
Imports System.Data.SqlClient

Public Class Form1
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim con As SqlConnection = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\HotelVB.mdf;Integrated Security=True;Connect Timeout=30")
        Dim cmd As SqlCommand = New SqlCommand("select * from login where username='" + txtUsername.Text + "' and password='" + txtPassword.Text + "'", con)
        Dim sda As SqlDataAdapter = New SqlDataAdapter(cmd)
        Dim dt As DataTable = New DataTable()
        sda.Fill(dt)
        If (dt.Rows.Count > 0) Then
            MessageBox.Show("Login Success", "information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Dim this As Menu = New Menu()
            this.Show()
            Me.Hide()
        Else
            MessageBox.Show("Error", "information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        System.Windows.Forms.Application.Exit()
    End Sub
End Class
