using System;

namespace classes_SAE_2._01
{
    public class Plateau
    {
        private string[,] tableau;

        public void InitialiserGrille(int lignes, int colonnes)
        {
            tableau = new string[lignes, colonnes];
        }

        public void Afficher()
        {
            int lignes = tableau.GetLength(0);
            int colonnes = tableau.GetLength(1);

            for (int i = 0; i < lignes; i++)
            {
                for (int j = 0; j < colonnes; j++)
                {
                    Console.Write(tableau[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}