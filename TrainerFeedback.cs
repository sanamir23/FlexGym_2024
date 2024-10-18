using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DBMS_PROJECT
{
    public partial class TrainerFeedback : Form
    {
        private string username;
        private string role;
        public TrainerFeedback()
        {
            InitializeComponent();
            this.username = "";
            this.role = "";
        }
        public TrainerFeedback(string username, string role)
        {
            InitializeComponent();
            this.username = username;
            this.role = role;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TrainerFeedback_Load(object sender, EventArgs e)
        {
            string query = "";
            query = "SELECT * FROM Feedback WHERE TrainerUsername = @Username";
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
            this.Close();
            UserForm user = new UserForm(this.role, this.username);
            user.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;

            if (comboBox1.SelectedItem.ToString() == "MemberUsername")
            {
                string query = "";
                string name = textBox1.Text;

                query = "SELECT * FROM Feedback WHERE TrainerUsername = @Username AND MemberUsername = @Name";
                string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";

                // Create a DataTable to hold the results
                DataTable dataTable = new DataTable();

                // Establish a connection and execute the query
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", this.username);
                    command.Parameters.AddWithValue("@Name", name);

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

            if (comboBox1.SelectedItem.ToString() == "Rating")
            {
                string query = "";
                int rating = int.Parse(textBox1.Text);

                query = "SELECT * FROM Feedback WHERE TrainerUsername = @Username AND TrainerRating = @Rating";
                string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";

                // Create a DataTable to hold the results
                DataTable dataTable = new DataTable();

                // Establish a connection and execute the query
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", this.username);
                    command.Parameters.AddWithValue("@Rating", rating);

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

        }
    }
}
