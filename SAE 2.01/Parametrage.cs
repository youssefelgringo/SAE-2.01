using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAE_2._01
{
    public partial class Parametrage : Form
    {
        public Parametrage()
        {
            InitializeComponent();
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            if(radioButton2.Checked == true) {
                textBox2.Enabled = false;
            }
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                textBox2.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 jeuPrincipal = new Form2();

            jeuPrincipal.Show();

            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == "Rouge")
                comboBox2.SelectedItem = "Jaune";

            if (comboBox1.SelectedItem == "Jaune")
                comboBox2.SelectedItem = "Rouge";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == "Rouge")
                comboBox1.SelectedItem = "Jaune";

            if (comboBox2.SelectedItem == "Jaune")
                comboBox1.SelectedItem = "Rouge";
        }
    }
}
