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
    public partial class Feedback : Form
    {
        private string role;
        private string username;
        public Feedback()
        {
            InitializeComponent();
            this.role = "";
            this.username = "";
        }
        public Feedback(string role, string username)
        {
            InitializeComponent();
            this.role = role;
            this.username = username;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            UserForm user = new UserForm(this.role, this.username);
            user.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";
            string query = "";
            string GymName = textBox1.Text;
            string Tname = textBox2.Text;
            int rating = (int)numericUpDown1.Value;


            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    

                    query = "INSERT INTO Feedback (TrainerUsername,MemberUsername,GymName,TrainerRating) " +
                            "VALUES (@TrainerUsername, @MemberUsername,@GymName,@TrainerRating)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters to avoid SQL injection
                        cmd.Parameters.AddWithValue("@TrainerUsername", Tname);
                        cmd.Parameters.AddWithValue("@MemberUsername", this.username);
                        cmd.Parameters.AddWithValue("@GymName", GymName);
                        cmd.Parameters.AddWithValue("@TrainerRating", rating);
                        
                        cmd.ExecuteNonQuery();

                        /*cmd.CommandText = "SELECT IDENT_CURRENT('WorkoutPlan') AS LastWorkoutID";
                        this.WorkoutId = Convert.ToInt32(cmd.ExecuteScalar());*/
                    }
                    MessageBox.Show("Feedback provided successfully");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}
