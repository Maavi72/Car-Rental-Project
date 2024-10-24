using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Rental_System_Management
{
    public partial class Rental : Form
    {
        public Rental()
        {
            InitializeComponent();
            carload();
            rentalload();
        }
        SqlConnection con = new SqlConnection("Data Source=MAAVI-RAJPOOT\\SQLEXPRESS;Initial Catalog=\"Car Rental\";Integrated Security=True;");



        SqlCommand cmd;
        SqlCommand cmd1;
        SqlDataReader dr;
        string proid;
        string sql;
        string sql1;
        bool Mode = true;
        string id;




        public void carload()
        {
            cmd = new SqlCommand("select * from CarReg", con);
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtcarid.Items.Add(dr["regno"].ToString());
            }

            con.Close();
        }






        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string carid = txtcarid.SelectedItem.ToString();
            string custid = txtcustid.Text;
            string custname = txtcustname.Text;
            string fee = txtfee.Text;
            string date = txtdate.Value.Date.ToString("yyyy-MM-dd");
            string due = txtdue.Value.Date.ToString("yyyy-MM-dd");


            sql = "insert into Rental (carid,custid,custname,fee,date,duedate) values (@carid,@custid,@custname,@fee,@date,@duedate)";
            con.Open();
            cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@carid", carid);
            cmd.Parameters.AddWithValue("@custid", custid);
            cmd.Parameters.AddWithValue("@custname" , custname);
            cmd.Parameters.AddWithValue("@fee" , fee);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@duedate", due);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Record Added");
            con.Close();

            sql1 = "update CarReg set available = 'No' where regno = @regno";
            con.Open();
            cmd1= new SqlCommand(sql1, con);
            cmd1.Parameters.AddWithValue("@regno" , carid);
            cmd1.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Record updated");
            
        }

        private void txtcarid_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select * from CarReg where regno = '" + txtcarid.Text + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

          if (dr.Read())
            {

                string aval;
                aval = dr["available"].ToString();
                label9.Text = aval;

                if( aval == "No")
                {
                    txtcustid.Enabled = false;
                    txtcustname.Enabled = false;
                    txtfee.Enabled = false;
                    txtdate.Enabled = false;
                    txtdue.Enabled = false;
                }
                else
                {
                    txtcustid.Enabled = true;
                    txtcustname.Enabled = true;
                    txtfee.Enabled = true;
                    txtdate.Enabled = true ;
                    txtdue.Enabled= true ;  
                }
                    







            }

            else
            {
                label9.Text = "Car is not Available";
            }

            con.Close();

        }

        private void rentalload()
        {
            sql = "select * from Rental";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();

            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6]);
            }

            con.Close();
        }

        private void txtcustid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                cmd = new SqlCommand("select * from customer where custid = '" + txtcustid.Text + "'", con);
                con.Open();
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtcustname.Text = dr["custname"].ToString();

                }
                else
                {
                    MessageBox.Show("Customer ID Not Found");
                }
                con.Close();

            }
                
        }
    }
}
