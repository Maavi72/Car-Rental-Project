using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rental_System_Management
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string uname = txtuname.Text;
            string pass = txtpass.Text;

            if (uname.Equals("Admin") && pass.Equals("123"))
            {
                MessageBox.Show("Login Successfull");

                Main m = new Main();
                this.Hide();
                m.Show();
            }
            else
            {
                MessageBox.Show("Wrong data, Login Faild");
                txtuname.Clear();
                txtpass.Clear();
                txtuname.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
