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
    public partial class YourMember : Form
    {
        private string username;
        public YourMember()
        {
            InitializeComponent();
            this.username = "";
        }
        public YourMember(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void YourMember_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";


            // Construct the SQL query to select GymName with OwnerUsername
            string gymQuery = "SELECT GymName FROM Gym WHERE OwnerUsername = @OwnerUsername";

            // Create a string variable to store GymName
            string gymName = "";

            // Establish a connection and execute the query to get GymName
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(gymQuery, connection);
                command.Parameters.AddWithValue("@OwnerUsername", this.username);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    // Check if GymName is retrieved
                    if (reader.Read())
                    {
                        gymName = reader["GymName"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("No gym found for the provided owner username.");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    return;
                }
            }

            // Construct the SQL query to select membership info based on GymName
            string membershipQuery = "SELECT * FROM MembershipInfo WHERE GymName = @GymName";

            // Create a DataTable to hold the results of MembershipInfo
            DataTable membershipDataTable = new DataTable();

            // Establish a connection and execute the query to get MembershipInfo
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(membershipQuery, connection);
                command.Parameters.AddWithValue("@GymName", gymName);

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(membershipDataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    return;
                }
            }
            dataGridView1.DataSource = membershipDataTable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            OwnerForm owner = new OwnerForm(this.username);
            owner.Show();
        }
    }
}
