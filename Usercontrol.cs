using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Financepal
{
    public partial class Usercontrol : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;

        public Usercontrol()
        {
            InitializeComponent();
            LoadUserData();
        }

        private void LoadUserData()
        {
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();
                string selectQuery = "SELECT Name, Email, PhoneNumber,  RegistrationDate, ProfileImage FROM Users";

                using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Create a new DataTable with desired columns
                    DataTable newDataTable = new DataTable();
                    newDataTable.Columns.Add("Name", typeof(string));
                    newDataTable.Columns.Add("Email", typeof(string));
                    newDataTable.Columns.Add("PhoneNumber", typeof(string));
                    //newDataTable.Columns.Add("DOB", typeof(DateTime));
                    newDataTable.Columns.Add("RegistrationDate", typeof(DateTime));
                    newDataTable.Columns.Add("ProfileImage", typeof(Image)); // This will hold images

                    foreach (DataRow row in dataTable.Rows)
                    {
                        // Convert bytes to image
                        byte[] imageBytes = (byte[])row["ProfileImage"];
                        Image image = ConvertBytesToImage(imageBytes);

                        // Add data to the new DataTable
                        newDataTable.Rows.Add(
                            row["Name"],
                            row["Email"],
                            row["PhoneNumber"],
                           // row["DOB"],
                            row["RegistrationDate"],
                            image
                        );
                    }

                    dataGridView1.DataSource = newDataTable;
                }
            }
        }

        byte[] ConvertImageToBytes(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        public Image ConvertBytesToImage(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return Image.FromStream(ms);
            }
        }

        private void Usercontrol_Load(object sender, EventArgs e)
        {
            LoadUserData();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                string name = selectedRow.Cells["Name"].Value.ToString(); // Replace "Name" with the actual column name in the DataGridView
                string email = selectedRow.Cells["Email"].Value.ToString(); // Replace "Email" with the actual column name in the DataGridView

                using (SqlConnection connection = new SqlConnection(cs))
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM Users WHERE Name = @Name AND Email = @Email";

                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Email", email);

                        command.ExecuteNonQuery();
                        MessageBox.Show("User record deleted successfully.");

                        // Refresh the DataGridView to reflect the changes
                        LoadUserData();
                        dataGridView1.Refresh();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
           admindash dashboard = new admindash();   
            dashboard.Show();   
        }
    }
}
