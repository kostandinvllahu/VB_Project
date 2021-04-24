Imports System.Data.SqlClient

Public Class Room

    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim i As Integer

    Private Sub Room_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
        con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\HotelVB.mdf;Integrated Security=True;Connect Timeout=30"
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
        Disp_data()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label5.Text = Date.Now.ToString("dd-MMM-yy hh:mm:ss")
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim menu As Menu = New Menu()
        menu.Show()
        Me.Close()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        TextBox4.Text = ComboBox1.SelectedItem
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked Then
            TextBox5.Text = "Available"
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked Then
            TextBox5.Text = "Booked"
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Clean()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then  'KETU RREGULLO NJE ERROR CATCH'
            con.Open()
            MessageBox.Show("Enter ID first!")
            Clean()
            con.Close()
        End If
        If TextBox1.Text = "" Then
            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "insert into Room values('" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "')"
            cmd.ExecuteNonQuery()
            MessageBox.Show("Records are saved successfully!")
            Clean()
            Disp_data()
        End If
    End Sub

    Public Sub Clean()

        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox7.Text = ""
        ComboBox1.Text = "Room Type"
        RadioButton1.Checked = False
        RadioButton2.Checked = False

    End Sub


    Public Sub Disp_data()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from Room"
        cmd.ExecuteNonQuery()
        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        DataGridView1.DataSource = dt
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()

        i = Convert.ToInt32(DataGridView1.SelectedCells.Item(0).Value.ToString())

        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from room where id=" & i & ""
        cmd.ExecuteNonQuery()
        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        Dim dr As SqlClient.SqlDataReader
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While dr.Read
            TextBox1.Text = dr.GetInt32(0).ToString()
            TextBox2.Text = dr.GetString(1).ToString()
            TextBox3.Text = dr.GetString(2).ToString()
            TextBox4.Text = dr.GetString(3).ToString()
            TextBox5.Text = dr.GetString(4).ToString()
        End While

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "update room set roomnum='" + TextBox2.Text + "',roomcel='" + TextBox3.Text + "',roomtype='" + TextBox4.Text + "',roomstate='" + TextBox5.Text + "'where id=" & i & ""
        cmd.ExecuteNonQuery()
        MessageBox.Show("Records are updated successfully!")
        Clean()
        Disp_data()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "Delete from room where id='" + TextBox1.Text + "'"
        cmd.ExecuteNonQuery()
        MessageBox.Show("Records are deleted successfully!")
        Clean()
        Disp_data()
    End Sub

    Private Sub Guna2ImageButton1_Click(sender As Object, e As EventArgs) Handles Guna2ImageButton1.Click
        Disp_data()
        TextBox7.Text = ""
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If con.State = ConnectionState.Open Then
            con.Close()
        End If

        If TextBox7.Text = "" Then
            MessageBox.Show("Please input a room number to search!")
            con.Close()
        End If
        con.Open()
        Dim query As String = "SELECT  * FROM room WHERE roomnum='" + TextBox7.Text + "'"
        Using con As SqlConnection = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\HotelVB.mdf;Integrated Security=True;Connect Timeout=30")
            Using cmd As SqlCommand = New SqlCommand(query, con)
                Using da As SqlDataAdapter = New SqlDataAdapter()
                    da.SelectCommand = cmd
                    Using dt As New DataTable()
                        da.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            DataGridView1.DataSource = dt
                        Else
                            MessageBox.Show("No records found")
                        End If
                    End Using
                End Using
            End Using
        End Using
    End Sub
End Class