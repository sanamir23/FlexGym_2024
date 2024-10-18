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

    public partial class GymForm : Form
    {
        private string username;
        public GymForm()
        {
            InitializeComponent();
            this.username = "";
        }
        public GymForm(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";
            string query = "";

            string gymName = textBox1.Text;
            string AdminUserName = textBox2.Text;
            string city = textBox8.Text;
            string street = textBox5.Text;
            string buildingNo = textBox4.Text;
            string Sector = textBox6.Text;
            int activeMember = int.Parse(textBox7.Text);
            int satisfaction = int.Parse(textBox9.Text);
            int attendanceRate = int.Parse(textBox11.Text);
            int rating = (int)numericUpDown2.Value;


            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();


                    query = "INSERT INTO Gym (GymName,City,Street,BuildingNo,Sector,ActiveMembers,Rating,Attendance,Satisfaction,OwnerUsername,AdminUsername) " +
                            "VALUES (@GymName, @City,@Street,@BuildingNo,@Sector,@ActiveMembers,@Rating,@Attendance,@Satisfaction,@OwnerUsername,@AdminUsername)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters to avoid SQL injection
                        cmd.Parameters.AddWithValue("@GymName", gymName);
                        cmd.Parameters.AddWithValue("@City", city);
                        if (!string.IsNullOrEmpty(street))
                        {
                            cmd.Parameters.AddWithValue("@Street", street);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Street", DBNull.Value);
                        }
                        if (!string.IsNullOrEmpty(buildingNo))
                        {
                            cmd.Parameters.AddWithValue("@BuildingNo", buildingNo);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@BuildingNo", DBNull.Value);
                        }
                        if (!string.IsNullOrEmpty(Sector))
                        {
                            cmd.Parameters.AddWithValue("@Sector", Sector);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Sector", DBNull.Value);
                        }
                        cmd.Parameters.AddWithValue("@ActiveMembers", activeMember);
                        cmd.Parameters.AddWithValue("@Rating", rating);
                        cmd.Parameters.AddWithValue("@Attendance", attendanceRate);
                        cmd.Parameters.AddWithValue("@Satisfaction", satisfaction);
                        cmd.Parameters.AddWithValue("@OwnerUsername", this.username);
                        cmd.Parameters.AddWithValue("@AdminUsername", AdminUserName);

                        cmd.ExecuteNonQuery();

                        /*cmd.CommandText = "SELECT IDENT_CURRENT('WorkoutPlan') AS LastWorkoutID";
                        this.WorkoutId = Convert.ToInt32(cmd.ExecuteScalar());*/
                    }
                    MessageBox.Show("Gym Registration request sent to Admin successfully!");
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
            OwnerForm owner = new OwnerForm(this.username);
            owner.Show();
        }
    }
}
