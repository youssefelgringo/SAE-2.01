using System;
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
        private string joueurActuel = "Joueur";
        private IA ia;
        private FinPartie finPartie;
        private Parametrage parametrage;

        public Form2()
        {
            InitializeComponent();

            // Initialisez les instances des classes nécessaires
            ia = new IA();
            finPartie = new FinPartie();
            parametrage = new Parametrage();

            nbColonnes = Parametrage.nbColonnes;
            nbLignes = Parametrage.nbLignes;

            // Ajoutez le TableLayoutPanel au formulaire et définissez son ancrage pour le remplir
            tableLayoutPanel1.Dock = DockStyle.Fill;
            Controls.Add(tableLayoutPanel1);

            // Chargez la grille du jeu
            Form2_Load(this, EventArgs.Empty);
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
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, tailleCellule));
                for (int colonne = 0; colonne < nbColonnes; colonne++)
                {
                    tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / nbColonnes));
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.BackgroundImage = Properties.Resources.image; // Image à utiliser pour les cellules
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox.Size = new Size(tailleCellule, tailleCellule);
                    tableLayoutPanel1.Controls.Add(pictureBox, colonne, ligne);
                    pictureBox.Tag = null; // Initialise le Tag à null pour indiquer que la cellule est vide
                    pictureBox.Click += PictureBox_Click;
                }
            }

            // Ajuster la taille de la forme en fonction de la grille de jeu
            int largeurForm = largeurGrille + tableLayoutPanel1.Margin.Horizontal + SystemInformation.VerticalScrollBarWidth;
            int hauteurForm = hauteurGrille + tableLayoutPanel1.Margin.Vertical + SystemInformation.HorizontalScrollBarHeight;

            // Ajuster la taille de la forme
            ClientSize = new Size(largeurForm, hauteurForm);
        }

        private async void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            int colonneIndex = tableLayoutPanel1.GetColumn(pictureBox);

            // Trouver la première ligne vide dans la colonne sélectionnée
            for (int ligne = tableLayoutPanel1.RowCount - 1; ligne >= 0; ligne--)
            {
                PictureBox cellulePictureBox = tableLayoutPanel1.GetControlFromPosition(colonneIndex, ligne) as PictureBox;
                if (cellulePictureBox != null && cellulePictureBox.Tag == null) // Vérifier si la cellule est vide (le Tag est null)
                {
                    if (joueurActuel == "Joueur")
                    {
                        cellulePictureBox.BackColor = Color.Red; // Placer le jeton du joueur dans la cellule vide
                        cellulePictureBox.Tag = "Joueur"; // Marquer la cellule comme occupée par le joueur
                        joueurActuel = "IA"; // Changer le joueur actuel à l'IA

                        bool partieTerminee = finPartie.DetectionFinDePartie(tableLayoutPanel1); // Vérifier si la partie est terminée après avoir placé le jeton du joueur

                        if (partieTerminee)
                        {
                            // La partie est terminée
                            // Effectuez les actions nécessaires pour annoncer le gagnant de la manche
                            // Réinitialisez le jeu pour une nouvelle manche
                            Console.WriteLine("fini");
                        }
                        else
                        {
                            await JouerCoupIAAsync(tableLayoutPanel1, 6); // Jouer le coup de l'IA de manière asynchrone
                        }
                    }

                    break; // Sortir de la boucle une fois que le jeton a été placé
                }
            }
        }

        public async Task JouerCoupIAAsync(TableLayoutPanel tableLayoutPanel, int profondeur)
        {
            await Task.Delay(500); // Attendre un court instant pour donner une impression de réflexion à l'IA

            int meilleureColonne = ia.MinMax(tableLayoutPanel, profondeur, false);
            PlacerJeton(tableLayoutPanel, meilleureColonne);

            joueurActuel = "Joueur"; // Changer le joueur actuel au joueur
        }

        private void PlacerJeton(TableLayoutPanel tableLayoutPanel, int colonne)
        {
            for (int ligne = tableLayoutPanel.RowCount - 1; ligne >= 0; ligne--)
            {
                PictureBox cellulePictureBox = tableLayoutPanel.GetControlFromPosition(colonne, ligne) as PictureBox;
                if (cellulePictureBox != null && cellulePictureBox.Tag == null)
                {
                    cellulePictureBox.BackColor = Color.Yellow;
                    cellulePictureBox.Tag = "IA";
                    joueurActuel = "Joueur";
                    break;
                }
            }
        }

    }
}
