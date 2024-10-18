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
    public partial class TrainerInfo : Form
    {
        public string qual { get; private set; }
        public int yrsExp { get; private set; }
        public string spec { get; private set; }
        public string gymName { get; private set; }

        public TrainerInfo()
        {
            InitializeComponent();
            qual = "International Sports Sciences Association (ISSA)";
            spec = "Sports-Specific Training";
            yrsExp = 3;
            gymName = "xyz";
        }

        public void setAttributes()
        {
            qual = comboBox1.SelectedItem?.ToString();
            spec = comboBox2.SelectedItem?.ToString();
            yrsExp = (int)numericUpDown1.Value;
            gymName = textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text) || string.IsNullOrEmpty(comboBox2.Text) || string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Kindly enter all the details before continuing");
                return;
            }
            else
            {
                this.Close();
            }
        }

        
    }
}
