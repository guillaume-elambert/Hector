using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Hector
{
    internal partial class FenetreImporter : Form
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

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="Connexion">Connexion à la base de données</param>
        public FenetreImporter(ConnexionBDD Connexion)
        {
            this.Connexion = Connexion;

            ArticleDAO = new ArticleDAO(Connexion);
            MarqueDAO = new MarqueDAO(Connexion);
            FamilleDAO = new FamilleDAO(Connexion);
            SousFamilleDAO = new SousFamilleDAO(Connexion);

            Articles = new Dictionary<string, Article>();
            Marques = new Dictionary<string, Marque>();
            SousFamilles = new Dictionary<string, SousFamille>();
            Familles = new Dictionary<string, Famille>();

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

            if (DialogueFichier.ShowDialog() != DialogResult.OK) return;


            FileStream Fichier = (FileStream)DialogueFichier.OpenFile();
            NomFichierTextBox.Text = Fichier.Name;

            using (StreamReader StreamReader = new StreamReader(Fichier))
            {
                string ContenuFichier = StreamReader.ReadToEnd();
                ContenuFichierTextBox.Text = ContenuFichier;
            }
            Fichier.Close();

            BoutonAjout.Enabled = true;
            BoutonEcrasement.Enabled = true;

            RecupererDepuisCSV(DialogueFichier.FileName);
        }

        public void RecupererDepuisCSV(string CheminVersFichier)
        {
            ParseurCSV ParseurCSV = new ParseurCSV(CheminVersFichier);
            ParseurCSV.Parser(true);

            Article Article;
            Marque Marque;
            SousFamille SousFamille;
            Famille Famille;

            string DescriptionArticle, RefArticle, PrixArticle, NomMarque, NomSousFamille, NomFamille;

            foreach (string[] Ligne in ParseurCSV)
            {

                //On récupère l'ensemble des paramètres passés
                DescriptionArticle = Ligne[0];
                RefArticle = Ligne[1];
                NomMarque = Ligne[2];
                NomFamille = Ligne[3];
                NomSousFamille = Ligne[4];
                PrixArticle = Ligne[5];

                //Si les objets sont déjà présents dans les dictionnaires on ne les ajoute pas
                Article = Articles.ContainsKey(RefArticle) ? Articles[RefArticle] : new Article();
                Marque = Marques.ContainsKey(NomMarque) ? Marques[NomMarque] : new Marque();
                SousFamille = SousFamilles.ContainsKey(NomSousFamille) ? SousFamilles[NomSousFamille] : new SousFamille();
                Famille = Familles.ContainsKey(NomFamille) ? Familles[NomFamille] : new Famille();


                Article.Description = DescriptionArticle;
                Article.RefArticle = RefArticle;
                Article.Prix = float.Parse(PrixArticle);

                Marque.Nom = NomMarque;
                SousFamille.Nom = NomSousFamille;
                Famille.Nom = NomFamille;

                SousFamille.Famille = Famille;

                Article.Marque = Marque;
                Article.SousFamille = SousFamille;

                Articles[Article.RefArticle] = Article;
                SousFamilles[SousFamille.Nom] = SousFamille;
                Familles[Famille.Nom] = Famille;
                Marques[Marque.Nom] = Marque;
            }
        }


        private void BoutonAjout_Click(object sender, EventArgs e)
        {

        }

        private void BoutonEcrasement_Click(object sender, EventArgs e)
        {
            ArticleDAO.ViderTable();
            MarqueDAO.ViderTable();
            FamilleDAO.ViderTable();
            SousFamilleDAO.ViderTable();
            Connexion.ViderTableSQLiteSequence();

            FamilleDAO.Inserer( new List<Famille>(Familles.Values));
            SousFamilleDAO.Inserer(new List<SousFamille>(SousFamilles.Values));
            MarqueDAO.Inserer(new List<Marque>(Marques.Values));
            ArticleDAO.Inserer(new List<Article>(Articles.Values));

            /*foreach (Famille Famille in Familles.Values)
            {
                FamilleDAO.Inserer(Famille);
            }

            foreach (SousFamille SousFamille in SousFamilles.Values)
            {
                SousFamilleDAO.Inserer(SousFamille);
            }

            foreach (Marque Marque in Marques.Values)
            {
                MarqueDAO.Inserer(Marque);
            }

            foreach (Article Article in Articles.Values)
            {
                ArticleDAO.Inserer(Article);
            }*/
        }




    }
}
