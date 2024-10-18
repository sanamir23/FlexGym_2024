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
    public partial class OwnerForm : Form
    {
        private string username;
        public OwnerForm()
        {
            InitializeComponent();
            this.username = "";
        }
        public OwnerForm(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            GymForm gym = new GymForm(this.username);
            gym.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            YourMember member = new YourMember(this.username);
            member.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            YourTrainer trainer = new YourTrainer(this.username);
            trainer.Show();

        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            TrainerInfo trainerInfoForm = new TrainerInfo();
            trainerInfoForm.ShowDialog(); //show TrainerInfo form as a dialog

            trainerInfoForm.setAttributes();

            string attr1 = trainerInfoForm.qual;
            string attr2 = trainerInfoForm.spec;
            int attr3 = trainerInfoForm.yrsExp;
            string attr4 = trainerInfoForm.gymName;
            SignUp signUp = new SignUp("Gym Trainer", attr1, attr2, attr3, attr4, true, this.username);
            signUp.Show();


        }
    }
}
