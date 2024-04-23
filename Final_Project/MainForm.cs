using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Project
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void setProducts(int page)
        {
            foreach (Control c in Group.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    Console.WriteLine("ii");
                }
                else if (c.GetType() == typeof(PictureBox))
                {
                    Console.WriteLine("iii");
                }
            }
        }
    }
}
