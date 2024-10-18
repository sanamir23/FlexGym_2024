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

namespace DBMS_PROJECT
{
    public partial class DietPlanCreate : Form
    {
        private string role;
        private string username;
        private int DietID;
        private string name;
        private string purpose;
        private string type;
        private string time;
        private string day;
        private bool flag;
        public DietPlanCreate()
        {
            InitializeComponent();
            this.role = "";
            username = "";
            DietID = 0;
            name = "";
            purpose = "";
            type = "";
            time = "";
            day = "";
            flag = false;
        }
        public DietPlanCreate(string role, string username)
        {
            InitializeComponent();
            this.role = role;
            this.username = username;
            DietID = 0;
            name = "";
            purpose = "";
            type = "";
            time = "";
            day = "";
            flag = false;

        }
        public void InsertDiet()
        {
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";
            string query = "";

            this.name = textBox1.Text;
            this.purpose = comboBox1.SelectedItem?.ToString();
            this.type = comboBox2.SelectedItem?.ToString();
            // int count = 0;

            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    //MessageBox.Show("Sign-up successful");

                    query = "INSERT INTO DietPlan (DietName, CreatedBy,CreatorUsername, Purpose,DietType) " +
                                "VALUES (@DietName, @CreatedBy,@CreatorUsername ,@Purpose,@DietType)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters to avoid SQL injection
                        cmd.Parameters.AddWithValue("@DietName", this.name);
                        cmd.Parameters.AddWithValue("@CreatedBy", this.role);
                        cmd.Parameters.AddWithValue("@CreatorUsername", this.username);
                        cmd.Parameters.AddWithValue("@Purpose", this.purpose);
                        cmd.Parameters.AddWithValue("@DietType", this.type);
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "SELECT IDENT_CURRENT('DietPlan') AS LastDietID";
                        this.DietID = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    if (this.role == "Gym Member")
                    {

                        query = "INSERT INTO MemberDietInfo (MemberUsername,DietPlanID) " +
                                    "VALUES (@MemberUsername, @DietPlanID)";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            // Add parameters to avoid SQL injection
                            cmd.Parameters.AddWithValue("@MemberUsername", this.username);

                            cmd.Parameters.AddWithValue("@DietPlanID", this.DietID);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        query = "INSERT INTO TrainerDietInfo (TrainerUsername,DietPlanID) " +
                                    "VALUES (@TrainerUsername, @DietPlanID)";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            // Add parameters to avoid SQL injection
                            cmd.Parameters.AddWithValue("@TrainerUsername", this.username);

                            cmd.Parameters.AddWithValue("@DietPlanID", this.DietID);

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
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && comboBox3.SelectedItem != null)
            {
                if (!flag)
                {
                    InsertDiet();
                    flag = true;
                }
                this.day = "Monday";
                this.time = comboBox3.SelectedItem?.ToString();
                Meal meal = new Meal(this.DietID, this.day, this.time);
                meal.ShowDialog();

            }
            else
            {
                MessageBox.Show("Please fill in all the required fields (Diet Name, Diet Purpose, Diet Type, Time).");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && comboBox4.SelectedItem != null)
            {
                if (!flag)
                {
                    InsertDiet();
                    flag = true;
                }
                this.day = "Tuesday";
                this.time = comboBox4.SelectedItem?.ToString();
                Meal meal = new Meal(this.DietID, this.day, this.time);
                meal.ShowDialog();

            }
            else
            {
                MessageBox.Show("Please fill in all the required fields (Diet Name, Diet Purpose, Diet Type, Time).");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && comboBox5.SelectedItem != null)
            {
                if (!flag)
                {
                    InsertDiet();
                    flag = true;
                }
                this.day = "Wednesday";
                this.time = comboBox5.SelectedItem?.ToString();
                Meal meal = new Meal(this.DietID, this.day, this.time);
                meal.ShowDialog();

            }
            else
            {
                MessageBox.Show("Please fill in all the required fields (Diet Name, Diet Purpose, Diet Type, Time).");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && comboBox6.SelectedItem != null)
            {
                if (!flag)
                {
                    InsertDiet();
                    flag = true;
                }
                this.day = "Thursday";
                this.time = comboBox6.SelectedItem?.ToString();
                Meal meal = new Meal(this.DietID, this.day, this.time);
                meal.ShowDialog();

            }
            else
            {
                MessageBox.Show("Please fill in all the required fields (Diet Name, Diet Purpose, Diet Type, Time).");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && comboBox7.SelectedItem != null)
            {
                if (!flag)
                {
                    InsertDiet();
                    flag = true;
                }
                this.day = "Friday";
                this.time = comboBox7.SelectedItem?.ToString();
                Meal meal = new Meal(this.DietID, this.day, this.time);
                meal.ShowDialog();

            }
            else
            {
                MessageBox.Show("Please fill in all the required fields (Diet Name, Diet Purpose, Diet Type, Time).");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && comboBox8.SelectedItem != null)
            {
                if (!flag)
                {
                    InsertDiet();
                    flag = true;
                }
                this.day = "Saturday";
                this.time = comboBox8.SelectedItem?.ToString();
                Meal meal = new Meal(this.DietID, this.day, this.time);
                meal.ShowDialog();

            }
            else
            {
                MessageBox.Show("Please fill in all the required fields (Diet Name, Diet Purpose, Diet Type, Time).");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
