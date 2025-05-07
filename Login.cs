using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Forms;

namespace Financepal
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        string cs= ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool status = guna2CheckBox1.Checked;
            if (status == true)
            {
                guna2TextBox2.UseSystemPasswordChar = false;
            }
            else if (status == false)
            {
                guna2TextBox2.UseSystemPasswordChar = true;
            }

        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
        private void button1_Click(object sender, EventArgs e)
        {
            Register registerForm = new Register();
            this.Hide();
            registerForm.Show();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            Focus();
            
        }

        private void guna2TextBox1_Leave(object sender, EventArgs e)
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
        }

        private void guna2TextBox2_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(guna2TextBox2.Text.Trim()))
            {
                errorProvider2.SetError(guna2TextBox2, "This field can not be left empty");
                return;
            }
            else
            {
                errorProvider2.SetError(guna2TextBox2, "");
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text!="" && guna2TextBox2.Text!="")
            {
                if (guna2TextBox1.Text=="admin" && guna2TextBox2.Text=="admin")
                {
                    this.Hide();    
                    admindash adminForm = new admindash();  
                    adminForm.Show();   
                }
                else
                {
                    SqlConnection con = new SqlConnection(cs);
                    string query = "select * from UserRegistration where email=@Username and password = @password";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Username", guna2TextBox1.Text);
                    cmd.Parameters.AddWithValue("@password", guna2TextBox2.Text);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows == true)
                    {
                        MessageBox.Show("Login Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UserDash dashboardForm = new UserDash();
                        this.Hide();
                        dashboardForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Login Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    con.Close();
                }
                
            }
            else
            {
                MessageBox.Show("Please enter email and password", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }   
        }
    }
}
