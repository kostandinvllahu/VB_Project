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
        Disp_data()
        txtParking.Visible = False
        btnCheckParking.Visible = False
    End Sub

    Public Sub Disp_data()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from Book_tbl"
        cmd.ExecuteNonQuery()
        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        DataGridView1.DataSource = dt
    End Sub

    Public Sub Clear()
        txtID.Text = ""
        txtClient.Text = ""
        txtRoom.Text = ""
        txtTotalPrice.Text = ""
        txtDate.Text = ""
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        txtParking.Visible = False
        btnCheckParking.Visible = False
        TextBox4.Text = ""
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
        txtID.Text = ComboBox1.SelectedIndex.ToString
        txtClient.Text = ComboBox1.Text
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
        txtRoom.Text = ComboBox2.Text
    End Sub

    Public Sub calendar()
        Dim roomPrice As Integer = 30
        Dim DateEntry As DateTime = Convert.ToDateTime(dateTimePicker1.Text)
        Dim DateExit As DateTime = Convert.ToDateTime(DateTimePicker2.Text)
        Dim CountDays As TimeSpan = DateExit.Subtract(DateEntry)
        Dim TotalDays = Convert.ToInt32(CountDays.Days)
        If Convert.ToInt32(CountDays.Days) >= 0 Then
            txtDate.Text = TotalDays
            txtTotalPrice.Text = TotalDays * roomPrice
        Else
            MsgBox("Please enter a valid date")
        End If
    End Sub

    Private Sub dateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles dateTimePicker1.ValueChanged
        ' Dim checkIn As String = Convert.ToString(dateTimePicker1.Text)
        TextBox2.Text = Convert.ToString(dateTimePicker1.Text)

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label1.Text = Date.Now.ToString("dd-MMM-yy hh:mm:ss")
    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        ' Dim checkOut As String = Convert.ToString(DateTimePicker2.Text)
        TextBox1.Text = Convert.ToString(DateTimePicker2.Text)
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles btnCheckPrice.Click
        calendar()
    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles txtDate.TextChanged

    End Sub
    Public Sub parking()
        If (RadioButton1.Checked = True) Then
            txtParking.Visible = True
            btnCheckParking.Visible = True
            Dim time As Integer = 3
            Dim charge As Integer = Convert.ToInt32(txtDate.Text)
            If (charge = 1) Then
                txtParking.Text = "3.00"

            Else
                Dim TotalParking As Integer = charge * time
                txtParking.Text = TotalParking.ToString
                Dim P1 As Integer = txtParking.Text
                Dim P2 As Integer = txtTotalPrice.Text
                Dim TotalPrice As Integer = P1 + P2
                txtTotalPrice.Text = Convert.ToString(TotalPrice)

            End If

        End If
    End Sub
    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        txtParking.Visible = True
        btnCheckParking.Visible = True
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles btnCheckParking.Click
        parking()
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If (RadioButton2.Checked = True) Then
            txtParking.Visible = False
            btnCheckParking.Visible = False
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        TextBox4.Text = "Pending"
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        TextBox4.Text = "Paid"
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        TextBox4.Text = "Not Paid"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If txtID.Text = "" Then  'KETU RREGULLO NJE ERROR CATCH'
            con.Open()
            MessageBox.Show("Enter ID first!")
            Clear()
            con.Close()
        End If
        If txtID.Text >= "" Then
            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "insert into Book_tbl values('" + txtID.Text + "','" + txtClient.Text + "','" + txtRoom.Text + "','" + TextBox2.Text + "','" + TextBox1.Text + "','" + txtDate.Text + "','" + txtParking.Text + "','" + txtTotalPrice.Text + "','" + TextBox4.Text + "')"
            cmd.ExecuteNonQuery()
            MessageBox.Show("Records are saved successfully!")
            Clear()
            Disp_data()
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "update Book_tbl set room='" + txtRoom.Text + "',checkin='" + TextBox2.Text + "',checkout='" + TextBox1.Text + "',stayindDays='" + txtDate.Text + "',parkingPrice='" + txtParking.Text + "', TotalPrice='" + txtTotalPrice.Text + "',PaymentStatus='" + TextBox4.Text + "'where id=" + txtID.Text + ""
        cmd.ExecuteNonQuery()
        MessageBox.Show("Records are updated successfully!")
        Clear()
        Disp_data()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()

        i = Convert.ToInt32(DataGridView1.SelectedCells.Item(0).Value.ToString())

        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from Book_tbl where id=" & i & ""
        cmd.ExecuteNonQuery()
        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Dim dr As SqlClient.SqlDataReader
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While dr.Read
            txtID.Text = dr.GetInt32(0).ToString() 'PO 1
            txtClient.Text = dr.GetString(1).ToString() 'PO 2
            txtRoom.Text = dr.GetString(2).ToString() 'PO 3
            TextBox2.Text = dr.GetString(3).ToString() 'CHECKIN 4
            TextBox1.Text = dr.GetString(4).ToString() 'CHECKOUT 5
            txtDate.Text = dr.GetString(5).ToString() 'PARKING DAYS 6
            txtParking.Text = dr.GetString(6).ToString() 'PARKING PRICE 7
            txtTotalPrice.Text = dr.GetString(7).ToString() 'TOTAL PRICE 8
            TextBox4.Text = dr.GetString(8).ToString() 'STATUS 9
        End While
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        If TextBox4.Text = "Pending" Then
            MsgBox("The room must be paid then can be deleted")
            Clear()
        ElseIf TextBox4.Text = "Not Paid" Then
            MsgBox("Contact your manager now!")
        ElseIf TextBox4.Text = "Paid" Then
            con.Open()
            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "Delete from Book_tbl where id='" + txtID.Text + "'"
            cmd.ExecuteNonQuery()
            MessageBox.Show("Booking are deleted successfully!")
            Clear()
            Disp_data()
        End If
    End Sub
End Class