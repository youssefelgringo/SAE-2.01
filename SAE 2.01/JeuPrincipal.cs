using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAE_2._01
{
    public partial class Form2 : Form
    {
        private const int tailleCellule = 100; // Taille des cellules de la grille
        private int nbColonnes;
        private int nbLignes;
        public string joueurActuel = "Joueur1";
        private IA ia;
        private FinPartie finPartie;
        private string modeJeu;

        public Form2()
        {
            InitializeComponent();

            nbColonnes = Parametrage.nbColonnes;
            nbLignes = Parametrage.nbLignes;

            modeJeu = Parametrage.modeJeu;

            // Initialisez les instances des classes nécessaires
            finPartie = new FinPartie();
            ia = new IA();

            // Ajoutez le TableLayoutPanel au formulaire et définissez son ancrage pour le remplir
            tableLayoutPanel1.Dock = DockStyle.Fill;
            Controls.Add(tableLayoutPanel1);

            // Chargez la grille du jeu
            Form2_Load(this, EventArgs.Empty);
        }

        private PictureBox CreerPictureBox()
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.BackgroundImage = Properties.Resources.image; // Image to use for the cells
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Tag = null; // Initialize the Tag to null to indicate that the cell is empty
            pictureBox.Click += PictureBox_Click;

            return pictureBox;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Générer une grille de Puissance 4 en fonction du nombre de lignes et de colonnes
            int espaceCellule = 0; // Espacement entre les cellules

            // Calculer la taille de la grille
            int largeurGrille = nbColonnes * (tailleCellule + espaceCellule) - espaceCellule;
            int hauteurGrille = nbLignes * (tailleCellule + espaceCellule) - espaceCellule;

            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.Controls.Clear();

            // Ajouter les lignes et colonnes au TableLayoutPanel
            tableLayoutPanel1.ColumnCount = nbColonnes;
            tableLayoutPanel1.RowCount = nbLignes;

            for (int ligne = 0; ligne < nbLignes; ligne++)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / nbLignes));
                for (int colonne = 0; colonne < nbColonnes; colonne++)
                {
                    tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / nbColonnes));
                    PictureBox pictureBox = CreerPictureBox();
                    tableLayoutPanel1.Controls.Add(pictureBox, colonne, ligne);
                }
            }

            // Ajuster la taille de la forme en fonction de la grille de jeu
            int largeurForm = largeurGrille + tableLayoutPanel1.Margin.Horizontal + SystemInformation.VerticalScrollBarWidth;
            int hauteurForm = hauteurGrille + tableLayoutPanel1.Margin.Vertical + SystemInformation.HorizontalScrollBarHeight;

            // Ajuster la taille de la forme
            ClientSize = new Size(largeurForm, hauteurForm);
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = (PictureBox)sender;
            int colonneIndex = tableLayoutPanel1.GetColumn(clickedPictureBox);

            // Trouver la première ligne vide dans la colonne sélectionnée
            for (int ligne = tableLayoutPanel1.RowCount - 1; ligne >= 0; ligne--)
            {
                PictureBox cellule = (PictureBox)tableLayoutPanel1.GetControlFromPosition(colonneIndex, ligne);
                if (cellule.Tag == null)
                {
                    // La cellule est vide ; placez le jeton du joueur ici
                    cellule.Tag = joueurActuel;

                    // Définir la couleur d'arrière-plan en fonction du joueur
                    if (joueurActuel == "Joueur1")
                    {
                        cellule.BackColor = Parametrage.colorJoueur1; // Use color from Parametrage
                    }
                    else if (joueurActuel == "Joueur2")
                    {
                        cellule.BackColor = Parametrage.colorJoueur2; // Use color from Parametrage
                    }

                    // Vérifier si le joueur a gagné
                    if (finPartie.VerifierVictoire(tableLayoutPanel1, nbLignes, nbColonnes, cellule))
                    {
                        FinDePartie(joueurActuel + " a gagné !");
                        return;
                    }

                    // Vérifier si le match est nul
                    if (finPartie.VerifierMatchNul(tableLayoutPanel1, nbLignes, nbColonnes))
                    {
                        FinDePartie("Match nul !");
                        return;
                    }

                    if (modeJeu == "JvsJ")
                    {
                        // Alterner entre les joueurs en mode Joueur contre Joueur
                        joueurActuel = (joueurActuel == "Joueur1") ? "Joueur2" : "Joueur1";
                    }
                    else if (modeJeu == "JvsIA")
                    {
                        JouerCoupIA(); // Laisser l'IA jouer immédiatement après le joueur
                    }
                    break;
                }
            }
        }

        private void JouerCoupIA()
        {
            // Obtenir le meilleur coup à jouer par l'IA
            int colonne = ia.TrouverMeilleurCoup(tableLayoutPanel1);

            // Vérifier si l'indice de colonne renvoyé par l'IA est valide
            if (colonne < 0 || colonne >= nbColonnes)
            {
                Console.WriteLine("L'IA a renvoyé un indice de colonne invalide: " + colonne);
                return;
            }

            Console.WriteLine("L'IA a choisi la colonne : " + colonne);

            // Trouver la première ligne vide dans la colonne sélectionnée
            for (int ligne = tableLayoutPanel1.RowCount - 1; ligne >= 0; ligne--)
            {
                PictureBox cellule = (PictureBox)tableLayoutPanel1.GetControlFromPosition(colonne, ligne);
                if (cellule.Tag == null)
                {
                    // La cellule est vide ; placez le jeton de l'IA ici
                    cellule.Tag = "IA";
                    cellule.BackColor = Color.Yellow; // Définir la couleur d'arrière-plan sur Jaune pour le jeton de l'IA

                    // Vérifier si l'IA a gagné
                    if (finPartie.VerifierVictoire(tableLayoutPanel1, nbLignes, nbColonnes, cellule))
                    {
                        FinDePartie("L'IA a gagné !");
                        return;
                    }

                    // Vérifier si le match est nul
                    if (finPartie.VerifierMatchNul(tableLayoutPanel1, nbLignes, nbColonnes))
                    {
                        FinDePartie("Match nul !");
                        return;
                    }
                    break;
                }
            }
        }

        private void FinDePartie(string message)
        {
            MessageBox.Show(message, "Fin de partie", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Demandez à l'utilisateur s'il souhaite rejouer
            DialogResult result = MessageBox.Show("Voulez-vous rejouer ?", "Nouvelle partie", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Form2_Load(this, EventArgs.Empty); // Rechargez la grille du jeu
                joueurActuel = "Joueur1"; // Reset the current player to "Joueur1"
            }
            else
            {
                Close(); // Fermer le formulaire
            }
        }
    }
}
