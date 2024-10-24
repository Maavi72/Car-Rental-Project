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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CarReg c = new CarReg();
            c.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            customer c = new customer();
            c.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Rental r = new Rental();
            r.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Return re = new Return();
            re.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            this.Hide();
            frm.Show();

        }
    }
}
