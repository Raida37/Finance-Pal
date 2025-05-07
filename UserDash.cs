using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Financepal
{
    public partial class UserDash : Form
    {
        public UserDash()
        {
            InitializeComponent();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Budget budget = new Budget();


            budget.Show();  
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dailyexpense DE = new Dailyexpense();
            DE.Show();  
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            List l = new List();
            l.Show();   
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Account acc = new Account();
            acc.Show(); 

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Graph g = new Graph();
            g.Show();   
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Goal goal = new Goal();
            goal.Show();    
        }

        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Feedback feedback = new Feedback();
            feedback.Show();    
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Userprofile up = new Userprofile();
            up.Show();  
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();  
            login.Show();   
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            string searchQuery = "https://www.google.com/maps/search/atm+near+me";

            // Open the default browser with the Google Maps URL
            Process.Start(new ProcessStartInfo
            {
                FileName = searchQuery,
                UseShellExecute = true
            });
        }
    }
}
