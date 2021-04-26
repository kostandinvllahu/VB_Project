Imports System.Data.SqlClient
Public Class Clients

    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim i As Integer

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim menu As Menu = New Menu()
        menu.Show()
        Me.Close()
    End Sub

    Private Sub Clients_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
        con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\HotelVB.mdf;Integrated Security=True;Connect Timeout=30"
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
        Disp_data()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label2.Text = Date.Now.ToString("dd-MMM-yy hh:mm:ss")
    End Sub

    Public Sub Clean()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        'TextBox6.Text = ""
        TextBox7.Text = ""
        ComboBox1.Text = "Country"
    End Sub

    Public Sub Disp_data()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from clients"
        cmd.ExecuteNonQuery()
        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        DataGridView1.DataSource = dt
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Clean()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        TextBox7.Text = ComboBox1.SelectedItem
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then  'KETU RREGULLO NJE ERROR CATCH'
            con.Open()
            MessageBox.Show("Enter ID first!")
            Clean()
            con.Close()
        End If
        If TextBox1.Text >= "" Then
            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "insert into clients values('" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "','" + TextBox7.Text + "')"
            cmd.ExecuteNonQuery()
            MessageBox.Show("Records are saved successfully!")
            Clean()
            Disp_data()
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()

        i = Convert.ToInt32(DataGridView1.SelectedCells.Item(0).Value.ToString())

        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from clients where id=" & i & ""
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
            TextBox7.Text = dr.GetString(5).ToString()
        End While

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "update clients set name='" + TextBox2.Text + "',surname='" + TextBox3.Text + "',phone='" + TextBox4.Text + "',idcard='" + TextBox5.Text + "',country='" + TextBox7.Text + "'where id=" & i & ""
        cmd.ExecuteNonQuery()
        MessageBox.Show("Records are updated successfully!")
        Clean()
        Disp_data()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "Delete from clients where id='" + TextBox1.Text + "'"
        cmd.ExecuteNonQuery()
        MessageBox.Show("Records are deleted successfully!")
        Clean()
        Disp_data()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub Guna2ImageButton1_Click(sender As Object, e As EventArgs) Handles Guna2ImageButton1.Click
        Disp_data()
        TextBox6.Text = ""
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If con.State = ConnectionState.Open Then
            con.Close()
        End If

        con.Open()
        Dim query As String = "SELECT  * FROM staff WHERE name='" + TextBox6.Text + "'"
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