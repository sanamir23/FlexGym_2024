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
using System.Data.SqlClient;
namespace DBMS_PROJECT
{
    public partial class Login : Form
    {
        private string selectRole;
        private string username;
        public Login()
        {
            selectRole = "";
            username = "";
            InitializeComponent();
        }

        public Login(string selectRole)
        {
            this.selectRole = selectRole;
            username = "";
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //verify before closing form
            this.username = textBox1.Text;
            string user = textBox1.Text;
            string pass = textBox2.Text;

            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";
            string query = "";

            switch (selectRole)
            {
                case "Gym Member":
                    query = "SELECT COUNT(*) FROM GymMember WHERE MemberUsername = @Username AND MemberPassword = @Password";
                    break;
                case "Gym Owner":
                    query = "SELECT COUNT(*) FROM GymOwner WHERE OwnerUsername = @Username AND OwnerPassword = @Password";
                    break;
                case "Gym Trainer":
                    query = "SELECT COUNT(*) FROM GymTrainer WHERE TrainerUsername = @Username AND TrainerPassword = @Password";
                    break;
                case "Admin":
                    query = "SELECT COUNT(*) FROM Admin WHERE AdminUsername = @Username AND AdminPassword = @Password";
                    break;
                default:
                    MessageBox.Show("Invalid role!");
                    return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@Username", user);
                        command.Parameters.AddWithValue("@Password", pass);

                        int count = (int)command.ExecuteScalar();

                        //check if username and password combination exists
                        if (count > 0)
                        {
                            MessageBox.Show("Login successfull!");

                            Form form;
                            switch (selectRole)
                            {
                                case "Gym Member":
                                case "Gym Trainer":
                                    form = new UserForm(selectRole, username);
                                    break;
                                case "Gym Owner":
                                    form = new OwnerForm(this.username);
                                    break;
                                case "Admin":
                                    form = new AdminForm(this.username);
                                    break;
                                default:
                                    MessageBox.Show("Invalid role!");
                                    return;
                            }

                            // Show the form

                            this.Close();
                            form.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password.");
                            //this.Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

            //this.Close(); 
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
