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
using System.Windows.Forms.DataVisualization.Charting;

namespace Financepal
{
    public partial class Graph : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        public Graph()
        {
            InitializeComponent();
            InitializeChart();  
            InitializeChart2();
        }

        private void InitializeChart()
        {
            chart1.Series.Clear();

            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                string query = "SELECT eventName, amount FROM DailyExpenses"; // Adjust the query as needed

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                Series series = new Series("MySeries");
                series.ChartType = SeriesChartType.Pie;

                series.Points.DataBind(dataTable.DefaultView, "eventName", "amount", null);

                chart1.Series.Add(series);
            }

        }

        private void InitializeChart2()
        {
            chart2.Series.Clear();

            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                string query = "SELECT event, amount FROM Budget"; // Adjust the query as needed

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                Series series = new Series("MySeries");
                series.ChartType = SeriesChartType.Column;

                series.Points.DataBind(dataTable.DefaultView, "event", "amount", null);

                chart2.Series.Add(series);
            }
        }
        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
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
