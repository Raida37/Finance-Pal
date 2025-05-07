using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Financepal
{
    public partial class Dailyexpense : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        private DataGridViewRow selectedRow;
        public Dailyexpense()
        {
            InitializeComponent();
            LoadDataIntoDataGridView();
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string name = textBox1.Text;    
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            double amount = Convert.ToDouble(textBox2.Text);    
        }
        private void LoadDataIntoDataGridView()
        {
            string connectionString = cs; // Replace with your actual connection string

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM DailyExpenses"; // Adjust the query to select the appropriate columns

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                guna2DataGridView1.DataSource = dataTable; // dataGridView1 is the name of your DataGridView
                UpdateTotalAmount();
            }
           
        }
        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedRow = guna2DataGridView1.Rows[e.RowIndex];
                textBox1.Text = selectedRow.Cells["eventName"].Value.ToString();
                textBox2.Text = selectedRow.Cells["amount"].Value.ToString();
            }
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-640F2MM\\SQLEXPRESS;Initial Catalog=MyDB;Integrated Security=True");
            SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[Dailyexpenses]
           ([eventName]
           ,[amount])
           
     VALUES
           ('" + textBox1.Text + "','" + textBox2.Text + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            LoadDataIntoDataGridView();
            con.Close();
            MessageBox.Show("Record inserted successfully");
            UpdateTotalAmount();
        }

        private void RemoveSelectedRow()
        {
            if (guna2DataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this row?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
                    guna2DataGridView1.Rows.Remove(selectedRow);
                    UpdateTotalAmount();
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            RemoveSelectedRow();
        }

        private void UpdateSelectedRow()
        {
            if (selectedRow != null)
            {
                // Update the selectedRow with the modified data
                selectedRow.Cells["eventName"].Value = textBox1.Text;
                selectedRow.Cells["amount"].Value = Convert.ToDecimal(textBox2.Text);
               

                // Clear the selection
                guna2DataGridView1.ClearSelection();

                // Clear the reference to the selectedRow
                selectedRow = null;

                // Clear the input fields or controls
                ClearInputFields();
            }
        }

        private void ClearInputFields()
        {
            textBox1.Clear();
            textBox2.Clear();   

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            UpdateSelectedRow();
            guna2DataGridView1.Refresh();
        }

        private void UpdateTotalAmount()
        {
            decimal totalAmount = 0;

            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                if (row.Cells["amount"].Value != null)
                {
                    totalAmount += Convert.ToDecimal(row.Cells["amount"].Value);
                }
            }

            textBox3.Text = totalAmount.ToString("0.00");
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserDash ud = new UserDash();
            ud.Show();
        }
    }
}
