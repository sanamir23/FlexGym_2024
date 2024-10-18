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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace DBMS_PROJECT
{
    public partial class ScheduleSession : Form
    {
        private string role;
        private string username;
        public ScheduleSession()
        {
            InitializeComponent();
            this.role = "";
            this.username = "";
        }
        public ScheduleSession(string role, string username)
        {
            InitializeComponent();
            this.role = role;
            this.username = username;
            if (this.role == "Gym Trainer")
            {
                label14.Text = "Enter Member Username";
            }
            else
            {
                label14.Text = "Enter Trainer Username";
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";
            string query = "";

            string name = textBox2.Text;
            string selectedDate = dateTimePicker1.Value.ToString("dddd, dd/MM/yyyy");
            string startTime = dateTimePicker2.Value.ToString("HH:mm:ss");
            string endTime = dateTimePicker3.Value.ToString("HH:mm:ss");


            // int count = 0;

            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    MessageBox.Show("Training Session Created successfullly");

                    query = "INSERT INTO TrainingSession (SessionDate, StartingTime,EndingTime,TrainerUsername,MemberUsername) " +
                                "VALUES (@SessionDate, @StartingTime,@EndingTime,@TrainerUsername,@MemberUsername)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters to avoid SQL injection
                        cmd.Parameters.AddWithValue("@SessionDate", selectedDate);
                        cmd.Parameters.AddWithValue("@StartingTime", startTime);
                        cmd.Parameters.AddWithValue("@EndingTime", endTime);
                        if (role == "Gym Member")
                        {
                            cmd.Parameters.AddWithValue("@TrainerUsername", name);
                            cmd.Parameters.AddWithValue("@MemberUsername", this.username);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@TrainerUsername", this.username);
                            cmd.Parameters.AddWithValue("@MemberUsername", name);
                        }

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Training Session Created successfullly");
                        }

                        /* cmd.CommandText = "SELECT IDENT_CURRENT('WorkoutPlan') AS LastWorkoutID";
                         this.WorkoutId = Convert.ToInt32(cmd.ExecuteScalar());*/
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
        }
    }
}
