using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBMS_PROJECT
{
    public partial class WorkoutPlan : Form
    {
        private string role;
        private string username;
        private string plan;
       
        public WorkoutPlan()
        {
            InitializeComponent();
            this.role = "";
            this.username = "";
            this.plan = "";
        }
        public WorkoutPlan(string role, string username)
        {
            InitializeComponent();
            this.role = role;
            this.username = username;
            this.plan = "Workout";

        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            WorkoutCreation workout = new WorkoutCreation(role, username);
            workout.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            UserForm userForm = new UserForm(role, username);
            userForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            BrowseWorkoutDiet obj = new BrowseWorkoutDiet(username,role,this.plan);
            obj.Show();
        }
    }
}
