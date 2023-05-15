using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

public class Puissance4Grid
{
    private int nbLignes;
    private int nbColonnes;
    private PictureBox pictureBox;

    public Puissance4Grid(int nbLignes, int nbColonnes)
    {
        this.nbLignes = nbLignes;
        this.nbColonnes = nbColonnes;

        // Création du PictureBox pour afficher l'image
        pictureBox = new PictureBox();
        pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;

        // Création de l'image de la grille
        Bitmap gridImage = CreateGridImage();

        // Assignation de l'image au PictureBox
        pictureBox.Image = gridImage;
    }

    // Méthode privée pour créer l'image de la grille
    private Bitmap CreateGridImage()
    {
        // Définir les dimensions de l'image en fonction du nombre de lignes et de colonnes
        int cellSize = 50; // Taille d'une cellule
        int width = nbColonnes * cellSize;
        int height = nbLignes * cellSize;

        // Créer une nouvelle image bitmap avec les dimensions calculées
        Bitmap image = new Bitmap(width, height);

        // Créer un objet Graphics à partir de l'image
        using (Graphics graphics = Graphics.FromImage(image))
        {
            // Dessiner la grille
            for (int row = 0; row < nbLignes; row++)
            {
                for (int col = 0; col < nbColonnes; col++)
                {
                    int x = col * cellSize;
                    int y = row * cellSize;

                    // Dessiner une cellule vide
                    graphics.DrawRectangle(Pens.Black, x, y, cellSize, cellSize);
                }
            }
        }

        return image;
    }

    public void DisplayGrid()
    {
        // Créer une nouvelle fenêtre de formulaire pour afficher l'image
        Form form = new Form();
        form.Text = "Puissance 4 Grid";
        form.Controls.Add(pictureBox);

        // Afficher la fenêtre
        Application.Run(form);
    }
}
