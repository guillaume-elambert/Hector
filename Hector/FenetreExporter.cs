using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Hector
{
    internal partial class FenetreExporter : Form
    {
        /// <summary>
        /// La connexion vers la base de données
        /// </summary>
        private ConnexionBDD Connexion;

        /// <summary>
        /// Objet DAO des Articles.
        /// </summary>
        private ArticleDAO ArticleDAO;
        /// <summary>
        /// Objet DAO des Marques.
        /// </summary>
        MarqueDAO MarqueDAO;
        /// <summary>
        /// Objet DAO des Familles.
        /// </summary>
        FamilleDAO FamilleDAO;
        /// <summary>
        /// Objet DAO des SousFamilles.
        /// </summary>
        SousFamilleDAO SousFamilleDAO;

        /// <summary>
        /// Dictionnaire des Articles.
        /// </summary>
        public Dictionary<string, Article> Articles { get; set; }
        /// <summary>
        /// Dictionnaire des Marques.
        /// </summary>
        public Dictionary<string, Marque> Marques { get; set; }
        /// <summary>
        /// Dictionnaire des SousFamilles.
        /// </summary>
        public Dictionary<string, SousFamille> SousFamilles { get; set; }
        /// <summary>
        /// Dictionnaire des Familles.
        /// </summary>
        public Dictionary<string, Famille> Familles { get; set; }


        /// <summary>
        /// Constructeur.
        /// </summary>
        public FenetreExporter()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="Connexion">Connexion vers la base de données</param>
        public FenetreExporter(ConnexionBDD Connexion)
        {
            InitializeComponent();


            this.Connexion = Connexion;

            //Initialisation des DAO
            ArticleDAO = new ArticleDAO(Connexion);
            MarqueDAO = new MarqueDAO(Connexion);
            FamilleDAO = new FamilleDAO(Connexion);
            SousFamilleDAO = new SousFamilleDAO(Connexion);

            //Initialisation des dictionnaires
            Articles = new Dictionary<string, Article>();
            Marques = new Dictionary<string, Marque>();
            SousFamilles = new Dictionary<string, SousFamille>();
            Familles = new Dictionary<string, Famille>();

        }


        /// <summary>
        /// Méthode qui gère le clic sur le bouton de choix du fichier CSV
        /// </summary>
        /// <param name="Emetteur">L'objet emetteur</param>
        /// <param name="Evenement">L'evenement</param>
        private void BoutonChoixFichier_Click(object Emetteur, EventArgs Evenement)
        {
            //On affiche la boite de dialogue
            SaveFileDialog DialogueFichier = new SaveFileDialog()
            {
                FileName = "données-exportées_" + DateTime.Now.ToString("dd-MM-yyyy") + ".csv",
                Title = "Exporter les données dans un fichier CSV",
                InitialDirectory = Application.StartupPath,
                Filter = "Fichiers CSV (*.csv)|*.csv"
            };

            //On sort si l'utilisateur n'a pas validé
            if (DialogueFichier.ShowDialog() != DialogResult.OK) return;

            //On récupère le chemin du fichier
            NomFichierTextBox.Text = DialogueFichier.FileName;

            //On active le bouton d'exportation
            BoutonExporter.Enabled = true;
        }


        /// <summary>
        /// Méthode qui gère le clic sur le bouton d'annulation (fermeture de la fenêtre)
        /// </summary>
        /// <param name="Emetteur">L'objet emetteur</param>
        /// <param name="Evenement">L'evenement</param>
        private void BoutonAnnuler_Click(object Emetteur, EventArgs Evenement)
        {
            Close();
        }


        /// <summary>
        /// Méthode qui gère le clic sur le bouton d'exportation
        /// </summary>
        /// <param name="Emetteur">L'objet emetteur</param>
        /// <param name="Evenement">L'evenemnt</param>
        private void BoutonExporter_Click(object Emetteur, EventArgs Evenement)
        {

            //On récupère les données et on les insère dans le fichier fichier
            StreamWriter Fichier = new StreamWriter(NomFichierTextBox.Text);
            Fichier.Write("Description;Ref;Marque;Famille;Sous-Famille;Prix H.T.\n");

            Dictionary<string, Article> Articles = ArticleDAO.ObtenirTout();

            //On écrit tous les Articles dans le fichier
            foreach (Article Article in Articles.Values)
            {
                //On écrit autant de fois l'article qu'il y a de quantité
                for (int Compteur = 0; Compteur < Article.Quantite; ++Compteur)
                {
                    Fichier.Write(Article.ToCSV() + "\n");
                }
            }

            Fichier.Close();
        }


        /// <summary>
        /// Méthode pour fermer la fernêtre lors de l'appuie sur la touche "Echap" ou "Entrée".
        /// </summary>
        /// <param name="Touche">La touche</param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys Touche)
        {
            if (Form.ModifierKeys == Keys.None && Touche == Keys.Escape)
            {
                Close();
                return true;
            }

            if (Form.ModifierKeys == Keys.None && Touche == Keys.Enter)
            {
                if (BoutonExporter.Enabled)
                {
                    BoutonExporter.PerformClick();
                    return true;
                }
            }

            return base.ProcessDialogKey(Touche);
        }
    }
}
