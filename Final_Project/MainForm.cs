using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Project
{
    public partial class MainForm : Form
    {
        string con = "Data Source=MARTINADEF\\SQLEXPRESS;Initial Catalog=db2;Integrated Security=True;Encrypt=False";
        int page = 1;
        string lastQuery;
        string userName;
        Dictionary<int, int> dict1 = new Dictionary<int, int>();


        public MainForm(string username)
        {
            InitializeComponent();
            string query = "SELECT price, productName, mainPhoto, product_id FROM product_inventory";
            lastQuery = query;
            setProducts(query);
            userName = username;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string query = String.Format("SELECT price, productName, mainPhoto, product_id FROM product_inventory WHERE productName LIKE '{0}%'", SearchBox.Text);
            lastQuery = query;
            setProducts(query);
        }

        private void setProducts(string query)
        {
            updatePage(query);
            clearSlots();
            var slots = getProductSlots();

            using (SqlConnection connection = new SqlConnection(con))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                int toFill = 0;
                int skips;
                Image image;
                MemoryStream ms;
                skips = (page - 1) * slots.Count;

                while (reader.Read()) {
                    if (toFill >= slots.Count)
                    {
                        break;
                    }

                    if (toFill < skips)
                    {
                        skips--;
                    }
                    else
                    {
                        string tb = reader[1].ToString() + "\n\nPrice: " + reader[0].ToString();
                        slots[toFill].Item1.Text = tb;
                        int tmp = -1;
                        byte[] imageBytes = (byte[])reader[2];
                        using (ms = new MemoryStream(imageBytes))
                        {
                            image = Image.FromStream(ms);
                            slots[toFill].Item2.SizeMode = PictureBoxSizeMode.StretchImage;
                            slots[toFill].Item2.Image = image;
                            tmp = (int)reader[3];
                        }

                        dict1.Add(slots[toFill].Item3, tmp);

                        toFill++;
                    }
                }
                reader.Close();
            }
        }
        private void updatePage(string query)
        {
            PageCounter.Text = "Page " + page + " of " + maxPage(query);
        }

        private int maxPage(string query)
        {
            int items = 0;

            using (SqlConnection connection = new SqlConnection(con))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    items++;
                }

                reader.Close();
            }
            return (int)Math.Ceiling((double)items / 8);
        }

        private void clearSlots()
        {
            var slots = getProductSlots();
            foreach (var slot in slots)
            {
                slot.Item1.Text = "";
                slot.Item2.Image = null;
            }
        }

        private List<Tuple<Label, PictureBox, int>> getProductSlots()
        {
            var dict = new List<Tuple<Label, PictureBox, int>>();
            //var t = new Label();
            //var p = new PictureBox();

            //foreach (Control g in Group.Controls)
            //{

            //    if (g.GetType() == typeof(GroupBox))
            //    {
            //        foreach (Control c in g.Controls)
            //        {
            //            if (c.GetType() == typeof(Label))
            //            {
            //                t = (Label)c;
            //            }
            //            else if (c.GetType() == typeof(PictureBox))
            //            {
            //                p = (PictureBox)c;
            //            }
            //        }
            //        dict.Add(new Tuple<Label, PictureBox>(t, p));
            //    }
            //}

            dict.Add(new Tuple<Label, PictureBox, int>(label10, pictureBox4, 0));
            dict.Add(new Tuple<Label, PictureBox, int>(label4, pictureBox3, 1));
            dict.Add(new Tuple<Label, PictureBox, int>(label2, pictureBox1, 2));
            dict.Add(new Tuple<Label, PictureBox, int>(label6, pictureBox2, 3));
            dict.Add(new Tuple<Label, PictureBox, int>(label8, pictureBox5, 4));
            dict.Add(new Tuple<Label, PictureBox, int>(label14, pictureBox7, 5));
            dict.Add(new Tuple<Label, PictureBox, int>(label16, pictureBox8, 6));
            dict.Add(new Tuple<Label, PictureBox, int>(label12, pictureBox6, 7));


            return dict;
        }

        private void AccountButton_Click(object sender, EventArgs e)
        {
            //Application.Run(new AccountForm());
        }

        private void ShoppingCartButton_Click(object sender, EventArgs e)
        {
            //Application.Run(new OrderMenu_Form4(userName));
        }

        private void Group_Enter(object sender, EventArgs e)
        {

        }

        private void Previous_Click(object sender, EventArgs e)
        {
            page--;
            if (page < 1)
            {
                page = 1;
            }
            setProducts(lastQuery);
        }

        private void Next_Click(object sender, EventArgs e)
        {
            page++;
            if (page > maxPage(lastQuery))
            {
                page = maxPage(lastQuery);
            }
            setProducts(lastQuery);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Run(new Form1(dict1[0]));
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Run(new Form1(dict1[1]));

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Run(new Form1(dict1[2]));

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Run(new Form1(dict1[3]));

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Run(new Form1(dict1[4]));

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Application.Run(new Form1(dict1[5]));

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Application.Run(new Form1(dict1[6]));

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Application.Run(new Form1(dict1[7]));

        }
    }
}
