using classes_SAE_2._01;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Power4
{
    class AI
    {
        public class MinMaxIA
        {
            /*public int MinMax(int noeud, int profondeur, bool evalMax)
            {
                if (profondeur == 0 || Jeu.FinPartie == true) // Si la profondeur est 0 ou la partie est terminée
                {
                    return Evaluer(noeud); // Retourne l'évaluation du nœud
                }
                else
                {
                    if (evalMax) // Si c'est le tour du joueur Max
                    {
                        int max = int.MinValue; // Valeur initiale maximale
                        int valeur = MinMax(f, profondeur - 1, false); // Appel récursif avec evalMax à false pour le joueur Min
                        max = Math.Max(max, valeur); // Mise à jour de la valeur maximale

                        return max; // Retourne la valeur maximale
                    }
                    else // Si c'est le tour du joueur Min
                    {
                        int min = int.MaxValue; // Valeur initiale minimale
                        int valeur = MinMax(f, profondeur - 1, true); // Appel récursif avec evalMax à true pour le joueur Max
                        min = Math.Min(min, valeur); // Mise à jour de la valeur minimale

                        return min; // Retourne la valeur minimale
                    }
                }
            }
        }
        private int Evaluer(int noeud)
            {
                int score = 0;
                int joueurMax = 1; // Le joueur Max est représenté par 1
                int joueurMin = 2; // Le joueur Min est représenté par 2

                // Tableau 2D représentant le plateau de jeu
                int[,] plateau = ConvertirEnTableau2D(noeud);

                // Évaluation des lignes horizontales
                for (int ligne = 0; ligne < 6; ligne++)
                {
                    for (int colonne = 0; colonne < 4; colonne++)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            int valeurCase = plateau[ligne, colonne + k];
                            if (valeurCase == joueurMax)
                                score++;
                            else if (valeurCase == joueurMin)
                                score--;
                        }
                        score += FenetreScore(score);
                    }
                }

                // Évaluation des lignes verticales
                for (int colonne = 0; colonne < 7; colonne++)
                {
                    for (int ligne = 0; ligne < 3; ligne++)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            int valeurCase = plateau[ligne + k, colonne];
                            if (valeurCase == joueurMax)
                                score++;
                            else if (valeurCase == joueurMin)
                                score--;
                        }
                        score += FenetreScore(score);
                    }
                }

                // Évaluation des diagonales (diagonales ascendantes)
                for (int ligne = 0; ligne < 3; ligne++)
                {
                    for (int colonne = 0; colonne < 4; colonne++)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            int valeurCase = plateau[ligne + k, colonne + k];
                            if (valeurCase == joueurMax)
                                score++;
                            else if (valeurCase == joueurMin)
                                score--;
                        }
                        score += FenetreScore(score);
                    }
                }

                // Évaluation des diagonales (diagonales descendantes)
                for (int ligne = 0; ligne < 3; ligne++)
                {
                    for (int colonne = 0; colonne < 4; colonne++)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            int valeurCase = plateau[ligne + 3 - k, colonne + k];
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

            // Méthode auxiliaire pour convertir un entier en un tableau 2D représentant le plateau de jeu
            private int[,] ConvertirEnTableau2D(int noeud)
            {
                int[,] plateau = new int[6, 7];
                for (int ligne = 0; ligne < 6; ligne++)
                {
                    for (int colonne = 0; colonne < 7; colonne++)
                    {
                        plateau[ligne, colonne] = noeud % 10;
                        noeud /= 10;
                    }
                }
                return plateau;
            }

            // Méthode auxiliaire pour calculer le score d'une fenêtre de 4 cases
            private int FenetreScore(int score)
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

        }*/
        }
    }
}