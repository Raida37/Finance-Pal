using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Financepal
{
    public partial class admindash : Form
    {
        public admindash()
        {
            InitializeComponent();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Usercontrol userControl1 = new Usercontrol();
            userControl1.Show();    
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            showFeedback showFeedback = new showFeedback();
            showFeedback.Show();
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
