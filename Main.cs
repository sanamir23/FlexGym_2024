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
namespace DBMS_PROJECT
{
    public partial class Main : Form
    {
        public bool login = false;
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // login button
            
            login = true;
            SpecifyRole specifyRole = new SpecifyRole(login);
            specifyRole.ShowDialog();
            string role = specifyRole.selectRole;
            if (role != "")
            {
                Login f2 = new Login(role);
                f2.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // search button

            Search f1 = new Search();
            f1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // sign up button
           
            SpecifyRole f1 = new SpecifyRole();
            f1.ShowDialog();
        }

        
    }
}
