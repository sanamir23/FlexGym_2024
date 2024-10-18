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
    public partial class GymRegisteration : Form
    {
        private string username;
        public GymRegisteration()
        {
            InitializeComponent();
            this.username = "";
        }
        public GymRegisteration(string username)
        {
            InitializeComponent();
            this.username = username;
        }
        public void GymRegister_Load(object sender, EventArgs e)
        {
            string query = "";
            query = "SELECT * FROM Gym WHERE AdminUsername = @Username";

            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";

            // Create a DataTable to hold the results
            DataTable dataTable = new DataTable();

            // Establish a connection and execute the query
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", this.username);
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

        private void button1_Click(object sender, EventArgs e)
        {
            string gymName = textBox1.Text;

            // Check if the gym name is empty
            if (string.IsNullOrEmpty(gymName))
            {
                MessageBox.Show("Please enter a gym name.");
                return;
            }

            // Construct the SQL query to delete the gym
            string query = "DELETE FROM Gym WHERE GymName = @GymName";

            // Define the connection string
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";

            // Perform the deletion
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@GymName", gymName);

                    // Open the connection
                    connection.Open();

                    // Execute the query
                    int rowsAffected = command.ExecuteNonQuery();

                    // Check if any rows were affected
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Gym deleted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Gym does not exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            AdminForm form = new AdminForm(this.username);
            form.Show();
        }
    }
}
