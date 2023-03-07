Public Class Form1

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        'Validasi username kosong
        If String.IsNullOrEmpty(txtUsername.Text) Then
            MessageBox.Show("Username tidak boleh kosong", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtUsername.Focus()
            'Validasi password kosong
        ElseIf String.IsNullOrEmpty(txtPassword.Text) Then
            MessageBox.Show("Password tidak boleh kosong", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtPassword.Focus()
        Else

            'Jika username dan password terisi maka buat koneksi
            Dim conn As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\users.accdb")

            'Membuka koneksi ke database
            conn.Open()

            'Membuat command untuk memeriksa data login pengguna di database
            Dim cmd As New OleDb.OleDbCommand("SELECT * FROM users WHERE username = @username AND password = @password", conn)

            'Mengisi parameter pada command
            cmd.Parameters.AddWithValue("@username", txtUsername.Text)
            cmd.Parameters.AddWithValue("@password", txtPassword.Text)

            'Menjalankan command dan menyimpan hasilnya dalam sebuah data reader
            Dim dr As OleDb.OleDbDataReader = cmd.ExecuteReader()
            If dr.HasRows Then
                'Membaca data pengguna
                dr.Read()

                'Memeriksa apakah pengguna memiliki hak akses yang sesuai
                If dr("userlvl") = "admin" Then
                    'Jika level pengguna adalah admin
                    MessageBox.Show("Level user anda adalah Admin", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ElseIf dr("userlvl") = "users" Then
                    'Jika level pengguna adalah pengguna User
                    MessageBox.Show("Level user anda adalah User", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                'Jika data pengguna tidak ditemukan, tampilkan pesan kesalahan
                MessageBox.Show("Username atau Password salah!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtUsername.Text = ""
                txtPassword.Text = ""
                txtUsername.Focus()

                'Menutup koneksi ke database
                dr.Close()
                cmd.Dispose()
                conn.Close()

            End If
        End If
    End Sub
End Class