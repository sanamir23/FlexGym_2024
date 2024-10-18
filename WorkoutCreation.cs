using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DBMS_PROJECT
{
    public partial class WorkoutCreation : Form
    {
        private string role;
        private string username;
        private string name;
        private string goal;
        private string experience;
        private string day;
        private string time;
        private int WorkoutId;
        private bool flag;



        public WorkoutCreation()
        {
            InitializeComponent();
            role = "";
            username = "";
            name = "";
            goal = "";
            experience = "";
            day = "";
            flag = false;

        }
        public WorkoutCreation(string role, string username)
        {
            InitializeComponent();
            this.role = role;
            this.username = username;
            name = "";
            goal = "";
            experience = "";
            day = "";
            flag = false;
        }
        public void InsertWorkout()
        {
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";
            string query = "";

            name = textBox1.Text;
            goal = comboBox1.SelectedItem?.ToString();
            experience = comboBox4.SelectedItem?.ToString();
            // int count = 0;

            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    //MessageBox.Show("Sign-up successful");

                    query = "INSERT INTO WorkoutPlan (WorkoutName, CreatedBy,CreatorUsername,ExperienceLevel,Goal) " +
                                "VALUES (@WorkoutName, @CreatedBy,@CreatorUsername,@ExperienceLevel,@Goal)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters to avoid SQL injection
                        cmd.Parameters.AddWithValue("@WorkoutName", name);
                        cmd.Parameters.AddWithValue("@CreatedBy", this.role);
                        cmd.Parameters.AddWithValue("@CreatorUsername", this.username);
                        cmd.Parameters.AddWithValue("@ExperienceLevel", experience);
                        cmd.Parameters.AddWithValue("@Goal", goal);
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "SELECT IDENT_CURRENT('WorkoutPlan') AS LastWorkoutID";
                        this.WorkoutId = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    if (this.role == "Gym Member")
                    {

                        query = "INSERT INTO MemberWorkoutInfo (MemberUsername, WorkoutID) " +
                                    "VALUES (@MemberUsername, @WorkoutID)";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            // Add parameters to avoid SQL injection
                            cmd.Parameters.AddWithValue("@MemberUsername", this.username);
                            cmd.Parameters.AddWithValue("@WorkoutID", this.WorkoutId);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        query = "INSERT INTO TrainerWorokoutInfo (TrainerUsername, WorkoutID) " +
                                    "VALUES (@TrainerUsername, @WorkoutID,@DietPlanID)";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            // Add parameters to avoid SQL injection
                            cmd.Parameters.AddWithValue("@TrainerUsername", this.username);
                            cmd.Parameters.AddWithValue("@WorkoutID", this.WorkoutId);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && comboBox1.SelectedItem != null && comboBox4.SelectedItem != null)
            {
                if (!flag)
                {
                    InsertWorkout();
                    flag = true;
                }

                DateTime selectedDate = dateTimePicker1.Value;
                this.time = selectedDate.ToString("HH:mm:ss");
                this.day = "Monday";

                Exercise exercise = new Exercise(this.WorkoutId, this.day, this.time);
                exercise.Show();
            }
            else
            {
                MessageBox.Show("Please fill in all the required fields (Workout Name, Goal, Experience Level).");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && comboBox1.SelectedItem != null && comboBox4.SelectedItem != null)
            {
                if (!flag)
                {
                    InsertWorkout();
                    flag = true;
                }

                DateTime selectedDate = dateTimePicker2.Value;
                this.time = selectedDate.ToString("HH:mm:ss");
                this.day = "Tuesday";

                Exercise exercise = new Exercise(this.WorkoutId, this.day, this.time);
                exercise.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please fill in all the required fields (Workout Name, Goal, Experience Level).");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && comboBox1.SelectedItem != null && comboBox4.SelectedItem != null)
            {
                if (!flag)
                {
                    InsertWorkout();
                    flag = true;
                }

                DateTime selectedDate = dateTimePicker3.Value;
                this.time = selectedDate.ToString("HH:mm:ss");
                this.day = "Wednesday";

                Exercise exercise = new Exercise(this.WorkoutId, this.day, this.time);
                exercise.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please fill in all the required fields (Workout Name, Goal, Experience Level).");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && comboBox1.SelectedItem != null && comboBox4.SelectedItem != null)
            {
                if (!flag)
                {
                    InsertWorkout();
                    flag = true;
                }

                DateTime selectedDate = dateTimePicker4.Value;
                this.time = selectedDate.ToString("HH:mm:ss");
                this.day = "Thursday";

                Exercise exercise = new Exercise(this.WorkoutId, this.day, this.time);
                exercise.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please fill in all the required fields (Workout Name, Goal, Experience Level).");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && comboBox1.SelectedItem != null && comboBox4.SelectedItem != null)
            {
                if (!flag)
                {
                    InsertWorkout();
                    flag = true;
                }

                DateTime selectedDate = dateTimePicker5.Value;
                this.time = selectedDate.ToString("HH:mm:ss");
                this.day = "Friday";
                ;
                Exercise exercise = new Exercise(this.WorkoutId, this.day, this.time);
                exercise.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please fill in all the required fields (Workout Name, Goal, Experience Level).");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && comboBox1.SelectedItem != null && comboBox4.SelectedItem != null)
            {
                if (!flag)
                {
                    InsertWorkout();
                    flag = true;
                }

                DateTime selectedDate = dateTimePicker6.Value;
                this.time = selectedDate.ToString("HH:mm:ss");
                this.day = "Saturday";

                Exercise exercise = new Exercise(this.WorkoutId, this.day, this.time);
                exercise.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please fill in all the required fields (Workout Name, Goal, Experience Level).");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
            WorkoutPlan workout = new WorkoutPlan(this.role, this.username);
            workout.Show();
        }
    }
}
