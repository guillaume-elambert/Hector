using System;
using System.IO;
using System.Windows.Forms;

namespace Hector
{
    public partial class FenetreImporter : Form
    {
        private string ContenuFichier;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="Connexion">Connexion à la base de données</param>
        public FenetreImporter(ConnexionBDD Connexion)
        {
            InitializeComponent();
        }


        /// <summary>
        /// Action du click sur le bouton d'ajout.
        /// </summary>
        /// <param name="sender">L'objet appelant</param>
        /// <param name="e">Les arguments</param>
        private void BoutonImporter_Click(object sender, EventArgs e)
        {
            var DialogueFichier = new OpenFileDialog()
            {
                FileName = "",
                Title = "Import .csv file"

            };
            DialogueFichier.InitialDirectory = Application.StartupPath;
            DialogueFichier.Filter = "csv files (*.csv)|*.csv";
            DialogueFichier.ShowDialog();

            FileStream Fichier = (FileStream)DialogueFichier.OpenFile();
            NomFichierTextBox.Text = Fichier.Name;

            using (StreamReader StreamReader = new StreamReader(Fichier))
            {
                ContenuFichier = StreamReader.ReadToEnd();
                ContenuFichierTextBox.Text = ContenuFichier;
            }
            Fichier.Close();

            BoutonAjout.Enabled = true;
            BoutonEcrasement.Enabled = true;
        }

        private void BoutonAjout_Click(object sender, EventArgs e)
        {
                
        }

        private void BoutonEcrasement_Click(object sender, EventArgs e)
        {
            //ViderBaseDonnees();
        }


        

    }
}
