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
    public partial class EditSession : Form
    {
        private string role;
        private string username;
        public EditSession()
        {
            InitializeComponent();
            this.username = "";
            this.role = "";
        }
        public EditSession(string role, string username)
        {
            InitializeComponent();
            this.role = role;
            this.username = username;
        }

        private void EditSession_Load(object sender, EventArgs e)
        {


            string query = "";
            if (this.role == "Gym Member")
            {
                query = "SELECT * FROM TrainingSession WHERE MemberUsername = @Username";
            }
            else if (this.role == "Gym Trainer")
            {
                query = "SELECT * FROM TrainingSession WHERE TrainerUsername = @Username";
            }

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
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";

            DataTable dataTable = new DataTable();
            int sessionID;

            if (!int.TryParse(textBox1.Text, out sessionID))
            {
                MessageBox.Show("Please enter a valid session ID.");
                return;
            }

            // Check if the sessionID exists in the TrainingSession table
            string query = "SELECT COUNT(*) FROM TrainingSession WHERE SessionID = @SessionID";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SessionID", sessionID);

                        int count = (int)command.ExecuteScalar();

                        if (count > 0)
                        {
                            // SessionID exists, proceed with deletion
                            string deleteQuery = "DELETE FROM TrainingSession WHERE SessionID = @SessionID";

                            using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                            {
                                deleteCommand.Parameters.AddWithValue("@SessionID", sessionID);

                                int rowsAffected = deleteCommand.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Session deleted successfully.");
                                }
                                else
                                {
                                    MessageBox.Show("Error: Failed to delete session.");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Session ID does not exist. Please enter a valid ID.");
                        }
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
            if(string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Kindly fill out the necessary information");
                return;
            }
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";
            int sessionID;
            // Parse sessionID from textBox
            
            if (!int.TryParse(textBox2.Text, out sessionID))
            {
                MessageBox.Show("Please enter a valid session ID.");
                return;
            }

            // Get the column to change and the updated value
            string columnToChange = comboBox1.SelectedItem.ToString();
            string updateTo = textBox3.Text;

            // Construct the update query
            string query = $"UPDATE TrainingSession SET {columnToChange} = @UpdateTo WHERE SessionID = @SessionID";

            // Establish a connection and execute the query
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UpdateTo", updateTo);
                command.Parameters.AddWithValue("@SessionID", sessionID);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Session updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Session not found or no changes made.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Session session = new Session(this.role, this.username);
            session.Show();
        }
    }
}
