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
    public partial class Goal : Form
    {
        private int labelCounter=1;

        public Goal()
        {
            InitializeComponent();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            var goal = guna2TextBox1.Text;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(guna2TextBox1.Text))
                return;

            string newItem = $"{labelCounter}. {guna2TextBox1.Text}";
            listBox1.Items.Add(newItem);

            // Only increment the labelCounter when adding a new item
            labelCounter++;

            guna2TextBox1.Clear();
            guna2TextBox1.Focus();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0)
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0 && !string.IsNullOrEmpty(guna2TextBox1.Text))
            {
                string updatedItem = $"{listBox1.SelectedIndex + 1}. {guna2TextBox1.Text}";
                listBox1.Items[listBox1.SelectedIndex] = updatedItem;

                guna2TextBox1.Clear();
                guna2TextBox1.Focus();
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            labelCounter = 1; // Reset the label counter to 1
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserDash ud = new UserDash();
            ud.Show();
        }
    }
}
