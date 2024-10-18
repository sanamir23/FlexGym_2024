using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography.Xml;

namespace DBMS_PROJECT
{
    public partial class SignUp : Form
    {
        private string role;
        private string qual;
        private string spec;
        private int yrsExp;
        private string gymName;
        private bool flagOwner;
        private string ownerUsername;

        public SignUp(string role, string qual, string spec, int yrsExp,string gymName)
        {
            InitializeComponent();
            this.role = role;
            this.qual = qual;
            this.spec = spec;
            this.yrsExp = yrsExp;
            this.gymName = gymName;
            this.flagOwner = false;
            this.ownerUsername = "";

        }
        public SignUp(string role, string qual, string spec, int yrsExp, string gymName,bool flag, string username)
        {
            InitializeComponent();
            this.role = role;
            this.qual = qual;
            this.spec = spec;
            this.yrsExp = yrsExp;
            this.gymName = gymName;
            this.flagOwner = flag;
            this.ownerUsername = username;

        }

        public SignUp(string role)
        {
            InitializeComponent();
            this.role = role;
            this.qual = "International Sports Sciences Association (ISSA)";
            this.spec = "Sports-Specific Training";
            this.yrsExp = 3;
            this.gymName = "Absolute Fitness";
            this.flagOwner = false;
            this.ownerUsername = "";
        }

        public SignUp()
        {
            InitializeComponent();
            this.role = "";
            this.qual = "";
            this.spec = "";
            this.yrsExp = 0;
            this.gymName = "";
            this.flagOwner = false;
            this.ownerUsername = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text) ||
                string.IsNullOrWhiteSpace(textBox6.Text) ||
                string.IsNullOrWhiteSpace(textBox7.Text))
            {
                MessageBox.Show("Please fill in all the required information.");
                return; // Stop further execution of the method
            }
            //SpecifyRole s = new SpecifyRole();
            /*string role = s.selectRole;*/ // Ensure selectRole property is properly set

            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";

            string na = textBox1.Text;
            string gen = textBox2.Text;
            int age = int.Parse(textBox3.Text);
            string SSN = textBox4.Text;
            string em = textBox5.Text;
            string user = textBox6.Text;
            string pass = textBox7.Text;

            try
            {
                if (!int.TryParse(textBox3.Text, out age))
                {
                    MessageBox.Show("Please enter a valid age.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (!flagOwner)
                    {
                        MessageBox.Show("Sign-up successful");
                    }
                    else if (flagOwner)
                    {
                        MessageBox.Show("Trainer Hired successfully!");
                    }

                    string query = "";
                    if (role == "Gym Member")
                    {
                        query = "INSERT INTO GymMember (MemberName, MemberUsername, Age, Gender, Email, MemberPassword, SSN) " +
                                "VALUES (@MemberName, @MemberUsername, @Age, @Gender, @Email, @MemberPassword, @SSN)";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            // Add parameters to avoid SQL injection
                            cmd.Parameters.AddWithValue("@MemberName", na);
                            cmd.Parameters.AddWithValue("@MemberUsername", user);
                            cmd.Parameters.AddWithValue("@Age", age);
                            cmd.Parameters.AddWithValue("@Gender", gen);
                            cmd.Parameters.AddWithValue("@Email", em);
                            cmd.Parameters.AddWithValue("@MemberPassword", pass);
                            cmd.Parameters.AddWithValue("@SSN", SSN);

                            cmd.ExecuteNonQuery();
                        }

                        this.Close();
                        Membership membership = new Membership(user);
                        membership.ShowDialog();
                    }

                    else if (role == "Gym Trainer")
                    {

                        query = "INSERT INTO GymTrainer (TrainerName, TrainerUsername, Age, Gender, Email, TrainerPassword, SSN, YearsExp, Qualification, Speciality) " +
                                "VALUES (@TrainerName, @TrainerUsername, @Age, @Gender, @Email, @TrainerPassword, @SSN, @YearsExp, @Qualification, @Speciality)";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            // Add parameters to avoid SQL injection
                            cmd.Parameters.AddWithValue("@TrainerName", na);
                            cmd.Parameters.AddWithValue("@TrainerUsername", user);
                            cmd.Parameters.AddWithValue("@Age", age);
                            cmd.Parameters.AddWithValue("@Gender", gen);
                            cmd.Parameters.AddWithValue("@Email", em);
                            cmd.Parameters.AddWithValue("@TrainerPassword", pass);
                            cmd.Parameters.AddWithValue("@SSN", SSN);
                            cmd.Parameters.AddWithValue("@YearsExp", this.yrsExp);
                            cmd.Parameters.AddWithValue("@Qualification", this.qual);
                            cmd.Parameters.AddWithValue("@Speciality", this.spec);

                            cmd.ExecuteNonQuery();
                        }

                        query = "INSERT INTO Trainer_Gym (TrainerUsername,GymName,HireDate) " +
                                "VALUES (@TrainerUsername,@GymName,GETDATE())";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@TrainerUsername", user);
                            cmd.Parameters.AddWithValue("@GymName", this.gymName);

                            cmd.ExecuteNonQuery();
                        }
                        if (!flagOwner)
                        {
                            this.Close();
                            SignUpSuccess s1 = new SignUpSuccess();
                            s1.Show();
                        }
                        else
                        {
                            this.Close();
                            OwnerForm owner = new OwnerForm(this.ownerUsername);
                            owner.Show();
                        }
                    }

                    else if (role == "Gym Owner")
                    {
                        query = "INSERT INTO GymOwner (OwnerName, OwnerUsername, Age, Gender, Email, OwnerPassword, SSN) " +
                                "VALUES (@OwnerName, @OwnerUsername, @Age, @Gender, @Email, @OwnerPassword, @SSN)";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            // Add parameters to avoid SQL injection
                            cmd.Parameters.AddWithValue("@OwnerName", na);
                            cmd.Parameters.AddWithValue("@OwnerUsername", user);
                            cmd.Parameters.AddWithValue("@Age", age);
                            cmd.Parameters.AddWithValue("@Gender", gen);
                            cmd.Parameters.AddWithValue("@Email", em);
                            cmd.Parameters.AddWithValue("@OwnerPassword", pass);
                            cmd.Parameters.AddWithValue("@SSN", SSN);

                            cmd.ExecuteNonQuery();
                        }
                        this.Close();
                        SignUpSuccess s2 = new SignUpSuccess();
                        s2.ShowDialog();
                    }
                }

                this.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("A SQL error occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}
