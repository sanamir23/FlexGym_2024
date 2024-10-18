using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBMS_PROJECT
{
    public partial class UserForm : Form
    {
        private string role;
        private string username;
        public UserForm()
        {
            InitializeComponent();
            this.role = "";
        }
        public UserForm(string role,string username)
        {
            InitializeComponent();
            this.role = role;
            this.username = username;
            if (role == "Gym Member")
            {
                button4.Text = "Give Feedback";
            }
            else
                button4.Text = "View Feedback";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            WorkoutPlan workoutPlan = new WorkoutPlan(role,username);
            workoutPlan.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            DietPlan dietPlan = new DietPlan(role,username);
            dietPlan.ShowDialog();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
            Session session = new Session(this.role,this.username);
            session.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            if (role == "Gym Member")
            {
                Feedback feedback = new Feedback(this.role, this.username);
                feedback.Show();
            }
            else if(role == "Gym Trainer")
            {
                TrainerFeedback feedback = new TrainerFeedback(this.username,this.role);
                feedback.Show();
            }

        }
    }
}
