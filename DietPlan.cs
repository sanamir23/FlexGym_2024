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
    public partial class DietPlan : Form
    {
        private string role;
        private string username;
        private string plan;
        public DietPlan()
        {
            InitializeComponent();
            this.role = "";
            this.username = "";
            this.plan = "";
        }
        public DietPlan(string role, string username)
        {
            InitializeComponent();
            this.role = role;
            this.username = username;
            this.plan = "Diet";
        }




        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            DietPlanCreate dietPlanCreate = new DietPlanCreate(role, username);
            dietPlanCreate.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
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
