using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Project
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Final_Project; Integrated Security=True;");
            con.Open();
            string query = "SELECT Count(*) FROM Users WHERE username=@username";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", txtUsername2.Text);
            cmd.ExecuteNonQuery();

            int count = Convert.ToInt32(cmd.ExecuteScalar());

            if (count == 1)
            {
                MainForm mainForm = new MainForm();
                mainForm.Show();
                return;
            }
            else
            {
                MessageBox.Show("Indvalid username or password");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            this.Hide();
            registerForm.ShowDialog();
            this.Close();
        }
    }
}
