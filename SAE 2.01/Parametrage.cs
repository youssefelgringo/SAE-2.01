using System;
using System.Drawing;
using System.Windows.Forms;

namespace SAE_2._01
{
    public partial class Parametrage : Form
    {
        public static int nbColonnes = 0;
        public static int nbLignes = 0;
        Joueur Joueur1 = new Joueur();
        Joueur Joueur2 = new Joueur();
        public static string nomJoueur1 = "";
        public static string nomJoueur2 = "";
        public static string modeJeu;
        public static Color colorJoueur1 = Color.Red; // Default color for Player 1
        public static Color colorJoueur2 = Color.Yellow; // Default color for Player 2

        public Parametrage()
        {
            InitializeComponent();
            button1.Enabled = false;
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                textBox2.Enabled = false;
                CheckEnableStartButton();
            }
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                textBox2.Enabled = true;
                CheckEnableStartButton();
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
            if (Convert.ToString(comboBox1.SelectedItem) == "Rouge")
            {
                comboBox2.SelectedItem = "Jaune";
                colorJoueur1 = Color.Red;
            }
            if (Convert.ToString(comboBox1.SelectedItem) == "Jaune")
            {
                comboBox2.SelectedItem = "Rouge";
                colorJoueur1 = Color.Yellow;
            }
            CheckEnableStartButton();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToString(comboBox2.SelectedItem) == "Rouge")
            {
                comboBox1.SelectedItem = "Jaune";
                colorJoueur2 = Color.Red;
            }
            if (Convert.ToString(comboBox2.SelectedItem) == "Jaune")
            {
                comboBox1.SelectedItem = "Rouge";
                colorJoueur2 = Color.Yellow;
            }
            CheckEnableStartButton();
        }


        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            nbColonnes = Convert.ToInt32(numericUpDown2.Value);
            CheckEnableStartButton();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            nbLignes = Convert.ToInt32(numericUpDown1.Value);
            CheckEnableStartButton();
        }

        private void Parametrage_Load(object sender, EventArgs e)
        {
            nbColonnes = Convert.ToInt32(numericUpDown2.Value);
            nbLignes = Convert.ToInt32(numericUpDown1.Value);
            CheckEnableStartButton();
        }
       
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Parametrage.nomJoueur1 = textBox1.Text;
            CheckEnableStartButton();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Parametrage.nomJoueur2 = textBox2.Text;
            CheckEnableStartButton();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            modeJeu = "JvsJ";
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            CheckEnableStartButton();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            modeJeu = "JvsIA";
            comboBox1.SelectedItem = "Rouge";
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            CheckEnableStartButton();
        }

        private void CheckEnableStartButton()
        {
            // Vérifie si toutes les informations nécessaires sont renseignées pour activer le bouton
            if ((modeJeu == "JvsJ" && !string.IsNullOrEmpty(nomJoueur1) && !string.IsNullOrEmpty(nomJoueur2))
                || (modeJeu == "JvsIA" && !string.IsNullOrEmpty(nomJoueur1)))
            {
                button1.Enabled = true; // Active le bouton
            }
            else
            {
                button1.Enabled = false; // Grise le bouton
            }
        }
    }
}
