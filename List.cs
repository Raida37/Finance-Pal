using Guna.UI2.WinForms;
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
    public partial class List : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        public List()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string name = textBox1.Text;
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {
            double amount = Convert.ToDouble(guna2TextBox2.Text);   
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string category = guna2ComboBox1.Text;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-640F2MM\\SQLEXPRESS;Initial Catalog=MyDB;Integrated Security=True");
            SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[list]
           ([title]
           ,[amount]
           
           ,[category]
           ,[description])
     VALUES
           ('" + textBox1 + "','" + guna2TextBox2.Text + "','" + guna2ComboBox1.Text + "','" + guna2TextBox3.Text + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            LoadDataIntoDataGridView();
            con.Close();
            MessageBox.Show("Record inserted successfully");
            LoadDataIntoDataGridView();

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {
            string description = guna2TextBox3.Text;    
        }

        private void LoadDataIntoDataGridView()
        {
            string connectionString = cs; // Replace with your actual connection string

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM list"; // Adjust the query to select the appropriate columns

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                DataGridView1.DataSource = dataTable; // dataGridView1 is the name of your DataGridView
            }
        }

        private void List_Load(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView();
        }

        private void RemoveSelectedRow()
        {
            if (DataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this row?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    DataGridViewRow selectedRow = DataGridView1.SelectedRows[0];
                    DataGridView1.Rows.Remove(selectedRow);
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            RemoveSelectedRow();    
        }

        private DataGridViewRow selectedRow;

        private void LoadDataForEditing()
        {
            if (DataGridView1.SelectedRows.Count > 0)
            {
                selectedRow = DataGridView1.SelectedRows[0];

                // Populate input fields or controls with data from selectedRow
                string title = selectedRow.Cells["title"].Value.ToString();
                decimal amount = Convert.ToDecimal(selectedRow.Cells["amount"].Value);
                string description = selectedRow.Cells["description"].Value.ToString();
                string category = selectedRow.Cells["category"].Value.ToString();

                // Populate your input fields or controls here
                textBox1.Text = title;
                guna2TextBox2.Text = amount.ToString();
                guna2TextBox3.Text = description;
                guna2ComboBox1.SelectedItem = category;
            }
        }

        private void UpdateSelectedRow()
        {
            if (selectedRow != null)
            {
                // Update the selectedRow with the modified data
                selectedRow.Cells["title"].Value = textBox1.Text;
                selectedRow.Cells["amount"].Value = Convert.ToDecimal(guna2TextBox2.Text);
                selectedRow.Cells["description"].Value = guna2TextBox3.Text;
                selectedRow.Cells["category"].Value = guna2ComboBox1.SelectedItem.ToString();

                // Clear the selection
                DataGridView1.ClearSelection();

                // Clear the reference to the selectedRow
                selectedRow = null;

                // Clear the input fields or controls
                ClearInputFields();
            }
        }

        private void ClearInputFields()
        {
            // Clear or reset your input fields or controls here
            textBox1.Clear();
            guna2TextBox2.Clear();
            guna2TextBox3.Clear();
            guna2ComboBox1.SelectedIndex = -1;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
           
            UpdateSelectedRow();
        }
        private void ClearDataGridView()
        {
            
            for (int i = DataGridView1.Rows.Count - 1; i >= 0; i--)
            {
                DataGridViewRow row = DataGridView1.Rows[i];

                if (row.IsNewRow)
                {
                    DataGridView1.Rows.Remove(row);
                }
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserDash ud = new UserDash();
            ud.Show();
        }
    }
}
