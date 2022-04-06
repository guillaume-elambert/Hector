using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hector
{
    internal partial class FenetreExporter : Form
    {
        private ConnexionBDD Connexion;

        private ArticleDAO ArticleDAO;
        private MarqueDAO MarqueDAO;
        private FamilleDAO FamilleDAO;
        private SousFamilleDAO SousFamilleDAO;


        public Dictionary<string, Article> Articles { get; set; }
        public Dictionary<string, Marque> Marques { get; set; }
        public Dictionary<string, SousFamille> SousFamilles { get; set; }
        public Dictionary<string, Famille> Familles { get; set; }
        

        public FenetreExporter()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="Connexion">Connexion à la base de données</param>
        public FenetreExporter(ConnexionBDD Connexion)
        {
            InitializeComponent();


            this.Connexion = Connexion;

            ArticleDAO = new ArticleDAO(Connexion);
            MarqueDAO = new MarqueDAO(Connexion);
            FamilleDAO = new FamilleDAO(Connexion);
            SousFamilleDAO = new SousFamilleDAO(Connexion);

            Articles = new Dictionary<string, Article>();
            Marques = new Dictionary<string, Marque>();
            SousFamilles = new Dictionary<string, SousFamille>();
            Familles = new Dictionary<string, Famille>();

            /*
            BarreProgressionTotale.Step = 1;
            BarreProgressionIntermediaire.Step = 1;
            */
        }

        private void BoutonChoixFichier_Click(object Emetteur, EventArgs Evenement)
        {
            SaveFileDialog DialogueFichier = new SaveFileDialog()
            {
                FileName = "données-exportées_"+ DateTime.Now.ToString("dd-MM-yyyy") +".csv",
                Title = "Exporter les données dans un fichier CSV",
                InitialDirectory = Application.StartupPath,
                Filter = "Fichiers CSV (*.csv)|*.csv"
            };

            if (DialogueFichier.ShowDialog() != DialogResult.OK) return;

            NomFichierTextBox.Text = DialogueFichier.FileName;
            
            /*
            string ContenuCSV = "Description;Ref;Marque;Famille;Sous-Famille;Prix H.T.\n";
            List<Article> Articles = ArticleDAO.ObtenirTout();
            foreach (Article Article in Articles)
            {
                ContenuCSV = Article.ToCSV()+"\n";
            }

            //On insère dans le fichier fichier
            FileStream Fichier = (FileStream)DialogueFichier.OpenFile();
            Fichier.WriteAsync(Encoding.UTF8.GetBytes(ContenuCSV), 0, ContenuCSV.Length);
            */
            BoutonAnnuler.Enabled = true;
            BoutonExporter.Enabled = true;
        }

        private void BoutonAnnuler_Click(object Emetteur, EventArgs Evenement)
        {
            this.Dispose(true);
        }

        private void BoutonExporter_Click(object Emetteur, EventArgs Evenement)
        {

            //BoutonAnnuler.Enabled = false;
            //BoutonExporter.Enabled = false;

            //On récupère les données et on les insère dans le fichier fichier
            StreamWriter Fichier = new StreamWriter(NomFichierTextBox.Text);
            Fichier.Write("Description;Ref;Marque;Famille;Sous-Famille;Prix H.T.\n");
            
            Dictionary<string, Article> Articles = ArticleDAO.ObtenirTout();
            
            foreach (Article Article in Articles.Values)
            {
                for(int Compteur = 0; Compteur < Article.Quantite; ++Compteur){
                    Fichier.Write(Article.ToCSV() + "\n");
                }
            }
            Fichier.Close();
        }
    }
}
