using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Final_Project
{
    public partial class Form1 : Form
    {
        private int productID;
        private byte[] mainPhoto;
        private decimal price;
        public Form1(int productID)
        {
            this.productID = productID;
            InitializeComponent();
            LoadProductData();
        }

        private void LoadProductData()
        {
            string connectionString = "your_connection_string_here";
            string query = "SELECT mainPhoto, productName, price, productDescription FROM product_inventory WHERE product_id = @productID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@productID", this.productID);


                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    this.mainPhoto = (byte[])reader["mainPhoto"];

                    label1.Text = (string)reader["productName"];
                    this.price = (decimal)reader["price"];
                    label2.Text = this.price.ToString();
                    richTextBox1.Text = (string)reader["productDescription"];

                    using (MemoryStream ms = new MemoryStream(this.mainPhoto))
                    {
                        System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox1.Image = image;
                    }
                }

                reader.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "your_connection_string_here";
            string query = "INSERT INTO OrderItems (ProductID, Price) VALUES (@ProductID, @Price)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductID", this.productID);
                command.Parameters.AddWithValue("@Price", this.price);


                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
