using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAE_2._01
{
    internal class FinPartie
    {
        public bool DetectionFinDePartie(TableLayoutPanel tableLayoutPanel)
        {
            int nbLignes = tableLayoutPanel.RowCount;
            int nbColonnes = tableLayoutPanel.ColumnCount;

            // Vérification horizontale
            for (int ligne = 0; ligne < nbLignes; ligne++)
            {
                int nbJetonsAlignes = 1;
                PictureBox premierJeton = tableLayoutPanel.GetControlFromPosition(0, ligne) as PictureBox;
                if (premierJeton == null)
                    continue;

                for (int colonne = 1; colonne < nbColonnes; colonne++)
                {
                    PictureBox jetonCourant = tableLayoutPanel.GetControlFromPosition(colonne, ligne) as PictureBox;
                    if (jetonCourant == null)
                        continue;

                    if (jetonCourant.BackColor == premierJeton.BackColor)
                    {
                        nbJetonsAlignes++;
                        if (nbJetonsAlignes == 4)
                            return true;
                    }
                    else
                    {
                        nbJetonsAlignes = 1;
                        premierJeton = jetonCourant;
                    }

                    
                }
            }

            // Vérification verticale
            for (int colonne = 0; colonne < nbColonnes; colonne++)
            {
                int nbJetonsAlignes = 1;
                PictureBox premierJeton = tableLayoutPanel.GetControlFromPosition(colonne, 0) as PictureBox;
                if (premierJeton == null)
                    continue;

                for (int ligne = 1; ligne < nbLignes; ligne++)
                {
                    PictureBox jetonCourant = tableLayoutPanel.GetControlFromPosition(colonne, ligne) as PictureBox;
                    if (jetonCourant == null)
                        continue;

                    if (jetonCourant.BackColor == premierJeton.BackColor)
                    {
                        nbJetonsAlignes++;
                        if (nbJetonsAlignes == 4)
                            return true;
                    }
                    else
                    {
                        nbJetonsAlignes = 1;
                        premierJeton = jetonCourant;
                    }
                }
            }

            // Vérification diagonale (de gauche à droite)
            for (int ligne = 0; ligne < nbLignes; ligne++)
            {
                for (int colonne = 0; colonne < nbColonnes; colonne++)
                {
                    PictureBox premierJeton = tableLayoutPanel.GetControlFromPosition(colonne, ligne) as PictureBox;
                    if (premierJeton == null)
                        continue;

                    int nbJetonsAlignes = 1;
                    int currentRow = ligne + 1;
                    int currentColumn = colonne + 1;

                    while (currentRow < nbLignes && currentColumn < nbColonnes)
                    {
                        PictureBox jetonCourant = tableLayoutPanel.GetControlFromPosition(currentColumn, currentRow) as PictureBox;
                        if (jetonCourant == null)
                            break;

                        if (jetonCourant.BackColor == premierJeton.BackColor)
                        {
                            nbJetonsAlignes++;
                            if (nbJetonsAlignes == 4)
                                return true;
                        }
                        else
                        {
                            break;
                        }

                        currentRow++;
                        currentColumn++;
                    }
                }
            }

            // Vérification diagonale (de droite à gauche)
            for (int ligne = 0; ligne < nbLignes; ligne++)
            {
                for (int colonne = 0; colonne < nbColonnes; colonne++)
                {
                    PictureBox premierJeton = tableLayoutPanel.GetControlFromPosition(colonne, ligne) as PictureBox;
                    if (premierJeton == null)
                        continue;

                    int nbJetonsAlignes = 1;
                    int currentRow = ligne + 1;
                    int currentColumn = colonne - 1;

                    while (currentRow < nbLignes && currentColumn >= 0)
                    {
                        PictureBox jetonCourant = tableLayoutPanel.GetControlFromPosition(currentColumn, currentRow) as PictureBox;
                        if (jetonCourant == null)
                            break;

                        if (jetonCourant.BackColor == premierJeton.BackColor)
                        {
                            nbJetonsAlignes++;
                            if (nbJetonsAlignes == 4)
                                return true;
                        }
                        else
                        {
                            break;
                        }

                        currentRow++;
                        currentColumn--;
                    }
                }
            }

            // Vérification si la grille est entièrement remplie (match nul)
            bool grilleComplete = true;
            for (int ligne = 0; ligne < nbLignes; ligne++)
            {
                for (int colonne = 0; colonne < nbColonnes; colonne++)
                {
                    PictureBox jetonCourant = tableLayoutPanel.GetControlFromPosition(colonne, ligne) as PictureBox;
                    if (jetonCourant == null)
                    {
                        grilleComplete = false;
                        break;
                    }
                }
                if (!grilleComplete)
                    break;
                if (grilleComplete)
                    return true; // La grille est entièrement remplie, match nul
            }


            return false; // Aucune condition de fin de partie n'a été rencontrée
        }
    }
}