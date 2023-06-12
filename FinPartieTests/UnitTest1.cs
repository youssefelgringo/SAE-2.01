using NUnit.Framework;
using SAE_2._01;
using System.Windows.Forms;

namespace SAE_2._01.Tests
{
    public class FinPartieTests
    {
        private TableLayoutPanel tableLayoutPanel;
        private FinPartie finPartie;
        private int nbLignes = 6;
        private int nbColonnes = 7;

        [SetUp]
        public void Setup()
        {
            tableLayoutPanel = new TableLayoutPanel
            {
                RowCount = nbLignes,
                ColumnCount = nbColonnes
            };

            for (int i = 0; i < nbLignes; i++)
            {
                for (int j = 0; j < nbColonnes; j++)
                {
                    tableLayoutPanel.Controls.Add(new PictureBox(), j, i);
                }
            }

            finPartie = new FinPartie();
        }

        private void SetJetons(int[,] positions, string tag)
        {
            foreach (var position in positions)
            {
                var pictureBox = new PictureBox
                {
                    Tag = tag
                };
                tableLayoutPanel.Controls.Add(pictureBox, position[1], position[0]);
            }
        }

        [Test]
        public void TestAlignementHorizontal()
        {
            SetJetons(new[,] { { 2, 1 }, { 2, 2 }, { 2, 3 }, { 2, 4 } }, "Joueur1");
            var dernierJeton = (PictureBox)tableLayoutPanel.GetControlFromPosition(2, 3);
            Assert.IsTrue(finPartie.VerifierVictoire(tableLayoutPanel, nbLignes, nbColonnes, dernierJeton));
        }

        [Test]
        public void TestAlignementVertical()
        {
            SetJetons(new[,] { { 2, 1 }, { 3, 1 }, { 4, 1 }, { 5, 1 } }, "Joueur1");
            var dernierJeton = (PictureBox)tableLayoutPanel.GetControlFromPosition(1, 4);
            Assert.IsTrue(finPartie.VerifierVictoire(tableLayoutPanel, nbLignes, nbColonnes, dernierJeton));
        }

        [Test]
        public void TestAlignementDiagonal()
        {
            SetJetons(new[,] { { 2, 0 }, { 3, 1 }, { 4, 2 }, { 5, 3 } }, "Joueur1");
            var dernierJeton = (PictureBox)tableLayoutPanel.GetControlFromPosition(2, 4);
            Assert.IsTrue(finPartie.VerifierVictoire(tableLayoutPanel, nbLignes, nbColonnes, dernierJeton));
        }

        [Test]
        public void TestPasDeGagnant()
        {
            SetJetons(new[,] { { 2, 0 }, { 3, 1 }, { 4, 2 } }, "Joueur1");
            var dernierJeton = (PictureBox)tableLayoutPanel.GetControlFromPosition(2, 4);
            Assert.IsFalse(finPartie.VerifierVictoire(tableLayoutPanel, nbLignes, nbColonnes, dernierJeton));
        }

        [Test]
        public void TestMatchNul()
        {
            for (int i = 0; i < nbLignes; i++)
            {
                for (int j = 0; j < nbColonnes; j++)
                {
                    tableLayoutPanel.GetControlFromPosition(j, i).Tag = "Rempli";
                }
            }
            Assert.IsTrue(finPartie.VerifierMatchNul(tableLayoutPanel, nbLignes, nbColonnes));
        }
    }
}