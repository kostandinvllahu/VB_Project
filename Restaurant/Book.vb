Imports System.Data.SqlClient
Public Class Book
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim i As Integer
    Private Sub Book_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
        con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\HotelVB.mdf;Integrated Security=True;Connect Timeout=30"
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
        fillClientcombo()
        fillRoomcombo()
    End Sub

    Public Sub Clear()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        RadioButton1.Checked = False
        RadioButton2.Checked = False

    End Sub

    Public Sub fillRoomcombo()
        Dim RoomBook As String = "Available"
        Dim cmd As New SqlCommand("select id, roomtype from Room where roomstate='" + RoomBook + "'", con)
        Dim da As New SqlDataAdapter
        da.SelectCommand = cmd
        Dim table As New DataTable
        da.Fill(table)
        Dim row As DataRow = table.NewRow
        row(0) = 0
        row(1) = "Select Room"
        table.Rows.InsertAt(row, 0)
        ComboBox2.DataSource = table
        ComboBox2.DisplayMember = "roomtype"
        ComboBox2.ValueMember = "id"
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
        TextBox2.Text = ComboBox1.Text
        'NUK E DI CA KA DASHUR TE THOT AUTORI
        '  Dim Query As String = "select  name from clients where id='" & TextBox1.Text & "'"
        '   Dim Command As New Command(Query, con)
        '   READER = Command.ExecuteReader
        '   While READER.Read
        '  TextBox1.Text = READER.GetString("name")

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim menu As Menu = New Menu()
        menu.Show()
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Clear()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        TextBox3.Text = ComboBox2.Text
    End Sub

    Public Sub calendar()
        Dim DateEntry As DateTime = Convert.ToDateTime(dateTimePicker1.Text)
        Dim DateExit As DateTime = Convert.ToDateTime(DateTimePicker2.Text)
        Dim CountDays As TimeSpan = DateExit.Subtract(DateEntry)
        Dim TotalDays = Convert.ToInt32(CountDays.Days)
        If Convert.ToInt32(CountDays.Days) >= 0 Then
            TextBox8.Text = TotalDays
        Else
            MsgBox("Please enter a valid date")

        End If
    End Sub

    Private Sub dateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles dateTimePicker1.ValueChanged
        TextBox4.Text = dateTimePicker1.Text

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label1.Text = Date.Now.ToString("dd-MMM-yy hh:mm:ss")
    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        TextBox5.Text = DateTimePicker2.Text
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        calendar()
    End Sub
End Class