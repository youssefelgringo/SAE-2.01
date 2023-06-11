using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAE_2._01
{
    public partial class DetailsJeu : Form
    {
        Parametrage Parametrage = new Parametrage();
        Form2 JeuPrincipal = new Form2();
        public DetailsJeu()
        {
            InitializeComponent();
        }

        private void DetailsJeu_Load(object sender, EventArgs e)
        {
            // Empêcher le redimensionnement de la Form
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            label1.Text = "Nombre de manches: " + Convert.ToString(Parametrage.numericUpDown3.Value);
            //label2.Text = "Nombre de manches restantes: " + Convert.ToString((Parametrage.numericUpDown3.Value - JeuPrincipal.manchesJouees));
        }
    }
}
