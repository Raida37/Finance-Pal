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
using System.Xml.Linq;

namespace Financepal
{
    public partial class Feedback : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        private SqlConnection connection;
        public Feedback()
        {
            InitializeComponent();
            connection = new SqlConnection(cs); 
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                string insertFeedbackQuery = @"
                    INSERT INTO Feedback (Name, Email, Message) VALUES (@Name, @Email, @Message)";

                using (SqlCommand command = new SqlCommand(insertFeedbackQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", textBox1.Text);
                    command.Parameters.AddWithValue("@Email", textBox2.Text);
                    command.Parameters.AddWithValue("@Message", textBox3.Text);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Feedback submitted successfully!");
                    ClearForm();
                }
            }
        }

        private void ClearForm()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserDash ud = new UserDash();
            ud.Show();
        }
    }
}
