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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = this.PopulateDataGridView();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = this.PopulateDataGridView();
        }
        private DataTable PopulateDataGridView()
        {
            string query = "SELECT nomer_zakaza, sostav_zakaza, data_zakaza, data_dostavki, kod_dlya_poluchenya, kod_statusa, kod_polzovatelya, kod_punkta_vydachi FROM UP07_Zakaz";
            query += " WHERE data_zakaza LIKE '%' + @data_zakaza + '%'";
            query += " OR @data_zakaza = ''";
            string constr = @"data source=stud-mssql.sttec.yar.ru,38325;initial catalog=user57_db;persist security info=True;user id=user57_db;password=user57;MultipleActiveResultSets=True;App=EntityFramework";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("data_zakaza", textBox1.Text.Trim());
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.Clear();
        }
    }
}
