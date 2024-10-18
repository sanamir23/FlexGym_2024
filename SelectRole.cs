using DBMS_PROJECT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace DBMS_PROJECT
{
    public partial class SpecifyRole : Form
    {
        public string selectRole = "";
        public bool checkLogin = false;

        public SpecifyRole()
        {
            InitializeComponent();
        }

        public SpecifyRole(bool checkLogin)
        {
            
            InitializeComponent();
            this.checkLogin = checkLogin;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool checkTrainer = false;
            //
            string attr1 = "";
            string attr2 = "";
            int attr3 = 0;
            string attr4 = "";

            int checkCount = 0;

            //continue button
            //show info form for specific user i.e trainer/member etc
            if (checkBox1.Checked)
            {
                //Gym Member

                selectRole = checkBox1.Text;
                checkCount++;
            }
            if (checkBox2.Checked)
            {
                //Gym Owner
                selectRole = checkBox2.Text;
                checkCount++;
            }
            if (checkBox3.Checked)
            {
                //Gym Trainer

                selectRole = checkBox3.Text;
                checkCount++;
                checkTrainer = true;
            }
            if(checkBox4.Checked)
            {
                // Admin
                selectRole = checkBox4.Text;
                checkCount++;
            }
           

            //checking if more than one checkBox is selected

            if (checkCount > 1)
            {
                MessageBox.Show("Please select exactly one role.");
                return;
            }

            if (checkLogin == false && checkTrainer == true)
            {

                TrainerInfo trainerInfoForm = new TrainerInfo();
                trainerInfoForm.ShowDialog(); //show TrainerInfo form as a dialog

                trainerInfoForm.setAttributes();

                attr1 = trainerInfoForm.qual;
                attr2 = trainerInfoForm.spec;
                attr3 = trainerInfoForm.yrsExp;
                attr4 = trainerInfoForm.gymName;
            }
            if (!string.IsNullOrEmpty(selectRole))
            {
                if (checkLogin == false)
                {
                    if (selectRole == "Gym Member" || selectRole == "Gym Owner")
                    {
                        this.Close();
                        SignUp signUpForm = new SignUp(selectRole);
                        signUpForm.Show();
                    }
                    else if(selectRole == "Admin")
                    {
                        // Admin can simply Login
                        this.Close();
                        Login loginForm = new Login(selectRole);
                        loginForm.Show();
                    }
                    else
                    {
                        // trainer
                        this.Close();
                        SignUp signUpForm = new SignUp(selectRole, attr1, attr2, attr3, attr4);
                        signUpForm.Show();
                    }
                }
                else
                {
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Please select a role.");
                
            }

            
        }
    }
}
