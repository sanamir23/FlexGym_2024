using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBMS_PROJECT
{
    public partial class GymPerformance : Form
    {
        private string username;
        public GymPerformance()
        {
            InitializeComponent();
            this.username = "";
        }
        public GymPerformance(string username)
        {
            InitializeComponent();
            this.username = username;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            string query = "";
            string gymName = textBox1.Text;
            query = "SELECT  GymName, Rating,Attendance,Satisfaction FROM Gym WHERE AdminUsername = @Username AND GymName = @GymName";

            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";

            // Create a DataTable to hold the results
            DataTable dataTable = new DataTable();

            // Establish a connection and execute the query
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", this.username);
                command.Parameters.AddWithValue("@GymName", gymName);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                try
                {
                    connection.Open();
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            // Bind the DataTable to the DataGridView
            dataGridView1.DataSource = dataTable;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            AdminForm a = new AdminForm();
            a.Show();
        }
    }
}
