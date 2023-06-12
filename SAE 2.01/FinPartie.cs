using System.Windows.Forms;

namespace SAE_2._01
{
    internal class FinPartie
    {
        private const int nombre_alignes = 4; // Nombre de jetons alignés nécessaires pour gagner

        public bool VerifierVictoire(TableLayoutPanel tableLayoutPanel, int nbLignes, int nbColonnes, Control dernierJeton)
        {
            // Vérifications horizontales, verticales et diagonales
            return VerifierAlignementHorizontal(tableLayoutPanel, nbLignes, nbColonnes, dernierJeton) ||
                   VerifierAlignementVertical(tableLayoutPanel, nbLignes, nbColonnes, dernierJeton) ||
                   VerifierAlignementDiagonal(tableLayoutPanel, nbLignes, nbColonnes, dernierJeton);
        }

        private bool VerifierAlignementHorizontal(TableLayoutPanel tableLayoutPanel, int nbLignes, int nbColonnes, Control dernierJeton)
        {
            if (dernierJeton == null)
            {
                return false;
            }

            int ligne = tableLayoutPanel.GetRow(dernierJeton);
            int colonne = tableLayoutPanel.GetColumn(dernierJeton);
            string joueur = dernierJeton.Tag.ToString();

            int alignes = 1;
            int colonneGauche = colonne - 1;
            int colonneDroite = colonne + 1;

            // Vérifier les jetons vers la gauche
            while (colonneGauche >= 0)
            {
                PictureBox cellPictureBox = (PictureBox)tableLayoutPanel.GetControlFromPosition(colonneGauche, ligne);
                if (cellPictureBox != null && cellPictureBox.Tag != null && cellPictureBox.Tag.ToString() == joueur)
                {
                    alignes++;
                    colonneGauche--;
                }
                else
                {
                    break;
                }
            }

            // Vérifier les jetons vers la droite
            while (colonneDroite < nbColonnes)
            {
                PictureBox cellPictureBox = (PictureBox)tableLayoutPanel.GetControlFromPosition(colonneDroite, ligne);
                if (cellPictureBox != null && cellPictureBox.Tag != null && cellPictureBox.Tag.ToString() == joueur)
                {
                    alignes++;
                    colonneDroite++;
                }
                else
                {
                    break;
                }
            }

            return alignes >= nombre_alignes;
        }

        private bool VerifierAlignementVertical(TableLayoutPanel tableLayoutPanel, int nbLignes, int nbColonnes, Control dernierJeton)
        {
            if (dernierJeton == null)
            {
                return false;
            }

            int ligne = tableLayoutPanel.GetRow(dernierJeton);
            int colonne = tableLayoutPanel.GetColumn(dernierJeton);
            string joueur = dernierJeton.Tag.ToString();

            int alignes = 1;
            int ligneBas = ligne + 1;

            // Vérifier les jetons vers le bas
            while (ligneBas < nbLignes)
            {
                PictureBox cellPictureBox = (PictureBox)tableLayoutPanel.GetControlFromPosition(colonne, ligneBas);
                if (cellPictureBox != null && cellPictureBox.Tag != null && cellPictureBox.Tag.ToString() == joueur)
                {
                    alignes++;
                    ligneBas++;
                }
                else
                {
                    break;
                }
            }

            return alignes >= nombre_alignes;
        }

        private bool VerifierAlignementDiagonal(TableLayoutPanel tableLayoutPanel, int nbLignes, int nbColonnes, Control dernierJeton)
        {
            if (dernierJeton == null || dernierJeton.Tag == null)
            {
                return false;
            }

            int ligne = tableLayoutPanel.GetRow(dernierJeton);
            int colonne = tableLayoutPanel.GetColumn(dernierJeton);
            string joueur = dernierJeton.Tag.ToString();

            // Vérifier les diagonales descendantes (bas gauche vers haut droit)
            int alignesDescendant = 1;
            int ligneBas = ligne + 1;
            int colonneGauche = colonne - 1;
            int ligneHaut = ligne - 1;
            int colonneDroite = colonne + 1;

            while (ligneBas < nbLignes && colonneGauche >= 0)
            {
                PictureBox cellPictureBox = (PictureBox)tableLayoutPanel.GetControlFromPosition(colonneGauche, ligneBas);
                if (cellPictureBox != null && cellPictureBox.Tag != null && cellPictureBox.Tag.ToString() == joueur)
                {
                    alignesDescendant++;
                    ligneBas++;
                    colonneGauche--;
                }
                else
                {
                    break;
                }
            }

            while (ligneHaut >= 0 && colonneDroite < nbColonnes)
            {
                PictureBox cellPictureBox = (PictureBox)tableLayoutPanel.GetControlFromPosition(colonneDroite, ligneHaut);
                if (cellPictureBox != null && cellPictureBox.Tag != null && cellPictureBox.Tag.ToString() == joueur)
                {
                    alignesDescendant++;
                    ligneHaut--;
                    colonneDroite++;
                }
                else
                {
                    break;
                }
            }

            if (alignesDescendant >= nombre_alignes)
            {
                return true;
            }

            // Vérifier les diagonales montantes (haut gauche vers bas droit)
            int alignesMontant = 1;
            ligneHaut = ligne - 1;
            colonneDroite = colonne + 1;
            ligneBas = ligne + 1;
            colonneGauche = colonne - 1;

            while (ligneHaut >= 0 && colonneGauche >= 0)
            {
                PictureBox cellPictureBox = (PictureBox)tableLayoutPanel.GetControlFromPosition(colonneGauche, ligneHaut);
                if (cellPictureBox != null && cellPictureBox.Tag != null && cellPictureBox.Tag.ToString() == joueur)
                {
                    alignesMontant++;
                    ligneHaut--;
                    colonneGauche--;
                }
                else
                {
                    break;
                }
            }

            while (ligneBas < nbLignes && colonneDroite < nbColonnes)
            {
                PictureBox cellPictureBox = (PictureBox)tableLayoutPanel.GetControlFromPosition(colonneDroite, ligneBas);
                if (cellPictureBox != null && cellPictureBox.Tag != null && cellPictureBox.Tag.ToString() == joueur)
                {
                    alignesMontant++;
                    ligneBas++;
                    colonneDroite++;
                }
                else
                {
                    break;
                }
            }

            return alignesMontant >= nombre_alignes;
        }

        public bool VerifierMatchNul(TableLayoutPanel tableLayoutPanel, int nbLignes, int nbColonnes)
        {
            for (int colonne = 0; colonne < nbColonnes; colonne++)
            {
                PictureBox cellPictureBox = (PictureBox)tableLayoutPanel.GetControlFromPosition(colonne, 0);
                if (cellPictureBox == null || cellPictureBox.Tag == null)
                {
                    return false; // Il y a encore des cellules vides, la partie n'est pas terminée par match nul
                }
            }

            return true; // Toutes les cellules sont remplies, la partie est terminée par match nul
        }
    }
}