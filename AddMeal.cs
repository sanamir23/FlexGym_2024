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

namespace DBMS_PROJECT
{
    public partial class Meal : Form
    {
        private int DietID;
        private int MealID;
        private string day;
        private string Mealtime;
        private bool flag;
        public Meal()
        {
            InitializeComponent();
            this.DietID = 0;
            this.MealID = 0;
            this.day = "";
            this.Mealtime = "";
            flag = false;
        }
        public Meal(int ID, string day, string time)
        {
            InitializeComponent();
            this.DietID = ID;
            this.day = day;
            this.Mealtime = time;
            flag = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";
            string query = "";
            //int count = 0;

            string Mname = textBox1.Text;
            int calories = int.Parse(textBox2.Text);
            int fats = int.Parse(textBox3.Text);
            int protein = int.Parse(textBox4.Text);
            int fiber = int.Parse(textBox5.Text);
            int carbs = int.Parse(textBox6.Text);

            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    MessageBox.Show("Diet Schedule successful");

                    query = "INSERT INTO Meal (MealName, Calories, Fat, Fiber,Carbs,Protein,MealTime)" +
                              "VALUES (@MealName, @Calories, @Fat, @Fiber,@Carbs,@Protein,@MealTime)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters to avoid SQL injection
                        cmd.Parameters.AddWithValue("@MealName", Mname);
                        cmd.Parameters.AddWithValue("@Calories", calories);
                        cmd.Parameters.AddWithValue("@Fat", fats);
                        cmd.Parameters.AddWithValue("@Fiber", fiber);
                        cmd.Parameters.AddWithValue("@Carbs", carbs);
                        cmd.Parameters.AddWithValue("@Protein", protein);
                        cmd.Parameters.AddWithValue("@MealTime", this.Mealtime);
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "SELECT IDENT_CURRENT('Meal') AS LastMealID";
                        this.MealID = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    query = "INSERT INTO DietSchedule (DietPlanID, DietDay, MealID, MealTime)" +
                               "VALUES (@DietPlanID, @DietDay, @MealID, @MealTime)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters to avoid SQL injection
                        cmd.Parameters.AddWithValue("@DietPlanID", this.DietID);
                        cmd.Parameters.AddWithValue("@DietDay", this.day);
                        cmd.Parameters.AddWithValue("@MealID", this.MealID);
                        cmd.Parameters.AddWithValue("@MealTime", this.Mealtime);
                        cmd.ExecuteNonQuery();
                    }

                    foreach (Control control in this.Controls)
                    {
                        if (control is CheckBox checkBox && checkBox.Checked)
                        {
                            string allergenName = checkBox.Text;

                            query = "INSERT INTO Allergens (AllergenName, MealID)"
                                + "VALUES (@AllergenName, @MealID)";

                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@AllergenName", allergenName);
                                cmd.Parameters.AddWithValue("@MealID", this.MealID);
                                cmd.ExecuteNonQuery();
                            }


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            } 
            flag = true;  // meal has been inserted

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
