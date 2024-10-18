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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.VisualBasic.ApplicationServices;
using System.Runtime.Intrinsics.X86;
namespace DBMS_PROJECT
{

    public partial class Membership : Form
    {
        public string username { get; private set; }
        public Membership()
        {
            InitializeComponent();
            this.username = "";
        }
        public Membership(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=BUDDY\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;";

            string GName = textBox1.Text;
            string TName = textBox2.Text;
            string package = comboBox1.SelectedItem?.ToString();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Query to select the membershipID based on gymName and package
                    string selectMembershipIDQuery = "SELECT MembershipID FROM Membership WHERE GymName = @GymName AND MembershipType = @Package";

                    using (SqlCommand selectCmd = new SqlCommand(selectMembershipIDQuery, conn))
                    {
                        // Add parameters to the select command
                        selectCmd.Parameters.AddWithValue("@GymName", GName);
                        selectCmd.Parameters.AddWithValue("@Package", package);

                        // Execute the select command
                        object result = selectCmd.ExecuteScalar();

                        // Check if the result is not null
                        if (result != null)
                        {
                            // Convert the result to the appropriate data type (assuming membershipID is an int)
                            int membershipID = Convert.ToInt32(result);

                            // Insert into MembershipInfo table
                            string insertQuery = "INSERT INTO MembershipInfo (MemberUsername, GymName, MembershipID, MemberSince, TrainerUsername) " +
                                                 "VALUES (@MemberUsername, @GymName, @MembershipID, GETDATE(), @TrainerUsername)";

                            using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                            {
                                // Add parameters to avoid SQL injection
                                insertCmd.Parameters.AddWithValue("@MemberUsername", this.username);
                                insertCmd.Parameters.AddWithValue("@GymName", GName);
                                insertCmd.Parameters.AddWithValue("@MembershipID", membershipID);
                                insertCmd.Parameters.AddWithValue("@TrainerUsername", TName);
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // Handle the case where no membershipID is found for the provided criteria
                            MessageBox.Show("No membershipID found for the selected gym and package.");
                        }
                    }
                }
                this.Close();
                SignUpSuccess signUpSuccess = new SignUpSuccess();
                signUpSuccess.Show();
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
