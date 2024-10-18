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
    public partial class Session : Form
    {
        private string role;
        private string username;
        public Session()
        {
            InitializeComponent();
            this.role = "";
            this.username = "";

        }
        public Session(string role, string username)
        {
            InitializeComponent();
            this.role = role;
            this.username = username;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ScheduleSession session = new ScheduleSession(this.role, this.username);
            session.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            UserForm user = new UserForm(role, username);
            user.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            EditSession edit = new EditSession(this.role, this.username);
            edit.Show();
        }
    }
}
