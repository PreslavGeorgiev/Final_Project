using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            string emptyTextBox = string.Empty;

            string salt = string.Empty;

            for (int i = 0; i < textBoxes.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(textBoxes[i].Text))
                {
                    emptyTextBox = textBoxes[i].Name.Substring(3);
                    labels[i].Text = "This textbox is empty!";
                }
                else
                {
                    if(txtPassword.Equals(txtConfirm))
                    {
                        salt = DateTime.Now.ToString();
                        txtConfirm.Text = salt;
                        string password = txtPassword.Text;
                        hashPassword($"{password}{salt}");
                        LoginForm loginForm = new LoginForm();
                        this.Hide();
                        loginForm.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        labels[4].Text = "Passwords don't match!";
                    }
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
