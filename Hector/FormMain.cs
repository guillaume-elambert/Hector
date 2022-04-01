using System;
using System.Windows.Forms;

namespace Hector
{
    public partial class FormMain : Form
    {

        /// <summary>
        /// Chemin vers la base de données SQLite.
        /// </summary>
        public static string CheminVersSQLite = Application.StartupPath + "\\Hector.SQLite";

        /// <summary>
        /// La connexion vers la base de données
        /// </summary>
        private ConnexionBDD Connexion;

        public FormMain()
        {
            Connexion = new ConnexionBDD(CheminVersSQLite);
            InitializeComponent();
        }

        private void ImporterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FenetreImporter FormulaireImporter = new FenetreImporter(Connexion);
            FormulaireImporter.ShowDialog();
        }

    }
}
