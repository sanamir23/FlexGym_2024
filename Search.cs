using Microsoft.VisualBasic.ApplicationServices;
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
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DBMS_PROJECT
{
    public partial class Search : Form
    {
        public Search()
        {
            InitializeComponent();
        }

        private void report1(string gymName, string trainerUsername)
        {
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";
            // SQL query to retrieve member details
            string query = @"
                SELECT G.MemberName, G.Gender, G.Age, G.Email, G.SSN
                FROM GymMember AS G
                JOIN MembershipInfo AS M ON M.MemberUsername = G.MemberUsername
                WHERE M.GymName = @GymName AND M.TrainerUsername = @TrainerUsername";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@GymName", gymName);
                    command.Parameters.AddWithValue("@TrainerUsername", trainerUsername);

                    try
                    {
                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void report2(string gymName, string dietName)
        {
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";
            // SQL query to retrieve member details
            string query = @"
                   SELECT GM.MemberName, GM.Gender, GM.Age, GM.Email, GM.SSN
                   FROM GymMember AS GM
                   JOIN MembershipInfo AS MI ON GM.MemberUsername = MI.MemberUsername
                   JOIN MemberDietInfo AS DI ON GM.MemberUsername = DI.MemberUsername
                   JOIN DietPlan AS D ON DI.DietPlanID = D.DietPlanID
                   WHERE MI.GymName = @GymName AND D.DietName = @DietName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@GymName", gymName);
                    command.Parameters.AddWithValue("@DietName", dietName);

                    try
                    {
                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void report3(string trainerUsername, string dietName)
        {
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";
            // SQL query to retrieve member details
            string query = @"
                   SELECT GM.MemberName, GM.Gender, GM.Age, GM.Email, GM.SSN
                   FROM GymMember AS GM
                   JOIN MembershipInfo AS MI ON GM.MemberUsername = MI.MemberUsername
                   JOIN MemberDietInfo AS DI ON GM.MemberUsername = DI.MemberUsername
                   JOIN DietPlan AS D ON DI.DietPlanID = D.DietPlanID
                   WHERE MI.TrainerUsername = @TrainerUsername AND D.DietName = @DietName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@TrainerUsername", trainerUsername);
                    command.Parameters.AddWithValue("@DietName", dietName);

                    try
                    {
                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void report4(string day, string machineName, string gymName)
        {
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";
            // SQL query to retrieve member details
            string query = @"SELECT COUNT(*) AS MemberCount
                            FROM GymMember AS GM
                            JOIN MemberWorkoutInfo AS MI ON GM.MemberUsername = MI.MemberUsername
                            JOIN WorkoutSchedule AS W ON W.WorkoutID = MI.WorkoutID
                            JOIN Machine AS M ON M.ExerciseID = W.ExerciseID
                            JOIN MembershipInfo AS MS ON MS.MemberUsername = GM.MemberUsername 
                            JOIN Gym AS G ON G.GymName = MS.GymName
                            WHERE M.MachineName = @MachineName
                            AND W.WorkoutDay = @Day
                            AND G.GymName = @GymName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@GymName", gymName);
                    command.Parameters.AddWithValue("@Day", day);
                    command.Parameters.AddWithValue("@MachineName", machineName);

                    try
                    {
                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void report5(string mealTime, int calories)
        {
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";
            // SQL query to retrieve member details
            string query = @"SELECT DISTINCT DP.DietName
                            FROM DietPlan AS DP
                            JOIN DietSchedule AS DS ON DP.DietPlanID = DS.DietPlanID
                            JOIN Meal AS M ON DS.MealID = M.MealID
                            WHERE DS.MealTime = @MealTime
                            AND M.Calories < @Calories";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@MealTime", mealTime);
                    command.Parameters.AddWithValue("@Calories", calories);

                    try
                    {
                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void report6(string nutritionalComponent, int amount)
        {
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";
            // SQL query to retrieve member details
            string query = @"
                    SELECT DISTINCT DP.DietName
                    FROM DietPlan AS DP
                    JOIN DietSchedule AS DS ON DP.DietPlanID = DS.DietPlanID
                    JOIN Meal AS M ON DS.MealID = M.MealID
                    GROUP BY DP.DietName
                    HAVING (CASE @NC
                            WHEN 'Fat' THEN SUM(M.Fat)
                            WHEN 'Fiber' THEN SUM(M.Fiber)
                            WHEN 'Carbohydrate' THEN SUM(M.Carbs)
                            WHEN 'Protein' THEN SUM(M.Protein)
                        END) < @Amount";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@NC", nutritionalComponent);
                    command.Parameters.AddWithValue("@Amount", amount);

                    try
                    {
                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void report7(string machine)
        {
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";
            // SQL query to retrieve member details
            string query = @"
                    SELECT DISTINCT WP.WorkoutName
                    FROM WorkoutPlan AS WP
                    LEFT JOIN WorkoutSchedule AS WS ON WP.WorkoutID = WS.WorkoutID
                    LEFT JOIN Machine AS M ON WS.ExerciseID = M.ExerciseID AND M.MachineName = @MachineName
                    WHERE M.MachineName IS NULL;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@MachineName", machine);

                    try
                    {
                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }


        private void report8(string allergen)
        {
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";
            // SQL query to retrieve member details
            string query = @"
                    SELECT DISTINCT DP.DietName
                    FROM DietPlan AS DP
                    WHERE NOT EXISTS (
                        SELECT 1
                        FROM DietSchedule AS DS
                        JOIN Meal AS M ON DS.MealID = M.MealID
                        JOIN Allergens AS A ON M.MealID = A.MealID
                        WHERE DP.DietPlanID = DS.DietPlanID
                        AND A.AllergenName = @Allergen
                        );";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@Allergen", allergen);

                    try
                    {
                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void report9(int months)
        {
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";
            // SQL query to retrieve member details
            string query = @"
                    SELECT MI.*, M.MembershipType, M.MonthlyFee
                    FROM MembershipInfo AS MI
                    JOIN Membership AS M ON MI.MembershipID = M.MembershipID
                    WHERE MI.MemberSince >= DATEADD(month, -@M, GETDATE());";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@M", months);

                    try
                    {
                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void report10(int months)
        {
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";
            // SQL query to retrieve member details
            string query = @"
                    SELECT G.GymName, COUNT(MI.MemberUsername) AS TotalMembers
                    FROM Gym AS G
                    JOIN MembershipInfo AS MI ON G.GymName = MI.GymName
                    WHERE MI.MemberSince >= DATEADD(month, -@M, GETDATE())
                    GROUP BY G.GymName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@M", months);

                    try
                    {
                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void report11(string workoutName, string gymName)
        {
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";
            // SQL query to retrieve member details
            string query = @"SELECT gm.MemberName, gm.MemberUsername
                            FROM GymMember AS gm
                            JOIN MembershipInfo AS mi ON gm.MemberUsername = mi.MemberUsername
                            JOIN MemberWorkoutInfo AS mwi ON mi.MemberUsername = mwi.MemberUsername
                            JOIN WorkoutPlan AS wp ON mwi.WorkoutID = wp.WorkoutID
                            WHERE mi.GymName = @GymName AND wp.WorkoutName = @WorkoutName;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@GymName", gymName);
                    command.Parameters.AddWithValue("@WorkoutName", workoutName);

                    try
                    {
                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void report12(string level)
        {
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";
            // SQL query to retrieve member details
            string query = @"SELECT * 
                            FROM WorkoutPlan
                            WHERE ExperienceLevel = @ExpLevel";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@ExpLevel", level);

                    try
                    {
                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void report13(string muscle)
        {
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";
            // SQL query to retrieve member details
            string query = @"SELECT DISTINCT(wp.WorkoutName)
                            FROM WorkoutPlan wp
                            JOIN WorkoutSchedule ws ON wp.WorkoutID = ws.WorkoutID
                            WHERE ws.MuscleGroup = @Muscle;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@Muscle", muscle);

                    try
                    {
                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void report14(string package)
        {
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";
            // SQL query to retrieve member details
            string query = @"SELECT G.MemberUsername,M.MembershipType
                            FROM GymMember as G
                            JOIN MembershipInfo AS MI ON G.MemberUsername = MI.MemberUsername
                            JOIN Membership AS M ON MI.MembershipID = M.MembershipID
                            WHERE M.MembershipType = @Package";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@Package", package);

                    try
                    {
                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void report15(int months)
        {
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";
            // SQL query to retrieve member details
            string query = @"SELECT ts.SessionID, ts.SessionDate, ts.StartingTime, ts.EndingTime, gm.MemberName
                            FROM TrainingSession ts
                            JOIN GymMember gm ON ts.MemberUsername = gm.MemberUsername
                            WHERE ts.SessionDate >= DATEADD(MONTH, -@Months, GETDATE());";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@Months", months);

                    try
                    {
                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void report16(int rating)
        {
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";
            // SQL query to retrieve member details
            string query = @"SELECT TrainerUsername, AVG(TrainerRating) AS AverageRating
                            FROM Feedback
                            GROUP BY TrainerUsername
                            HAVING AVG(TrainerRating) >= @Rating
                            ORDER BY AverageRating DESC;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@Rating", rating);

                    try
                    {
                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void report17(string type)
        {
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";
            // SQL query to retrieve member details
            string query = @"SELECT gm.MemberUsername, gm.MemberName,dp.DietType
                            FROM GymMember AS gm
                            JOIN MemberDietInfo AS mdi ON gm.MemberUsername = mdi.MemberUsername
                            JOIN DietPlan AS dp ON mdi.DietPlanID = dp.DietPlanID
                            WHERE dp.DietType = @Type;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@Type", type);

                    try
                    {
                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void report18(string gym, string trainer)
        {
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";
            // SQL query to retrieve member details
            string query = @"SELECT gm.MemberUsername, gm.MemberName
                            FROM GymMember gm
                            JOIN TrainingSession ts ON gm.MemberUsername = ts.MemberUsername
                            JOIN Trainer_Gym tg ON ts.TrainerUsername = tg.TrainerUsername
                            WHERE tg.GymName = @Gym AND ts.TrainerUsername = @Trainer;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@Gym", gym);
                    command.Parameters.AddWithValue("@Trainer", trainer);

                    try
                    {
                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void report19(string letter)
        {
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";
            // SQL query to retrieve member details
            string query = @"SELECT TrainerUsername, COUNT(*) AS SessionCount
                            FROM TrainingSession
                            WHERE TrainerUsername LIKE @Letter + '%'
                            GROUP BY TrainerUsername;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@Letter", letter);

                    try
                    {
                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void report20(string city)
        {
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";
            // SQL query to retrieve member details
            string query = @"SELECT gm.*
                            FROM GymMember gm
                            JOIN MembershipInfo mi ON gm.MemberUsername = mi.MemberUsername
                            JOIN Gym g ON mi.GymName = g.GymName
                            WHERE g.City LIKE @City;
";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@City", city);

                    try
                    {
                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;

            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "1")
            {

                string gymName = comboBox2.SelectedItem.ToString();
                string trainerUsername = comboBox3.SelectedItem.ToString();


                // Call the DisplayMembersForTrainerInGym function with the provided gymName and trainerUsername
                report1(gymName, trainerUsername);
            }

            
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "2")
            {
                // Get the gym name from textBox1
                string gymName = comboBox2.SelectedItem.ToString();

                // Get the trainer username from textBox2
                string dietName = comboBox3.SelectedItem.ToString();

                // Call the DisplayMembersForTrainerInGym function with the provided gymName and trainerUsername
                report2(gymName, dietName);
            }

            
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "3")
            {
                // Get the gym name from textBox1
                string trainerUsername = comboBox2.SelectedItem.ToString();

                // Get the trainer username from textBox2
                string dietName = comboBox3.SelectedItem.ToString();

                // Call the DisplayMembersForTrainerInGym function with the provided gymName and trainerUsername
                report3(trainerUsername, dietName);
            }

            
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "4")
            {
                // Get the gym name from textBox1
                string day = comboBox3.SelectedItem.ToString();

                // Get the trainer username from textBox2
                string machineName = comboBox2.SelectedItem.ToString();

                string gymName = comboBox4.SelectedItem.ToString();

                // Call the DisplayMembersForTrainerInGym function with the provided gymName and trainerUsername
                report4(day, machineName, gymName);
            }

            
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "5")
            {
                // Get the gym name from textBox1
                string mealTime = comboBox2.SelectedItem.ToString();


                int calories;
                if (!int.TryParse(comboBox3.SelectedItem.ToString(), out calories))
                {
                    
                    MessageBox.Show("Calories must be a valid integer.");
                    return;
                }

                // Call the DisplayMembersForTrainerInGym function with the provided gymName and trainerUsername
                report5(mealTime, calories);
            }

            
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "6")
            {
                // Get the gym name from textBox1
                string nutrition = comboBox2.SelectedItem.ToString();

                int amount;
                if (!int.TryParse(comboBox3.SelectedItem.ToString(), out amount))
                {

                    MessageBox.Show("Weight must be a valid integer.");
                    return;
                }

                // Call the DisplayMembersForTrainerInGym function with the provided gymName and trainerUsername
                report6(nutrition, amount);
            }

            
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "7")
            {
                // Get the gym name from textBox1
                string machineName = comboBox2.SelectedItem.ToString();

                // Call the DisplayMembersForTrainerInGym function with the provided gymName and trainerUsername
                report7(machineName);
            }

            
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "8")
            {
                // Get the gym name from textBox1
                string allergen = comboBox2.SelectedItem.ToString();

                // Call the DisplayMembersForTrainerInGym function with the provided gymName and trainerUsername
                report8(allergen);
            }

            
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "9")
            {
                // Get the gym name from textBox1
                int months;
                if (!int.TryParse(comboBox2.SelectedItem.ToString(), out months))
                {

                    MessageBox.Show("Months must be a valid integer.");
                    return;
                }

                // Call the DisplayMembersForTrainerInGym function with the provided gymName and trainerUsername
                report9(months);
            }
            
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "10")
            {
                // Get the gym name from textBox1
                int months;
                if (!int.TryParse(comboBox2.SelectedItem.ToString(), out months))
                {

                    MessageBox.Show("Months must be a valid integer.");
                    return;
                }

                // Call the DisplayMembersForTrainerInGym function with the provided gymName and trainerUsername
                report10(months);
            }

            
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "11")
            {
                // Get the gym name from textBox1
                string gymName = comboBox2.SelectedItem.ToString();
                string workoutName = comboBox3.SelectedItem.ToString();

                // Call the DisplayMembersForTrainerInGym function with the provided gymName and trainerUsername
                report11(gymName, workoutName);
            }

            
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "12")
            {
                // Get the gym name from textBox1
                string level = comboBox2.SelectedItem.ToString();

                // Call the DisplayMembersForTrainerInGym function with the provided gymName and trainerUsername
                report12(level);
            }

            
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "13")
            {
                // Get the gym name from textBox1
                string muscle = comboBox2.SelectedItem.ToString();

                // Call the DisplayMembersForTrainerInGym function with the provided gymName and trainerUsername
                report13(muscle);
            }

            
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "14")
            {
                // Get the gym name from textBox1
                string package = comboBox2.SelectedItem.ToString();

                // Call the DisplayMembersForTrainerInGym function with the provided gymName and trainerUsername
                report14(package);
            }

            
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "15")
            {
                int months;
                if (!int.TryParse(comboBox2.SelectedItem.ToString(), out months))
                {

                    MessageBox.Show("Months must be a valid integer.");
                    return;
                }

                // Call the DisplayMembersForTrainerInGym function with the provided gymName and trainerUsername
                report15(months); 
            }

            
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "16")
            {
                int rating;
                if (!int.TryParse(comboBox2.SelectedItem.ToString(), out rating))
                {

                    MessageBox.Show("Rating must be a valid integer.");
                    return;
                }

                // Call the DisplayMembersForTrainerInGym function with the provided gymName and trainerUsername
                report16(rating);
            }

            
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "17")
            {
                string type = comboBox2.SelectedItem.ToString();

                // Call the DisplayMembersForTrainerInGym function with the provided gymName and trainerUsername
                report17(type);
            }

            
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "18")
            {
                string gym = comboBox2.SelectedItem.ToString();
                string trainer = comboBox3.SelectedItem.ToString();


                // Call the DisplayMembersForTrainerInGym function with the provided gymName and trainerUsername
                report18(gym, trainer);
            }

            
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "19")
            {
                string letter = comboBox2.SelectedItem.ToString();


                // Call the DisplayMembersForTrainerInGym function with the provided gymName and trainerUsername
                report19(letter);
            }
            
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "20")
            {
                string city = comboBox2.SelectedItem.ToString();


                // Call the DisplayMembersForTrainerInGym function with the provided gymName and trainerUsername
                report20(city);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "1")
            {

                string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";

                // Get the gym name from textBox1
                comboBox2.Visible = true;
                label1.Visible = true;
                label1.Text = "Specify Gym Name:";
                comboBox2.Items.Clear();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SQL command to select the trainer usernames
                    string query = "SELECT GymName FROM Gym";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Execute the command and read the results
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if the reader has rows
                        if (reader.HasRows)
                        {
                            // Iterate through the rows
                            while (reader.Read())
                            {
                                // Get the value of the trainerUsername column for each row
                                string name = reader.GetString(0); // Assuming traUsername is a string column

                                // Add the trainerUsername to the combo box items
                                comboBox2.Items.Add(name);
                            }
                        }
                    }
                }


                // Get the trainer username from textBox2
                comboBox3.Visible = true;
                label2.Visible = true;
                label2.Text = "Specify Trainer Name:";
                comboBox3.Items.Clear();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SQL command to select the trainer usernames
                    string query = "SELECT TrainerUsername FROM GymTrainer";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Execute the command and read the results
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if the reader has rows
                        if (reader.HasRows)
                        {
                            // Iterate through the rows
                            while (reader.Read())
                            {
                                // Get the value of the trainerUsername column for each row
                                string user = reader.GetString(0); // Assuming traUsername is a string column

                                // Add the trainerUsername to the combo box items
                                comboBox3.Items.Add(user);
                            }
                        }
                    }
                }

                label4.Visible = false;
                comboBox4.Visible = false;
            }

            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "2")
            {

                string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";

                // Get the gym name from textBox1
                comboBox2.Visible = true;
                label1.Visible = true;
                label1.Text = "Specify Gym Name:";
                comboBox2.Items.Clear();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SQL command to select the trainer usernames
                    string query = "SELECT GymName FROM Gym";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Execute the command and read the results
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if the reader has rows
                        if (reader.HasRows)
                        {
                            // Iterate through the rows
                            while (reader.Read())
                            {
                                // Get the value of the trainerUsername column for each row
                                string name = reader.GetString(0); // Assuming traUsername is a string column

                                // Add the trainerUsername to the combo box items
                                comboBox2.Items.Add(name);
                            }
                        }
                    }
                }


                // Get the trainer username from textBox2
                comboBox3.Visible = true;
                label2.Visible = true;
                label2.Text = "Specify Diet Name:";
                comboBox3.Items.Clear();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SQL command to select the trainer usernames
                    string query = "SELECT DietName FROM DietPlan";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Execute the command and read the results
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if the reader has rows
                        if (reader.HasRows)
                        {
                            // Iterate through the rows
                            while (reader.Read())
                            {
                                // Get the value of the trainerUsername column for each row
                                string user = reader.GetString(0); // Assuming traUsername is a string column

                                // Add the trainerUsername to the combo box items
                                comboBox3.Items.Add(user);
                            }
                        }
                    }
                }

                label4.Visible = false;
                comboBox4.Visible = false;
            }

            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "3")
            {

                string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";

                // Get the gym name from textBox1
                comboBox2.Visible = true;
                label1.Visible = true;
                label1.Text = "Specify Trainer Username:";
                comboBox2.Items.Clear();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SQL command to select the trainer usernames
                    string query = "SELECT TrainerUsername FROM GymTrainer";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Execute the command and read the results
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if the reader has rows
                        if (reader.HasRows)
                        {
                            // Iterate through the rows
                            while (reader.Read())
                            {
                                // Get the value of the trainerUsername column for each row
                                string name = reader.GetString(0); // Assuming traUsername is a string column

                                // Add the trainerUsername to the combo box items
                                comboBox2.Items.Add(name);
                            }
                        }
                    }
                }


                // Get the trainer username from textBox2
                comboBox3.Visible = true;
                label2.Visible = true;
                label2.Text = "Specify Diet Name:";
                comboBox3.Items.Clear();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SQL command to select the trainer usernames
                    string query = "SELECT DietName FROM DietPlan";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Execute the command and read the results
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if the reader has rows
                        if (reader.HasRows)
                        {
                            // Iterate through the rows
                            while (reader.Read())
                            {
                                // Get the value of the trainerUsername column for each row
                                string user = reader.GetString(0); // Assuming traUsername is a string column

                                // Add the trainerUsername to the combo box items
                                comboBox3.Items.Add(user);
                            }
                        }
                    }
                }

                label4.Visible = false;
                comboBox4.Visible = false;
            }

            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "4")
            {

                string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";

                // Get the gym name from textBox1
                comboBox2.Visible = true;
                label1.Visible = true;
                label1.Text = "Specify Machine name:";
                comboBox2.Items.Clear();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SQL command to select the trainer usernames
                    string query = "SELECT MachineName FROM Machine";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Execute the command and read the results
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if the reader has rows
                        if (reader.HasRows)
                        {
                            // Iterate through the rows
                            while (reader.Read())
                            {
                                // Get the value of the trainerUsername column for each row
                                string name = reader.GetString(0); // Assuming traUsername is a string column

                                // Add the trainerUsername to the combo box items
                                comboBox2.Items.Add(name);
                            }
                        }
                    }
                }


                // Get the trainer username from textBox2
                comboBox3.Visible = true;
                label2.Visible = true;
                label2.Text = "Specify Day:";
                comboBox3.Items.Clear();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SQL command to select the trainer usernames
                    string query = "SELECT DISTINCT(WorkoutDay) FROM WorkoutSchedule";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Execute the command and read the results
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if the reader has rows
                        if (reader.HasRows)
                        {
                            // Iterate through the rows
                            while (reader.Read())
                            {
                                // Get the value of the trainerUsername column for each row
                                string user = reader.GetString(0); // Assuming traUsername is a string column

                                // Add the trainerUsername to the combo box items
                                comboBox3.Items.Add(user);
                            }
                        }
                    }
                }

                label4.Visible = true;
                label4.Text = " Specify Gym name:";

                comboBox4.Visible = true;
                comboBox4.Items.Clear();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SQL command to select the trainer usernames
                    string query = "SELECT GymName FROM Gym";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Execute the command and read the results
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if the reader has rows
                        if (reader.HasRows)
                        {
                            // Iterate through the rows
                            while (reader.Read())
                            {
                                // Get the value of the trainerUsername column for each row
                                string gym = reader.GetString(0); // Assuming traUsername is a string column

                                // Add the trainerUsername to the combo box items
                                comboBox4.Items.Add(gym);
                            }
                        }
                    }
                }
            }

            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "5")
            {

                string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";

                // Get the gym name from textBox1
                comboBox2.Visible = true;
                label1.Visible = true;
                label1.Text = "Specify Meal time:";
                comboBox2.Items.Clear();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SQL command to select the trainer usernames
                    string query = "SELECT DISTINCT(MealTime) FROM DietSchedule";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Execute the command and read the results
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if the reader has rows
                        if (reader.HasRows)
                        {
                            // Iterate through the rows
                            while (reader.Read())
                            {
                                // Get the value of the trainerUsername column for each row
                                string name = reader.GetString(0); // Assuming traUsername is a string column

                                // Add the trainerUsername to the combo box items
                                comboBox2.Items.Add(name);
                            }
                        }
                    }
                }


                // Get the trainer username from textBox2
                comboBox3.Visible = true;
                label2.Visible = true;
                label2.Text = "Specify Max Calories:";
                comboBox3.Items.Clear();

                comboBox3.Items.Add("200");
                comboBox3.Items.Add("300");
                comboBox3.Items.Add("400");
                comboBox3.Items.Add("500");
                comboBox3.Items.Add("600");


                label4.Visible = false;
                comboBox4.Visible = false;
            }

            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "6")
            {

                string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";

                // Get the gym name from textBox1
                comboBox2.Visible = true;
                label1.Visible = true;
                label1.Text = "Specify Nutritional Component:";
                comboBox2.Items.Clear();

                comboBox2.Items.Add("Fat");
                comboBox2.Items.Add("Fiber");
                comboBox2.Items.Add("Carbs");
                comboBox2.Items.Add("Protein");


                // Get the trainer username from textBox2
                comboBox3.Visible = true;
                label2.Visible = true;
                label2.Text = "Specify Max Weight:";
                comboBox3.Items.Clear();

                comboBox3.Items.Add("200");
                comboBox3.Items.Add("300");
                comboBox3.Items.Add("400");
                comboBox3.Items.Add("500");
                comboBox3.Items.Add("600");


                label4.Visible = false;
                comboBox4.Visible = false;
            }

            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "7")
            {
                string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";

                comboBox2.Visible = true;
                label1.Visible = true;
                label1.Text = "Specify Machine name:";
                comboBox2.Items.Clear();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SQL command to select the trainer usernames
                    string query = "SELECT MachineName FROM Machine";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Execute the command and read the results
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if the reader has rows
                        if (reader.HasRows)
                        {
                            // Iterate through the rows
                            while (reader.Read())
                            {
                                // Get the value of the trainerUsername column for each row
                                string name = reader.GetString(0); // Assuming traUsername is a string column

                                // Add the trainerUsername to the combo box items
                                comboBox2.Items.Add(name);
                            }
                        }
                    }
                }

                label2.Visible = false;
                comboBox3.Visible = false;

                label4.Visible = false;
                comboBox4.Visible = false;
            }

            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "8")
            {
                string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";

                comboBox2.Visible = true;
                label1.Visible = true;
                label1.Text = "Specify Allergen:";
                comboBox2.Items.Clear();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SQL command to select the trainer usernames
                    string query = "SELECT DISTINCT(AllergenName) FROM Allergens";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Execute the command and read the results
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if the reader has rows
                        if (reader.HasRows)
                        {
                            // Iterate through the rows
                            while (reader.Read())
                            {
                                // Get the value of the trainerUsername column for each row
                                string name = reader.GetString(0); // Assuming traUsername is a string column

                                // Add the trainerUsername to the combo box items
                                comboBox2.Items.Add(name);
                            }
                        }
                    }
                }

                label2.Visible = false;
                comboBox3.Visible = false;

                label4.Visible = false;
                comboBox4.Visible = false;
            }

            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "9")
            {
                string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";

                comboBox2.Visible = true;
                label1.Visible = true;
                label1.Text = "Specify Months:";
                comboBox2.Items.Clear();


                comboBox2.Items.Add("1");
                comboBox2.Items.Add("2");
                comboBox2.Items.Add("3");
                comboBox2.Items.Add("4");
                comboBox2.Items.Add("5");
                comboBox2.Items.Add("6");
                comboBox2.Items.Add("7");
                comboBox2.Items.Add("8");
                comboBox2.Items.Add("9");
                comboBox2.Items.Add("10");
                comboBox2.Items.Add("11");
                comboBox2.Items.Add("12");


                label2.Visible = false;
                comboBox3.Visible = false;

                label4.Visible = false;
                comboBox4.Visible = false;
            }

            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "10")
            {
                string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";

                comboBox2.Visible = true;
                label1.Visible = true;
                label1.Text = "Specify Months:";
                comboBox2.Items.Clear();


                comboBox2.Items.Add("1");
                comboBox2.Items.Add("2");
                comboBox2.Items.Add("3");
                comboBox2.Items.Add("4");
                comboBox2.Items.Add("5");
                comboBox2.Items.Add("6");
                comboBox2.Items.Add("7");
                comboBox2.Items.Add("8");
                comboBox2.Items.Add("9");
                comboBox2.Items.Add("10");
                comboBox2.Items.Add("11");
                comboBox2.Items.Add("12");


                label2.Visible = false;
                comboBox3.Visible = false;

                label4.Visible = false;
                comboBox4.Visible = false;
            }

            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "11")
            {

                string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";

                // Get the gym name from textBox1
                comboBox2.Visible = true;
                label1.Visible = true;
                label1.Text = "Specify Gym Name:";
                comboBox2.Items.Clear();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SQL command to select the trainer usernames
                    string query = "SELECT GymName FROM Gym";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Execute the command and read the results
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if the reader has rows
                        if (reader.HasRows)
                        {
                            // Iterate through the rows
                            while (reader.Read())
                            {
                                // Get the value of the trainerUsername column for each row
                                string name = reader.GetString(0); // Assuming traUsername is a string column

                                // Add the trainerUsername to the combo box items
                                comboBox2.Items.Add(name);
                            }
                        }
                    }
                }


                // Get the trainer username from textBox2
                comboBox3.Visible = true;
                label2.Visible = true;
                label2.Text = "Specify Workout Name:";
                comboBox3.Items.Clear();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SQL command to select the trainer usernames
                    string query = "SELECT WorkoutName FROM WorkoutPlan";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Execute the command and read the results
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if the reader has rows
                        if (reader.HasRows)
                        {
                            // Iterate through the rows
                            while (reader.Read())
                            {
                                // Get the value of the trainerUsername column for each row
                                string user = reader.GetString(0); // Assuming traUsername is a string column

                                // Add the trainerUsername to the combo box items
                                comboBox3.Items.Add(user);
                            }
                        }
                    }
                }

                label4.Visible = false;
                comboBox4.Visible = false;
            }


            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "12")
            {
                string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";

                comboBox2.Visible = true;
                label1.Visible = true;
                label1.Text = "Specify Experience Level:";
                comboBox2.Items.Clear();


                comboBox2.Items.Add("Beginner");
                comboBox2.Items.Add("Intermediate");
                comboBox2.Items.Add("Advanced");


                label2.Visible = false;
                comboBox3.Visible = false;

                label4.Visible = false;
                comboBox4.Visible = false;
            }

            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "13")
            {
                string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";

                comboBox2.Visible = true;
                label1.Visible = true;
                label1.Text = "Specify Muscle Group:";
                comboBox2.Items.Clear();


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SQL command to select the trainer usernames
                    string query = "SELECT DISTINCT(MuscleGroup) FROM WorkoutSchedule";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Execute the command and read the results
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if the reader has rows
                        if (reader.HasRows)
                        {
                            // Iterate through the rows
                            while (reader.Read())
                            {
                                // Get the value of the trainerUsername column for each row
                                string user = reader.GetString(0); // Assuming traUsername is a string column

                                // Add the trainerUsername to the combo box items
                                comboBox2.Items.Add(user);
                            }
                        }
                    }
                }


                label2.Visible = false;
                comboBox3.Visible = false;

                label4.Visible = false;
                comboBox4.Visible = false;
            }

            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "14")
            {
                string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";

                comboBox2.Visible = true;
                label1.Visible = true;
                label1.Text = "Specify Membership Package:";
                comboBox2.Items.Clear();


                comboBox2.Items.Add("Basic");
                comboBox2.Items.Add("Premium");
                comboBox2.Items.Add("Elite");


                label2.Visible = false;
                comboBox3.Visible = false;

                label4.Visible = false;
                comboBox4.Visible = false;
            }

            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "15")
            {
                string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";

                comboBox2.Visible = true;
                label1.Visible = true;
                label1.Text = "Specify Months:";
                comboBox2.Items.Clear();


                comboBox2.Items.Add("1");
                comboBox2.Items.Add("2");
                comboBox2.Items.Add("3");
                comboBox2.Items.Add("4");
                comboBox2.Items.Add("5");
                comboBox2.Items.Add("6");
                comboBox2.Items.Add("7");
                comboBox2.Items.Add("8");
                comboBox2.Items.Add("9");
                comboBox2.Items.Add("10");
                comboBox2.Items.Add("11");
                comboBox2.Items.Add("12");


                label2.Visible = false;
                comboBox3.Visible = false;

                label4.Visible = false;
                comboBox4.Visible = false;
            }

            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "16")
            {
                string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";

                comboBox2.Visible = true;
                label1.Visible = true;
                label1.Text = "Specify Min Rating:";
                comboBox2.Items.Clear();


                comboBox2.Items.Add("1");
                comboBox2.Items.Add("2");
                comboBox2.Items.Add("3");
                comboBox2.Items.Add("4");
                comboBox2.Items.Add("5");


                label2.Visible = false;
                comboBox3.Visible = false;

                label4.Visible = false;
                comboBox4.Visible = false;
            }

            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "17")
            {
                string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";

                comboBox2.Visible = true;
                label1.Visible = true;
                label1.Text = "Specify Diet Type:";
                comboBox2.Items.Clear();


                comboBox2.Items.Add("Vegan");
                comboBox2.Items.Add("Non-Vegan");


                label2.Visible = false;
                comboBox3.Visible = false;

                label4.Visible = false;
                comboBox4.Visible = false;
            }

            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "18")
            {

                string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";

                // Get the gym name from textBox1
                comboBox2.Visible = true;
                label1.Visible = true;
                label1.Text = "Specify Gym Name:";
                comboBox2.Items.Clear();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SQL command to select the trainer usernames
                    string query = "SELECT GymName FROM Gym";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Execute the command and read the results
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if the reader has rows
                        if (reader.HasRows)
                        {
                            // Iterate through the rows
                            while (reader.Read())
                            {
                                // Get the value of the trainerUsername column for each row
                                string name = reader.GetString(0); // Assuming traUsername is a string column

                                // Add the trainerUsername to the combo box items
                                comboBox2.Items.Add(name);
                            }
                        }
                    }
                }


                // Get the trainer username from textBox2
                comboBox3.Visible = true;
                label2.Visible = true;
                label2.Text = "Specify Trainer username:";
                comboBox3.Items.Clear();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SQL command to select the trainer usernames
                    string query = "SELECT TrainerUsername FROM GymTrainer";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Execute the command and read the results
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if the reader has rows
                        if (reader.HasRows)
                        {
                            // Iterate through the rows
                            while (reader.Read())
                            {
                                // Get the value of the trainerUsername column for each row
                                string user = reader.GetString(0); // Assuming traUsername is a string column

                                // Add the trainerUsername to the combo box items
                                comboBox3.Items.Add(user);
                            }
                        }
                    }
                }

                label4.Visible = false;
                comboBox4.Visible = false;
            }

            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "19")
            {
                string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";

                comboBox2.Visible = true;
                label1.Visible = true;
                label1.Text = "Specify Starting letter of TrainerUsername:";
                comboBox2.Items.Clear();


                comboBox2.Items.Add("a");
                comboBox2.Items.Add("b");
                comboBox2.Items.Add("c");
                comboBox2.Items.Add("d");
                comboBox2.Items.Add("e");
                comboBox2.Items.Add("f");
                comboBox2.Items.Add("g");
                comboBox2.Items.Add("h");
                comboBox2.Items.Add("i");
                comboBox2.Items.Add("j");
                comboBox2.Items.Add("k");
                comboBox2.Items.Add("l");
                comboBox2.Items.Add("m");
                comboBox2.Items.Add("n");
                comboBox2.Items.Add("o");
                comboBox2.Items.Add("p");
                comboBox2.Items.Add("q");
                comboBox2.Items.Add("r");
                comboBox2.Items.Add("s");
                comboBox2.Items.Add("t");
                comboBox2.Items.Add("u");
                comboBox2.Items.Add("v");
                comboBox2.Items.Add("w");
                comboBox2.Items.Add("x");
                comboBox2.Items.Add("y");
                comboBox2.Items.Add("z");


                label2.Visible = false;
                comboBox3.Visible = false;

                label4.Visible = false;
                comboBox4.Visible = false;
            }

            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "20")
            {
                string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";

                comboBox2.Visible = true;
                label1.Visible = true;
                label1.Text = "Specify City name:";
                comboBox2.Items.Clear();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SQL command to select the trainer usernames
                    string query = "SELECT DISTINCT(City) FROM Gym";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Execute the command and read the results
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if the reader has rows
                        if (reader.HasRows)
                        {
                            // Iterate through the rows
                            while (reader.Read())
                            {
                                // Get the value of the trainerUsername column for each row
                                string name = reader.GetString(0); // Assuming traUsername is a string column

                                // Add the trainerUsername to the combo box items
                                comboBox2.Items.Add(name);
                            }
                        }
                    }
                }

                label2.Visible = false;
                comboBox3.Visible = false;

                label4.Visible = false;
                comboBox4.Visible = false;
            }

        }


        private void Form_Default(object sender, EventArgs e)
        {
            comboBox2.Visible = false;
            comboBox3.Visible = false;
            comboBox4.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            label4.Visible = false;
        }
    }
}
