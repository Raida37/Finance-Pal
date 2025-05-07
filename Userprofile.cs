using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;

namespace Financepal
{
    public partial class Userprofile : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        public Userprofile()
        {
            InitializeComponent();
        }

        private void Insert(string name, string email, string phoneNumber, string password, byte[] image, DateTime dob)
        {
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Users (Name, Email, PhoneNumber, Password, ProfileImage, RegistrationDate) VALUES (@Name, @Email, @PhoneNumber, @Password, @Image, @RegistrationDate)", connection);

                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@Image", image);
                command.Parameters.AddWithValue("@RegistrationDate", DateTime.Now); // You can adjust this as needed
               // command.Parameters.AddWithValue("@RegistrationDate", dob);

                command.ExecuteNonQuery();
            }
        }
        public void LoadData()
        {
            LoadData(guna2CirclePictureBox1);
        }

        public void LoadData(PictureBox pictureBox1)
        {
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT ProfileImage FROM Users", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        byte[] imageBytes = (byte[])reader["ProfileImage"];
                        guna2CirclePictureBox1.Image = ConvertBytesToImage(imageBytes);
                        guna2CirclePictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
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
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Separator1_Click(object sender, EventArgs e)
        {

        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            string name = guna2TextBox1.Text;   
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            string name = guna2TextBox1.Text;
            string email = guna2TextBox4.Text;
            string phoneNumber = guna2TextBox5.Text;
            string password = guna2TextBox3.Text;
            DateTime dob = guna2DateTimePicker1.Value;

            // Check if all input fields (excluding picture) are filled
            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(phoneNumber) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fill in all fields (excluding picture).");
                return;
            }

            // Convert the image to bytes
            byte[] imageBytes = ConvertImageToBytes(guna2CirclePictureBox1.Image);

            // Insert user inputs and image into the database
            Insert(name, email, phoneNumber, password, imageBytes, dob);

            // Load and display the newly added image
            LoadData(guna2CirclePictureBox1);

            MessageBox.Show("User data and picture inserted successfully.");
        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {
            string email = guna2TextBox4.Text;  
        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "JPEG|*.jpg", ValidateNames = true, Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // Load the image into PictureBox
                    guna2CirclePictureBox1.Image = Image.FromFile(ofd.FileName);
                    guna2CirclePictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
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
