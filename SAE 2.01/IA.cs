using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAE_2._01
{
    internal class IA
    {
        FinPartie FinPartie = new FinPartie();
        public int MinMax(TableLayoutPanel tableLayoutPanel1, int profondeur, bool evalMax)
        {
            if (profondeur == 0 || FinPartie.DetectionFinDePartie(tableLayoutPanel1)) // Si la profondeur est 0 ou la partie est terminée
            {
                return Evaluer(tableLayoutPanel1); // Retourne l'évaluation du nœud
            }
            else
            {
                if (evalMax) // Si c'est le tour du joueur Max
                {
                    int max = int.MinValue; // Valeur initiale maximale
                    int valeurMax = MinMax(tableLayoutPanel1, profondeur - 1, false); // Appel récursif avec evalMax à false pour le joueur Min
                    max = Math.Max(max, valeurMax); // Mise à jour de la valeur maximale

                    return max; // Retourne la valeur maximale
                }
                else // Si c'est le tour du joueur Min
                {
                    int min = int.MaxValue; // Valeur initiale minimale
                    int valeurMin = MinMax(tableLayoutPanel1, profondeur - 1, true); // Appel récursif avec evalMax à true pour le joueur Max
                    min = Math.Min(min, valeurMin); // Mise à jour de la valeur minimale

                    return min; // Retourne la valeur minimale
                }
            }
        }

        public int Evaluer(TableLayoutPanel tableLayoutPanel1)
    {
        int score = 0;
        int joueurMax = 1; // Le joueur Max est représenté par 1
        int joueurMin = 2; // Le joueur Min est représenté par 2

        // Évaluation des lignes horizontales
        for (int ligne = 0; ligne < tableLayoutPanel1.RowCount; ligne++)
        {
            for (int colonne = 0; colonne < tableLayoutPanel1.ColumnCount - 3; colonne++)
            {
                for (int k = 0; k <= 4; k++)
                {
                    PictureBox cellPictureBox = tableLayoutPanel1.GetControlFromPosition(colonne + k, ligne) as PictureBox;
                    int valeurCase = (cellPictureBox != null && cellPictureBox.BackColor == Color.Red) ? joueurMax : (cellPictureBox != null && cellPictureBox.BackColor == Color.Yellow) ? joueurMin : 0;
                    if (valeurCase == joueurMax)
                        score++;
                    else if (valeurCase == joueurMin)
                        score--;
                }
                Console.WriteLine(score);
                score += FenetreScore(score);
            }
        }

        // Évaluation des lignes verticales
        for (int colonne = 0; colonne < tableLayoutPanel1.ColumnCount; colonne++)
        {
            for (int ligne = 0; ligne < tableLayoutPanel1.RowCount - 3; ligne++)
            {
                for (int k = 0; k <= 4; k++)
                {
                    PictureBox cellPictureBox = tableLayoutPanel1.GetControlFromPosition(colonne, ligne + k) as PictureBox;
                    int valeurCase = (cellPictureBox != null && cellPictureBox.BackColor == Color.Red) ? joueurMax : (cellPictureBox != null && cellPictureBox.BackColor == Color.Yellow) ? joueurMin : 0;
                    if (valeurCase == joueurMax)
                        score++;
                    else if (valeurCase == joueurMin)
                        score--;
                }
                score += FenetreScore(score);
            }
        }

        // Évaluation des diagonales (diagonales ascendantes)
        for (int ligne = 0; ligne < tableLayoutPanel1.RowCount - 3; ligne++)
        {
            for (int colonne = 0; colonne < tableLayoutPanel1.ColumnCount - 3; colonne++)
            {
                for (int k = 0; k <= 4; k++)
                {
                    PictureBox cellPictureBox = tableLayoutPanel1.GetControlFromPosition(colonne + k, ligne + k) as PictureBox;
                    int valeurCase = (cellPictureBox != null && cellPictureBox.BackColor == Color.Red) ? joueurMax : (cellPictureBox != null && cellPictureBox.BackColor == Color.Yellow) ? joueurMin : 0;
                    if (valeurCase == joueurMax)
                        score++;
                    else if (valeurCase == joueurMin)
                        score--;
                }
                score += FenetreScore(score);
            }
        }

        // Évaluation des diagonales (diagonales descendantes)
        for (int ligne = 0; ligne < tableLayoutPanel1.RowCount - 3; ligne++)
        {
            for (int colonne = 0; colonne < tableLayoutPanel1.ColumnCount - 3; colonne++)
            {
                for (int k = 0; k < 4; k++)
                {
                    PictureBox cellPictureBox = tableLayoutPanel1.GetControlFromPosition(colonne + k, ligne + 3 - k) as PictureBox;
                    int valeurCase = (cellPictureBox != null && cellPictureBox.BackColor == Color.Red) ? joueurMax : (cellPictureBox != null && cellPictureBox.BackColor == Color.Yellow) ? joueurMin : 0;
                    if (valeurCase == joueurMax)
                        score++;
                    else if (valeurCase == joueurMin)
                        score--;
                }
                score += FenetreScore(score);
            }
        }

        return score;
    }

    // Méthode auxiliaire pour calculer le score d'une fenêtre de 4 cases
    public int FenetreScore(int score)
    {
        if (score == 4)
            return 1000; // Joueur Max a gagné
        else if (score == -4)
            return -1000; // Joueur Min a gagné
        else if (score == 3)
            return 100; // Joueur Max est proche de gagner
        else if (score == -3)
            return -100; // Joueur Min est proche de gagner
        else if (score == 2)
            return 10; // Joueur Max a un avantage
        else if (score == -2)
            return -10; // Joueur Min a un avantage
        else
            return 0; // Pas de score significatif
    }
}
}
