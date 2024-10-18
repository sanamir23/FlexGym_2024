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
    public partial class BrowseWorkoutDiet : Form
    {
        private string username;
        private string role;
        private string plan;
        private bool searchPressed;
        public BrowseWorkoutDiet()
        {
            InitializeComponent();
            this.username = "";
            this.role = "";
            this.plan = "";

            searchPressed = false;

        }
        public BrowseWorkoutDiet(string username, string role, string plan)
        {
            InitializeComponent();
            this.username = username;
            this.role = role;
            this.plan = plan;
            searchPressed = false;
            comboBox1.Items.Clear();
            if (this.plan == "Workout")
            {
                label1.Text = "Enter WorkoutID:";
                comboBox1.Items.Add("WorkoutPlan");
                comboBox1.Items.Add("CreatedBy");
                comboBox1.Items.Add("ExperienceLevel");
                comboBox1.Items.Add("Goal");

            }
            else if (this.plan == "Diet")
            {
                label1.Text = "Enter DietID:";
                comboBox1.Items.Add("DietName");
                comboBox1.Items.Add("CreatedBy");
                comboBox1.Items.Add("Purpose");
                comboBox1.Items.Add("DietType");
            }
        }


        private void YourForm_Load(object sender, EventArgs e)
        {
            string query = "";
            if (this.plan == "Workout")
            {
                query = "SELECT * FROM WorkoutPlan";
            }
            else if (this.plan == "Diet")
            {
                query = "SELECT * FROM DietPlan";
            }
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";

            // Create a DataTable to hold the results
            DataTable dataTable = new DataTable();

            // Establish a connection and execute the query
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
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
            // Parse the entered ID from the textbox to an integer
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "";
                    if (this.plan == "Workout")
                    {
                        if (role == "Gym Trainer")
                        {
                            // Check if Trainer already exists in TrainerWorkoutInfo
                            query = "IF EXISTS (SELECT 1 FROM TrainerWorkoutInfo WHERE TrainerUsername = @TrainerUsername)" +
                                    "   UPDATE TrainerWorkoutInfo SET WorkoutID = @WorkoutID WHERE TrainerUsername = @TrainerUsername" +
                                    " ELSE" +
                                    "   INSERT INTO TrainerWorkoutInfo (TrainerUsername, WorkoutID) VALUES (@TrainerUsername, @WorkoutID)";
                        }
                        else
                        {
                            // Check if Member already exists in MemberWorkoutInfo
                            query = "IF EXISTS (SELECT 1 FROM MemberWorkoutInfo WHERE MemberUsername = @MemberUsername)" +
                                    "   UPDATE MemberWorkoutInfo SET WorkoutID = @WorkoutID WHERE MemberUsername = @MemberUsername" +
                                    " ELSE" +
                                    "   INSERT INTO MemberWorkoutInfo (MemberUsername, WorkoutID) VALUES (@MemberUsername, @WorkoutID)";
                        }
                    }
                    else if (this.plan == "Diet")
                    {
                        if (role == "Gym Trainer")
                        {
                            // Check if Trainer already exists in TrainerDietInfo
                            query = "IF EXISTS (SELECT 1 FROM TrainerDietInfo WHERE TrainerUsername = @TrainerUsername)" +
                                    "   UPDATE TrainerDietInfo SET DietPlanID = @DietPlanID WHERE TrainerUsername = @TrainerUsername" +
                                    " ELSE" +
                                    "   INSERT INTO TrainerDietInfo (TrainerUsername, DietPlanID) VALUES (@TrainerUsername, @DietPlanID)";
                        }
                        else
                        {
                            // Check if Member already exists in MemberDietInfo
                            query = "IF EXISTS (SELECT 1 FROM MemberDietInfo WHERE MemberUsername = @MemberUsername)" +
                                    "   UPDATE MemberDietInfo SET DietPlanID = @DietPlanID WHERE MemberUsername = @MemberUsername" +
                                    " ELSE" +
                                    "   INSERT INTO MemberDietInfo (MemberUsername, DietPlanID) VALUES (@MemberUsername, @DietPlanID)";
                        }
                    }

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (role == "Gym Trainer")
                        {
                            command.Parameters.AddWithValue("@TrainerUsername", username);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@MemberUsername", username);
                        }

                        if (this.plan == "Workout")
                        {
                            int enteredID;
                            if (!int.TryParse(textBox1.Text, out enteredID))
                            {
                                MessageBox.Show("Please enter a valid WorkoutID.");
                                return;
                            }
                            command.Parameters.AddWithValue("@WorkoutID", enteredID);
                        }
                        else if (this.plan == "Diet")
                        {
                            int enteredID;
                            if (!int.TryParse(textBox1.Text, out enteredID))
                            {
                                MessageBox.Show("Please enter a valid DietID.");
                                return;
                            }
                            command.Parameters.AddWithValue("@DietPlanID", enteredID);
                        }

                        command.ExecuteNonQuery();
                        MessageBox.Show("Plan added successfully.");
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
            searchPressed = true;
            string selectedAttribute = comboBox1.SelectedItem.ToString();
            string searchKeyword = textBox2.Text;
            if (string.IsNullOrEmpty(selectedAttribute) || string.IsNullOrEmpty(searchKeyword))
            {
                MessageBox.Show("Kindly enter all the details");
                return;
            }


            if (this.plan == "Workout")
            {
                // Write your SQL query
                string query = $"SELECT * FROM WorkoutPlan WHERE {selectedAttribute} = '{searchKeyword}'";

                string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";

                // Create a DataTable to hold the results
                DataTable dataTable = new DataTable();

                // Establish a connection and execute the query
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@searchKeyword", searchKeyword);
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
                dataGridView1.DataSource = dataTable;
            }
            else if (this.plan == "Diet")
            {
                // Write your SQL query
                string query = $"SELECT * FROM DietPlan WHERE {selectedAttribute} = '{searchKeyword}'";

                string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";

                // Create a DataTable to hold the results
                DataTable dataTable = new DataTable();

                // Establish a connection and execute the query
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@searchKeyword", searchKeyword);
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
                dataGridView1.DataSource = dataTable;
            }            // Bind the DataTable to the DataGridView
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            if (plan == "Workout")
            {
                WorkoutPlan workoutPlan = new WorkoutPlan(role, username);
                workoutPlan.Show();
            }
            else if (plan == "Diet")
            {
                DietPlan dietPlan = new DietPlan(role, username);
                dietPlan.Show();
            }
        }
    }
}


