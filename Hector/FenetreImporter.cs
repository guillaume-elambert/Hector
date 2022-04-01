using System;
using System.Collections.Generic;
using System.ComponentModel;
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


            BackgroundWorker BackgroundWorker = new BackgroundWorker();
            BackgroundWorker.DoWork += (s, args) => RecupererDepuisCSV(DialogueFichier.FileName);
            BackgroundWorker.RunWorkerAsync();
        }


        private void BoutonAjout_Click(object sender, EventArgs e)
        {
            
            BackgroundWorker BackgroundWorker = new BackgroundWorker();
            BackgroundWorker.DoWork += (s, args) =>
            {
                List<string> ListeErreurObjets = InsererContenuCSV();
                if (ListeErreurObjets.Count == 0)
                {
                    MessageBox.Show("Les données ont été importées avec succès", "Import réussi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Les données n'ont pas été importées correctement :\n" + string.Join("\n", ListeErreurObjets), "Erreur d'import", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            BackgroundWorker.RunWorkerAsync();
        }

        private void BoutonEcrasement_Click(object sender, EventArgs e)
        {
            ArticleDAO.ViderTable();
            MarqueDAO.ViderTable();
            FamilleDAO.ViderTable();
            SousFamilleDAO.ViderTable();
            Connexion.ViderTableSQLiteSequence();

                
            BackgroundWorker BackgroundWorker = new BackgroundWorker();
            BackgroundWorker.DoWork += (s, args) =>
            {
                List<string> ListeErreurObjets = InsererContenuCSV();
                if (ListeErreurObjets.Count == 0)
                {
                    MessageBox.Show("Les données ont été importées avec succès", "Import réussi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Les données n'ont pas été importées correctement :\n" + string.Join("\n", ListeErreurObjets), "Erreur d'import", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            
            BackgroundWorker.RunWorkerAsync();

        }


        /// <summary>
        /// Méthode qui permet de récupérer le contenu d'un fichier .csv
        /// </summary>
        /// <param name="CheminVersFichier">Le chemin vers le fichier .csv</param>
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
                this.Invoke((MethodInvoker)delegate
                {
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
                });
            }
        }


        public List<string> InsererContenuCSV()
        {
            List<string> ListeErreurObjets = new List<string>();
            
            /*
            List<string> ListeErreurArticles = new List<string>() { "\nErreur insertion Articles : " };
            List<string> ListeErreurMarques = new List<string>() { "\nErreur insertion Marques : " };
            List<string> ListeErreurSousFamilles = new List<string>() { "\nErreur insertion SousFamilles : " };
            List<string> ListeErreurFamilles = new List<string>() { "\nErreur insertion Familles : " };
            */
            
            int NombreArticles = Articles.Count;
            int NombreMarques = Marques.Count;
            int NombreSousFamilles = SousFamilles.Count;
            int NombreFamilles = Familles.Count;
            int NombreEnregistrements = NombreArticles + NombreMarques + NombreSousFamilles + NombreFamilles;

            this.Invoke((MethodInvoker)delegate
            {
                BoutonAjout.Enabled = false;
                BoutonEcrasement.Enabled = false;

                BarreProgressionTotale.Value = 0;
                BarreProgressionTotale.Maximum = NombreEnregistrements;
            });

            InitialiserBarreProgressionIntermediaire(NombreFamilles);

            foreach (Famille Famille in Familles.Values)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    if (!FamilleDAO.Inserer(Famille))
                    {
                        ListeErreurObjets.Add("Famille : "+Famille.ToString());
                        //ListeErreurFamilles.Add(Famille.ToString());
                    }
                });
                IncrementerBarresProgression();
            }


            InitialiserBarreProgressionIntermediaire(NombreSousFamilles);

            foreach (SousFamille SousFamille in SousFamilles.Values)
            {
                this.Invoke((MethodInvoker)delegate {
                    if (!SousFamilleDAO.Inserer(SousFamille))
                    {
                        ListeErreurObjets.Add("Sous-famille : " + SousFamille.ToString());
                        //ListeErreurSousFamilles.Add(SousFamille.ToString());
                    }
                });
                IncrementerBarresProgression();
            }


            InitialiserBarreProgressionIntermediaire(NombreMarques);

            foreach (Marque Marque in Marques.Values)
            {
                this.Invoke((MethodInvoker)delegate {
                    if (!MarqueDAO.Inserer(Marque))
                    {
                        ListeErreurObjets.Add("Marque : " + Marque.ToString());
                        //ListeErreurMarques.Add(Marque.ToString());
                    }
                });
                IncrementerBarresProgression();
            }


            InitialiserBarreProgressionIntermediaire(NombreArticles);

            foreach (Article Article in Articles.Values)
            {
                this.Invoke((MethodInvoker)delegate {
                    if (!ArticleDAO.Inserer(Article))
                    {
                        ListeErreurObjets.Add("Article : " + Article.ToString());
                        //ListeErreurArticles.Add(Article.ToString());
                    }
                });
                IncrementerBarresProgression();
            }

            this.Invoke((MethodInvoker)delegate
            {
                BoutonAjout.Enabled = true;
                BoutonEcrasement.Enabled = true;
                BarreProgressionTotale.Enabled = false;
                BarreProgressionIntermediaire.Enabled = false;
            });

            /*
            if(ListeErreurFamilles.Count > 1) ListeErreurObjets.AddRange(ListeErreurFamilles);
            if (ListeErreurSousFamilles.Count > 1) ListeErreurObjets.AddRange(ListeErreurSousFamilles);
            if (ListeErreurMarques.Count > 1) ListeErreurObjets.AddRange(ListeErreurMarques);
            if (ListeErreurArticles.Count > 1) ListeErreurObjets.AddRange(ListeErreurArticles);
            */
            return ListeErreurObjets;
        }

        public void InitialiserBarreProgressionIntermediaire(int Maximum)
        {
            this.Invoke((MethodInvoker)delegate
            {
                BarreProgressionIntermediaire.Value = 0;
                BarreProgressionIntermediaire.Maximum = Maximum;
            });
        }

        public void IncrementerBarresProgression()
        {

            this.Invoke((MethodInvoker)delegate
            {
                //Le code suivant permet de faire avancer la barre de progression intermédiaire sans animation
                if (BarreProgressionIntermediaire.Value - 1 > 0)
                {
                    --BarreProgressionIntermediaire.Value;
                    ++BarreProgressionIntermediaire.Value;
                }

                ++BarreProgressionIntermediaire.Value;


                //Le code suivant permet de faire avancer la barre de progression totale sans animation
                if (BarreProgressionTotale.Value - 1 > 0)
                {
                    --BarreProgressionTotale.Value;
                    ++BarreProgressionTotale.Value;
                }

                ++BarreProgressionTotale.Value;
            });
        }
    }
}
