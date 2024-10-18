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
    public partial class SignUpSuccess : Form
    {
        public SignUpSuccess()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            SpecifyRole specifyRole = new SpecifyRole(true);
            specifyRole.ShowDialog();
            string role = specifyRole.selectRole;
            Login f2 = new Login(role);
            f2.ShowDialog();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
