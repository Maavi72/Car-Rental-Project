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

namespace Rental_System_Management
{
    public partial class CarReg : Form
    {
        public CarReg()
        {
            InitializeComponent();
            Autono();
            load();
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
            sql = "select regno from CarReg order by regno desc";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.Read() )
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

            txtregno.Text = proid.ToString();

            con.Close();
        }



        private void load()
        {
            sql = "select * from CarReg";
            cmd = new SqlCommand(sql,con);
            con.Open();
            dr =cmd.ExecuteReader();
            dataGridView1.Rows.Clear();

            while(dr.Read())
            {
                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3]);
            }

            con.Close();
        }



        public void getid(String id)
        {
            sql = " select * from CarReg where regno = '" + id + "'";
            cmd = new SqlCommand (sql,con);
            con.Open();
            dr = cmd.ExecuteReader();

            while( dr.Read() )
            {
                txtregno.Text = dr[0].ToString();
                txtmake.Text= dr[1].ToString();
                txtmodel.Text= dr[2].ToString();
                txtavl.Text= dr[3].ToString();
            }
            con.Close();
        }





        private void CarReg_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string regno = txtregno.Text;
            string make = txtmake.Text;
            string model = txtmodel.Text;
            string aval = txtavl.SelectedItem.ToString();

            id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            if (Mode == true)
            {
                sql = "insert into CarReg(regno,make,model,available)values(@regno,@make,@model,@available)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@regno", regno);
                cmd.Parameters.AddWithValue("@make", make);
                cmd.Parameters.AddWithValue("@model", model);
                cmd.Parameters.AddWithValue("@available", aval);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Added");

                txtmake.Clear();
                txtmodel.Clear();
                txtavl.Items.Clear();
                txtmake.Focus();
                
            }
            else
            {
                sql = "update into CarReg set make = @make, model = @model, available = @available where regno = @regno"; 
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@make", make);
                cmd.Parameters.AddWithValue("@model", model);
                cmd.Parameters.AddWithValue("@available", aval);
                cmd.Parameters.AddWithValue("@regno", id);


               
                MessageBox.Show("Record Updated");
                txtregno.Enabled = true;
                Mode = true;

                
                
            }

            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == dataGridView1.CurrentRow.Cells[0].Value.ToString())
            {
                Mode = true;
                txtregno.Enabled = true;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                getid(id);
            }
            else if (e.ColumnIndex == dataGridView1.Columns["delete"].Index && e.RowIndex > 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                sql = "delete from Carreg where regno =@id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted");
                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            load();
            Autono();


            txtmake.Clear();
            txtmodel.Clear();
            txtmake.Focus();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
