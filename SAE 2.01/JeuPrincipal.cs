using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using classes_SAE_2._01;

namespace SAE_2._01
{
    public partial class Form2 : Form
    {
        Parametrage Parametrage;
        public int nbColonnes;
        public int nbLignes;
        
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Parametrage = new Parametrage();

            nbColonnes = Parametrage.nbColonnes;
            nbLignes = Parametrage.nbLignes;

            tableLayoutPanel1.ColumnCount = nbColonnes;
            tableLayoutPanel1.RowCount = nbLignes;

            // Ajouter les PictureBox à la grille
            for (int row = 0; row < nbLignes; row++)
            {
                for (int column = 0; column < nbColonnes; column++)
                {
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.BackgroundImage = Properties.Resources.image;
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox.Size = new Size(102, 99);
                    pictureBox.Dock = DockStyle.Fill; // Remplit la cellule de la grille
                    pictureBox.Margin = new Padding(0);
                    tableLayoutPanel1.Controls.Add(pictureBox, column, row);
                }
            }
            // Ajuster automatiquement la taille de la grille dans le formulaire
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            // Ajuster la taille du formulaire en fonction de la grille
            tableLayoutPanel1.Dock = DockStyle.Fill;

        }

        /*static void Main(string[] args)
        {
            
        }*/
    }
}
