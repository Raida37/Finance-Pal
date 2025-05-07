using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Windows.Forms;

namespace Financepal
{
    public partial class Register : Form
    {
        string cs= ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        public Register()
        {
            InitializeComponent();
        }

        private void guna2TextBox10_TextChanged(object sender, EventArgs e)
        {
            string password = guna2TextBox10.Text;
        }

        private void guna2TextBox14_TextChanged(object sender, EventArgs e)
        {
           
            string retypePassword = guna2TextBox14.Text;

            }

        private void guna2TileButton1_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(guna2TextBox1.Text.Trim()))
            {
                errorProvider1.SetError(guna2TextBox1, "This field can not be left empty");
                return;
            }
            else
            {
                errorProvider1.SetError(guna2TextBox1, "");
            }

            if (String.IsNullOrEmpty(guna2TextBox4.Text.Trim()))
            {
                errorProvider2.SetError(guna2TextBox4, "This field can not be left empty");
                return;
            }
            else
            {
                errorProvider2.SetError(guna2TextBox4, "");
            }

            if (String.IsNullOrEmpty(guna2TextBox6.Text.Trim()))
            {
                errorProvider3.SetError(guna2TextBox6, "This field can not be left empty");
                return;
            }
            else
            {
                errorProvider3.SetError(guna2TextBox6, "");
            }

            if (String.IsNullOrEmpty(guna2TextBox8.Text.Trim()))
            {
                errorProvider4.SetError(guna2TextBox8, "This field can not be left empty");
                return;
            }
            else
            {
                errorProvider4.SetError(guna2TextBox8, "");
            }

            if (String.IsNullOrEmpty(guna2TextBox10.Text.Trim()))
            {
                errorProvider5.SetError(guna2TextBox10, "This field can not be left empty");
                return;
            }
            else
            {
                errorProvider5.SetError(guna2TextBox10, "");
            }

            if (String.IsNullOrEmpty(guna2TextBox14.Text.Trim()))
            {
                errorProvider6.SetError(guna2TextBox14, "This field can not be left empty");
                return;
            }
            else
            {
                errorProvider6.SetError(guna2TextBox14, "");
            }   

            string password = guna2TextBox10.Text;
            string retypePassword = guna2TextBox14.Text;

            if (password==retypePassword)
            {
                MessageBox.Show("register confirm!");
            }
            else
            {
                MessageBox.Show("regster not confirm!");   
                errorProvider7.SetError(guna2TextBox14, "Password did not match");
            }

            SqlConnection con = new SqlConnection("Data Source=DESKTOP-640F2MM\\SQLEXPRESS;Initial Catalog=MyDB;Integrated Security=True");
            SqlCommand cmd= new SqlCommand(@"INSERT INTO [dbo].[UserRegistration]
           ([first_name]
           ,[last_name]
           ,[email]
           ,[contact_number]
           ,[password])
     VALUES
           ('"+guna2TextBox1.Text+"','"+guna2TextBox4.Text+"','"+guna2TextBox6.Text +"','"+guna2TextBox8.Text +"','"+guna2TextBox14.Text+"')", con);
            con.Open();   
            cmd.ExecuteNonQuery();  
            con.Close();   
            MessageBox.Show("Record inserted successfully");
        }

        private void guna2TextBox9_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
             this.Hide();   
            Login login = new Login();
            login.Show();   
        }
    }
}
