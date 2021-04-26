Public Class Menu
    Private Sub Guna2ImageButton1_Click(sender As Object, e As EventArgs) Handles Guna2ImageButton1.Click
        Dim room As Room = New Room()
        room.Show()
        Me.Hide()
    End Sub

    Private Sub Guna2ImageButton2_Click(sender As Object, e As EventArgs) Handles Guna2ImageButton2.Click
        Dim staff As Staff = New Staff()
        staff.Show()
        Me.Hide()
    End Sub

    Private Sub Guna2ImageButton3_Click(sender As Object, e As EventArgs) Handles Guna2ImageButton3.Click
        Dim book As Book = New Book()
        book.Show()
        Me.Hide()
    End Sub

    Private Sub Guna2ImageButton4_Click(sender As Object, e As EventArgs) Handles Guna2ImageButton4.Click
        Dim history As History = New History()
        history.Show()
        Me.Hide()
    End Sub

    Private Sub Guna2ImageButton5_Click(sender As Object, e As EventArgs) Handles Guna2ImageButton5.Click
        Dim ex As Form1 = New Form1()
        ex.Show()
        Me.Close()
    End Sub

    Private Sub Menu_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Guna2ImageButton6_Click(sender As Object, e As EventArgs) Handles Guna2ImageButton6.Click
        Dim cl As Clients = New Clients()
        cl.Show()
        Me.Close()
    End Sub
End Class