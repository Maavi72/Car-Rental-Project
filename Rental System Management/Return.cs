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
using System.Reflection;
using System.Text.RegularExpressions;

namespace Rental_System_Management
{
    public partial class Return : Form
    {
        public Return()
        {
            InitializeComponent();
            Returnload();
        }

        SqlConnection con = new SqlConnection("Data Source=MAAVI-RAJPOOT\\SQLEXPRESS;Initial Catalog=\"Car Rental\";Integrated Security=True;");

        SqlCommand cmd;
        SqlCommand cmd1;
        SqlDataReader dr;
        string proid;
        string sql;
        string sql1;





        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                cmd = new SqlCommand("select carid,custid,date,duedate,DATEDIFF(dd,duedate,GETDATE()) as elap from Rental where carid = ' " + txtcar_id.Text + " ' ", con);
                con.Open();
                 dr= cmd.ExecuteReader();

                if(dr.Read())
                {
                    txtcust_id.Text = dr["custid"].ToString();
                    txtdate.Text = dr["duedate"].ToString();
                }

                string elap = dr["elap"].ToString();

                int elapped = int.Parse(elap);
                txtelp.Text = (elap);

                if (elapped>0)
                {
                    
                    int fine = elapped * 100;
                    txtfine.Text = fine.ToString(); 
                }
                else
                {
                    txtfine.Text = "0";
                }
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string car_id = txtcar_id.Text;
            string cust_id = txtcust_id.Text;
            string elp = txtelp.Text;
            string date = txtdate.Text;
            string fine = txtfine.Text;


            sql1 = "insert into  [Return](car_id,cust_id,date,elp,fine)values(@car_id,@cust_id,@date,@elp,@fine)";
            con.Open();
            cmd1= new SqlCommand(sql1,con);
           cmd1.Parameters.AddWithValue("@car_id", car_id);
           cmd1.Parameters.AddWithValue("@cust_id", cust_id);
           cmd1.Parameters.AddWithValue("@date", date);
           cmd1.Parameters.AddWithValue("@elp", elp);
           cmd1.Parameters.AddWithValue("@fine" , fine);

            cmd1.ExecuteNonQuery();

            
            MessageBox.Show("Record Added");

            con.Close();

            txtcar_id.Clear();
            txtcust_id.Clear();
            txtdate.Clear();
            txtelp.Clear();     
            txtfine.Clear();
            txtcar_id.Focus();
        }

        private void Return_Load(object sender, EventArgs e)
        {
            txtcar_id.Focus();
        }
        private void Returnload()
        {
            sql = "select * from[Return]";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();

            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]);    
            }

            con.Close();
        }
    }
}
