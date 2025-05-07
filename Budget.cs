using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Financepal
{
    public partial class Budget : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        public Budget()
        {
            InitializeComponent();
            LoadDataIntoDataGridView(); 
        }
        private void LoadDataIntoDataGridView()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Budget";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {
              
        }

        private void Budget_Load(object sender, EventArgs e)
        {
            Account form1 = new Account();

            // Get the calculated total amount from Form1
            decimal totalAmount = form1.CalculateTotalAmount();

            // Now you can use the totalAmount in Form2 as needed
            // For example, display it in a label:
            textBox3.Text = totalAmount.ToString("0.00");
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            var amount = Convert.ToDecimal(textBox3.Text);  
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var remain = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox2.Text, out int subtractAmount))
            {
                // Parse the current total amount from textBox3
                if (decimal.TryParse(textBox3.Text, out decimal totalAmount))
                {
                    // Subtract the integer from the total amount
                    decimal newAmount = totalAmount - subtractAmount;

                    // Display the new amount in textBox1
                    textBox1.Text = newAmount.ToString("0.00");
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = comboBox1.Text;   
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string eventName = comboBox1.Text;
            if (decimal.TryParse(textBox3.Text, out decimal amount))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO Budget (Event, Amount) VALUES (@Event, @Amount)";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@Event", eventName);
                    insertCommand.Parameters.AddWithValue("@Amount", amount);
                    insertCommand.ExecuteNonQuery();
                }

                LoadDataIntoDataGridView();
                ClearInputFields();
            }
            else
            {
                MessageBox.Show("Please enter a valid amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearInputFields()
        {
            comboBox1.SelectedIndex = -1;
            textBox3.Clear();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserDash ud = new UserDash();
            ud.Show();
        }
    }
}
