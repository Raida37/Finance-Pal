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
    public partial class showFeedback : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        public showFeedback()
        {
            InitializeComponent();
            LoadFeedback();
        }
        private void LoadFeedback()
        {
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                string selectFeedbackQuery = "SELECT *FROM Feedback";

                using (SqlCommand command = new SqlCommand(selectFeedbackQuery, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = reader.GetString(0);
                        string email = reader.GetString(1);
                        string message = reader.GetString(2);
                        listBox1.Items.Add($"{name} - {email}");
                    }
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Hide();    
            admindash admindash = new admindash();
            admindash.Show();   
        }
    }
}
