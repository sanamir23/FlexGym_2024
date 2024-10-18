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
    public partial class YourTrainer : Form
    {
        private string username;
        private bool flag;
        public YourTrainer()
        {
            InitializeComponent();
            this.username = "";
            
        }
        public YourTrainer(string username)
        {
            InitializeComponent();
            this.username = username;
          
            DataGridViewComboBoxColumn approvalColumn = new DataGridViewComboBoxColumn();
            approvalColumn.Name = "ApprovalStatus";
            approvalColumn.HeaderText = "Approval Status";
            approvalColumn.Items.AddRange("Accept", "Decline");
            dataGridView1.Columns.Add(approvalColumn);
        }

        private void YourTrainer_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";


            // Construct the SQL query to select GymName with OwnerUsername
            string gymQuery = "SELECT GymName FROM Gym WHERE OwnerUsername = @OwnerUsername";
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
            string TrainerQuery = "SELECT * FROM Trainer_Gym WHERE GymName = @GymName";

            // Create a DataTable to hold the results of MembershipInfo
            DataTable membershipDataTable = new DataTable();

            // Establish a connection and execute the query to get MembershipInfo
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(TrainerQuery, connection);
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

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["ApprovalStatus"].Index && e.RowIndex >= 0)
            {
                // Get the selected value from the combo box cell
                DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)dataGridView1.Rows[e.RowIndex].Cells["ApprovalStatus"];
                string selectedValue = comboBoxCell.Value.ToString();

                // If the selected value is "Decline", delete the corresponding entry from Trainer_Gym
                if (selectedValue == "Decline")
                {
                    // Get the TrainerUsername and GymName from the selected row
                    string trainerUsername = dataGridView1.Rows[e.RowIndex].Cells["TrainerUsername"].Value.ToString();
                    string gymName = dataGridView1.Rows[e.RowIndex].Cells["GymName"].Value.ToString();

                    string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";
                    string deleteQuery = "DELETE FROM Trainer_Gym WHERE TrainerUsername = @TrainerUsername AND GymName = @GymName";

                    // Execute the delete query
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(deleteQuery, connection);
                        command.Parameters.AddWithValue("@TrainerUsername", trainerUsername);
                        command.Parameters.AddWithValue("@GymName", gymName);

                        try
                        {
                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Trainer entry deleted successfully.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
                else if (selectedValue == "Accept")
                {
                    MessageBox.Show("Trainer accepted into the gym successfully");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            this.Close();
            OwnerForm owner = new OwnerForm(this.username);
            owner.Show();

        }

        
    }
}
