using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SAE_2._01
{
    public partial class Form2 : Form
    {
        private int nbColonnes;
        private int nbLignes;
        private const int CellSize = 100; // Taille des cellules de la grille

        private void Form2_Load(object sender, EventArgs e)
        {
            // Générer une grille de Puissance 4 en fonction du nombre de lignes et de colonnes

            int cellSize = 100; // Taille des cellules de la grille
            int spacing = 0; // Espacement entre les cellules

            // Calculer la taille de la grille
            int gridWidth = nbColonnes * (cellSize + spacing) - spacing;
            int gridHeight = nbLignes * (cellSize + spacing) - spacing;

            // Centrer la grille au milieu de la fenêtre
            int startX = (this.ClientSize.Width - gridWidth) / 2;
            int startY = (this.ClientSize.Height - gridHeight) / 2;


            // Ajouter les PictureBox à la grille
            for (int row = 0; row < nbLignes; row++)
            {
                for (int column = 0; column < nbColonnes; column++)
                {
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.BackgroundImage = Properties.Resources.image; // Image à utiliser pour les cellules
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox.Size = new Size(cellSize, cellSize);

                    // Positionner la cellule dans la grille
                    int cellX = startX + column * (cellSize + spacing);
                    int cellY = startY + row * (cellSize + spacing);
                    pictureBox.Location = new Point(cellX, cellY);

                    this.Controls.Add(pictureBox);
                }
            }

            // Supprimer le carré qui apparaît dans la grille
            tableLayoutPanel1.Visible = false;
        }







        public Form2()
        {
            InitializeComponent();
            nbColonnes = Parametrage.nbColonnes;
            nbLignes = Parametrage.nbLignes;
            
        }

        
    }
}
