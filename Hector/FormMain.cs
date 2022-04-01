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
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode NoeudCourant = ArbreArticles.SelectedNode;
            switch (NoeudCourant.Text)
            {
                case "RacineArticles" :
                    //Recupérer tous les articles de la base de données
                    break;
                case "RacineFamille":
                    //Recuperer juste les articles par rapport à leur famille et non avec les marques
                    break;
                case "EcritureEtCorrection":
                    //Recuperer les objets qui appartiennent à la sous famille ecriture et correction
                    break;
                case "SousFamilleEC":
                    break;
            }
        }
    }
} 

