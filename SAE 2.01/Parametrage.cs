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
        public static int nbColonnes = 0;
        public static int nbLignes = 0;
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

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            nbColonnes = Convert.ToInt32(numericUpDown2.Value);
            Console.WriteLine(nbColonnes);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            nbLignes = Convert.ToInt32(numericUpDown1.Value);
        }

        private void Parametrage_Load(object sender, EventArgs e)
        {
            nbColonnes = Convert.ToInt32(numericUpDown2.Value);
            nbLignes = Convert.ToInt32(numericUpDown1.Value);
        }
    }
}
