using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Final_Project
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void signUpButton_Click(object sender, EventArgs e)
        {
            TextBox[] textBoxes = { txtFirstName, txtLastName, txtUsername, txtPassword, txtConfirm, txtAddress, txtPhone };
            Label[] labels = { errorFirstName, errorLastName, errorUsername, errorPassword, errorConfirm, errorAddress, errorPhone};

            string salt = string.Empty;

            for (int i = 0; i < textBoxes.Length; i++)
            {

                if (string.IsNullOrWhiteSpace(textBoxes[i].Text))
                {
                    labels[i].Text = "This textbox is empty!";
                }
                else
                {
                    if (!txtAddress.Text.Contains("@"))
                        errorAddress.Text = "Invalid email address!";
                    else
                        errorAddress.Text = string.Empty;

                    string special = "^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[!@#$%_&?*])[A-Za-z0-9!@#$%_&?*]{8,}$";
                    if (!Regex.IsMatch(txtPassword.Text, special))
                    {
                        errorPassword.Text = "Password needs more special characters";
                        return;
                    }
                    else
                        errorPassword.Text = string.Empty;

                    if (txtPassword.Text.Equals(txtConfirm.Text))
                    {
                        salt = DateTime.Now.ToString();
                        txtConfirm.Text = salt;
                        string password = txtPassword.Text;
                        string hash = hashPassword($"{password}{salt}");

                        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = Final_Project;Integrated Security = True;");
                        con.Open();
                        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE username=@username", con);
                        cmd.Parameters.Add("@username", SqlDbType.NVarChar).Value = txtUsername.Text;

                        if (Convert.ToInt32(cmd.ExecuteScalar()) != 0)
                        {
                            MessageBox.Show("User already exists");
                            return;
                        }

                        cmd = new SqlCommand("INSERT INTO Users(FirstName, LastName, Username, Password, Address, Phone, Date) VALUES(@FirstName, @LastName, @Username, @hash, @Address, @Phone, @Date)", con);
                        cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = txtFirstName.Text;
                        cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = txtLastName.Text;
                        cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = txtUsername.Text;
                        cmd.Parameters.Add("@hash", SqlDbType.NVarChar).Value = hash;
                        cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = txtAddress.Text;
                        cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = txtPhone.Text;
                        cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = salt;
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("User was successfully registered!");

                        LoginForm loginForm = new LoginForm();
                        this.Hide();
                        loginForm.ShowDialog();
                        this.Close();
                    }
                    else if(!(txtPassword.Text.Equals(txtConfirm.Text)))
                        labels[4].Text = "Passwords don't match!";
                    else
                        labels[4].Text = string.Empty;
                }
            }
        }
        string hashPassword(string text)
        {
            SHA256 hashAlgorithm = SHA256.Create();
            var bytes = Encoding.Default.GetBytes(text);
            var hash = hashAlgorithm.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }



        private void clickHereButton_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            this.Hide();
            loginForm.ShowDialog();
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
