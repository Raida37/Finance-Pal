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
    public partial class Account : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        public Account()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            double amount = Convert.ToDouble(textBox1.Text);    
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string accName = comboBox1.Text;    

        }
        private void LoadDataIntoDataGridView()
        {
            string connectionString = cs; // Replace with your actual connection string

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Accounts"; // Adjust the query to select the appropriate columns

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable; // dataGridView1 is the name of your DataGridView
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-640F2MM\\SQLEXPRESS;Initial Catalog=MyDB;Integrated Security=True");
            SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[Accounts]
           ([accountName]
           ,[amount]
           )
     VALUES
           ('" + comboBox1.Text + "','" + textBox1.Text+ "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            LoadDataIntoDataGridView();
            con.Close();
            MessageBox.Show("Record inserted successfully");
            LoadDataIntoDataGridView();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this row?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int rowIndex = dataGridView1.SelectedRows[0].Index;

                    if (rowIndex >= 0)
                    {
                        int accountId = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["id"].Value);

                        using (SqlConnection connection = new SqlConnection(cs))
                        {
                            connection.Open();
                            string deleteQuery = "DELETE FROM Accounts WHERE id = @AccountId";
                            SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                            deleteCommand.Parameters.AddWithValue("@AccountId", accountId);
                            deleteCommand.ExecuteNonQuery();
                        }

                        // Remove the row from the DataGridView
                        dataGridView1.Rows.RemoveAt(rowIndex);

                        MessageBox.Show("Record deleted successfully");
                    }
                }
            }
            }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedRows[0].Index;

                if (rowIndex >= 0)
                {
                    int accountId = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["id"].Value);
                    string updatedAccountName = comboBox1.Text;
                    double updatedAmount = Convert.ToDouble(textBox1.Text);

                    using (SqlConnection connection = new SqlConnection(cs))
                    {
                        connection.Open();
                        string updateQuery = "UPDATE Accounts SET accountName = @UpdatedAccountName, amount = @UpdatedAmount WHERE id = @AccountId";
                        SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@UpdatedAccountName", updatedAccountName);
                        updateCommand.Parameters.AddWithValue("@UpdatedAmount", updatedAmount);
                        updateCommand.Parameters.AddWithValue("@AccountId", accountId);
                        updateCommand.ExecuteNonQuery();
                    }

                    // Update the row in the DataGridView
                    dataGridView1.Rows[rowIndex].Cells["accountName"].Value = updatedAccountName;
                    dataGridView1.Rows[rowIndex].Cells["amount"].Value = updatedAmount;

                    MessageBox.Show("Record updated successfully");
                }
            }
        }

        internal decimal CalculateTotalAmount()
        {
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();
                string query = "SELECT SUM(Amount) FROM Budget";
                SqlCommand command = new SqlCommand(query, connection);

                object result = command.ExecuteScalar();
                if (result != DBNull.Value && result != null)
                {
                    return Convert.ToDecimal(result);
                }
                else
                {
                    return 0.0m; // Return 0 if there are no records or sum is null
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
