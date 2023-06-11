using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAE_2._01
{
    public partial class Parametrage : Form
    {
        public static int nbColonnes = 0;
        public static int nbLignes = 0;
        Joueur Joueur1 = new Joueur();
        Joueur Joueur2 = new Joueur();
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

            DetailsJeu detailsJeu = new DetailsJeu();
            detailsJeu.StartPosition = FormStartPosition.Manual;
            detailsJeu.Location = new Point(jeuPrincipal.Location.X + jeuPrincipal.Width, jeuPrincipal.Location.Y);
            detailsJeu.Show();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == "Rouge")
            {
                comboBox2.SelectedItem = "Jaune";
                Joueur1.couleurPiece = 1;
            }
                

            if (comboBox1.SelectedItem == "Jaune")
            {
                comboBox2.SelectedItem = "Rouge";
                Joueur1.couleurPiece = 0;
            }
                
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == "Rouge")
            {
                comboBox1.SelectedItem = "Jaune";
                Joueur2.couleurPiece = 0;
            }
                

            if (comboBox2.SelectedItem == "Jaune")
            {
                comboBox1.SelectedItem = "Rouge";
                Joueur2.couleurPiece = 1;
            }
                
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Joueur1.nom = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Joueur2.nom = textBox2.Text;
        }
    }
}
