using System;
using System.Drawing;
using System.Windows.Forms;

namespace SAE_2._01
{
    internal class IA
    {
        private FinPartie finPartie = new FinPartie();
        public int joueurMax = 1; // Le joueur Max est représenté par 1
        public int joueurMin = 2; // Le joueur Min est représenté par 2
        private Color couleurJoueurMax = Color.Red;
        private Color couleurJoueurMin = Color.Yellow;
        private Random random;
        private int profondeur = 6;

        public IA()
        {
            random = new Random();
        }

        public int MinMax(TableLayoutPanel tableLayoutPanel1, bool evalMax)
        {
            if (profondeur == 0 || finPartie.VerifierVictoire(tableLayoutPanel1, tableLayoutPanel1.RowCount, tableLayoutPanel1.ColumnCount, null) || finPartie.VerifierMatchNul(tableLayoutPanel1, tableLayoutPanel1.RowCount, tableLayoutPanel1.ColumnCount))
            {
                return Evaluer(tableLayoutPanel1); // Retourne l'évaluation du nœud
            }

            int meilleurScore = evalMax ? int.MinValue : int.MaxValue; // Meilleur score initial selon evalMax

            int meilleurCoup = -1; // Pour suivre la colonne du meilleur coup

            for (int colonne = 0; colonne < tableLayoutPanel1.ColumnCount; colonne++)
            {
                // Effectue la simulation du coup en posant un jeton dans la colonne correspondante
                PictureBox piece = JouerCoup(tableLayoutPanel1, colonne, evalMax ? joueurMax : joueurMin);

                // Appel récursif avec evalMax inversé pour l'adversaire
                int valeur = MinMax(tableLayoutPanel1, profondeur - 1, !evalMax);

                // Met à jour le meilleur score et le meilleur coup en fonction du joueur actuel
                if ((evalMax && valeur > meilleurScore) || (!evalMax && valeur < meilleurScore))
                {
                    meilleurScore = valeur;
                    meilleurCoup = colonne;
                }

                // Annule le coup
                AnnulerCoup(piece);
            }

            // Condition d'arrêt supplémentaire : si le meilleur score atteint une valeur extrême, on arrête la recherche
            if ((evalMax && meilleurScore == 1000) || (!evalMax && meilleurScore == -1000))
            {
                return meilleurScore;
            }

            if (profondeur == 6)
            {
                return meilleurCoup; // Retourne le meilleur coup lors du premier appel à MinMax
            }

            return meilleurScore; // Retourne le meilleur score
        }

        public int TrouverMeilleurCoup(TableLayoutPanel tableLayoutPanel1)
        {
            int meilleurCoup = -1;
            int meilleurScore = int.MinValue;

            for (int colonne = 0; colonne < tableLayoutPanel1.ColumnCount; colonne++)
            {
                // Vérifiez si la colonne est pleine
                if (EstColonnePleine(tableLayoutPanel1, colonne))
                    continue;

                // Effectue la simulation du coup en posant un jeton dans la colonne correspondante
                PictureBox piece = JouerCoup(tableLayoutPanel1, colonne, joueurMax);

                // Appel récursif pour évaluer le score du coup
                int score = EvaluerCoup(tableLayoutPanel1, colonne, joueurMax);

                // Met à jour le meilleur score et le meilleur coup
                if (score > meilleurScore)
                {
                    meilleurScore = score;
                    meilleurCoup = colonne;
                }

                // Annule le coup
                AnnulerCoup(piece);
            }

            return meilleurCoup;
        }

        private bool EstColonnePleine(TableLayoutPanel tableLayoutPanel1, int colonne)
        {
            for (int ligne = 0; ligne < tableLayoutPanel1.RowCount; ligne++)
            {
                PictureBox cellule = (PictureBox)tableLayoutPanel1.GetControlFromPosition(colonne, ligne);
                if (cellule.Tag == null)
                    return false;
            }

            return true;
        }

        private int EvaluerCoup(TableLayoutPanel tableLayoutPanel1, int colonne, int joueur)
        {
            // Place temporairement le jeton dans la colonne correspondante
            PictureBox piece = JouerCoup(tableLayoutPanel1, colonne, joueur);

            // Évalue le score du coup
            int score = Evaluer(tableLayoutPanel1);

            // Annule le coup
            AnnulerCoup(piece);

            return score;
        }

        public int Evaluer(TableLayoutPanel tableLayoutPanel1)
        {
            int score = 0;

            // Évaluation des lignes horizontales, verticales et diagonales
            for (int ligne = 0; ligne < tableLayoutPanel1.RowCount; ligne++)
            {
                for (int colonne = 0; colonne < tableLayoutPanel1.ColumnCount; colonne++)
                {
                    int scoreLigne = EvaluerFenetre(tableLayoutPanel1, ligne, colonne, 0, 1);
                    int scoreColonne = EvaluerFenetre(tableLayoutPanel1, ligne, colonne, 1, 0);
                    int scoreDiagonale1 = EvaluerFenetre(tableLayoutPanel1, ligne, colonne, 1, 1);
                    int scoreDiagonale2 = EvaluerFenetre(tableLayoutPanel1, ligne, colonne, 1, -1);

                    // Met à jour le score global
                    score += scoreLigne + scoreColonne + scoreDiagonale1 + scoreDiagonale2;
                }
            }

            return score;
        }

        // Méthode auxiliaire pour évaluer une fenêtre de 4 cases
        private int EvaluerFenetre(TableLayoutPanel tableLayoutPanel1, int ligne, int colonne, int deltaLigne, int deltaColonne)
        {
            int score = 0;
            int joueurMaxScore = 0; // Score du joueur Max dans la fenêtre
            int joueurMinScore = 0; // Score du joueur Min dans la fenêtre

            for (int k = 0; k < 4; k++)
            {
                int nouvelIndiceLigne = ligne + k * deltaLigne;
                int nouvelIndiceColonne = colonne + k * deltaColonne;

                // Vérifier si les indices sont valides
                if (nouvelIndiceLigne >= 0 && nouvelIndiceLigne < tableLayoutPanel1.RowCount &&
                    nouvelIndiceColonne >= 0 && nouvelIndiceColonne < tableLayoutPanel1.ColumnCount)
                {
                    Control cellControl = tableLayoutPanel1.GetControlFromPosition(nouvelIndiceColonne, nouvelIndiceLigne);

                    if (cellControl != null && cellControl is PictureBox)
                    {
                        PictureBox cellPictureBox = (PictureBox)cellControl;

                        if (cellPictureBox.BackColor == couleurJoueurMax)
                            joueurMaxScore++;
                        else if (cellPictureBox.BackColor == couleurJoueurMin)
                            joueurMinScore++;
                    }
                    else
                    {
                        // Gérer le cas où le contrôle renvoyé n'est pas un PictureBox ou est null
                        score += 0;
                    }
                }
                else
                {
                    // Gérer le cas où les indices sont hors limites
                    score += 0;
                }
            }

            if (joueurMaxScore == 4)
                score += 1000; // Joueur Max a gagné
            else if (joueurMinScore == 4)
                score -= 1000; // Joueur Min a gagné
            else if (joueurMaxScore == 3 && joueurMinScore == 0)
                score += 100; // Joueur Max est proche de gagner
            else if (joueurMaxScore == 0 && joueurMinScore == 3)
                score -= 100; // Joueur Min est proche de gagner
            else if (joueurMaxScore == 2 && joueurMinScore == 0)
                score += 10; // Joueur Max a un avantage
            else if (joueurMaxScore == 0 && joueurMinScore == 2)
                score -= 10; // Joueur Min a un avantage

            return score;
        }

        // Méthode pour jouer un coup en posant un jeton dans la colonne correspondante
        private PictureBox JouerCoup(TableLayoutPanel tableLayoutPanel1, int colonne, int joueur)
        {
            PictureBox piece = null;

            for (int ligne = tableLayoutPanel1.RowCount - 1; ligne >= 0; ligne--)
            {
                PictureBox cellule = (PictureBox)tableLayoutPanel1.GetControlFromPosition(colonne, ligne);
                if (cellule.Tag == null)
                {
                    // Place le jeton dans la cellule correspondante
                    if (joueur == joueurMax)
                        cellule.BackColor = couleurJoueurMax;
                    else if (joueur == joueurMin)
                        cellule.BackColor = couleurJoueurMin;

                    cellule.Tag = joueur; // Définit le joueur dans le Tag de la cellule

                    piece = cellule; // Sauvegarde la référence à la cellule jouée
                    break;
                }
            }

            return piece;
        }

        // Méthode pour annuler un coup en enlevant le jeton de la cellule correspondante
        private void AnnulerCoup(PictureBox piece)
        {
            if (piece != null)
            {
                piece.BackColor = Color.Transparent; // Remet la couleur d'arrière-plan à transparent
                piece.Tag = null; // Remet le Tag à null pour indiquer que la cellule est vide
            }
        }
    }
}
