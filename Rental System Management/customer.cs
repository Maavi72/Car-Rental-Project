using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace Rental_System_Management
{
    public partial class customer : Form
    {
        public customer()
        {
            InitializeComponent();
            Autono();
            customerload();
        }

        SqlConnection con = new SqlConnection("Data Source=MAAVI-RAJPOOT\\SQLEXPRESS;Initial Catalog=\"Car Rental\";Integrated Security=True;");



        SqlCommand cmd;
        SqlDataReader dr;
        string proid;
        string sql;
        bool Mode = true;
        string id;







        public void Autono()
        {
            sql = "select custid from customer order by custid desc"; 
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                int id = int.Parse(dr[0].ToString()) + 1;
                proid = id.ToString("00001");
            }
            else if (Convert.IsDBNull(dr))
            {
                proid = ("00001");
            }
            else
            {
                proid = ("00001");
            }

            txtid.Text = proid.ToString();

            con.Close();
        }



        private void customerload()
        {
            sql = "select * from customer";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();

            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3]);
            }

            con.Close();
        }




        private void button1_Click(object sender, EventArgs e)
        {
            string custid = txtid.Text;
            string custname = txtname.Text;
            string address = txtaddress.Text;
            string mobile = txtmobile.Text;





        //  id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            if (Mode == true)
            {
                sql = "insert into customer(custid,custname,address,mobile)values(@custid,@custname,@address,@mobile)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@custid", custid);
                cmd.Parameters.AddWithValue("@custname", custname);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@mobile", mobile);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Added");

                txtname.Clear();
                txtaddress.Clear();
                txtmobile.Clear();
                txtname.Focus();

            }
            else
            {
                sql = "update into CarReg set make = @make, model = @model, available = @available where regno = @regno";
                con.Open();
                cmd = new SqlCommand(sql, con);
             // cmd.Parameters.AddWithValue("@make", make);
            //  cmd.Parameters.AddWithValue("@model", model);
            //  cmd.Parameters.AddWithValue("@available", aval);
              cmd.Parameters.AddWithValue("@regno", id);



                MessageBox.Show("Record Updated");
            //txtregno.Enabled = true;
                Mode = true;

           //   txtmake.Clear();
            //  txtmodel.Clear();
            // txtavl.Items.Clear();
            //  txtmake.Focus();

            }

            con.Close();
        }

        private void customer_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            customerload();
        }
    }
}
