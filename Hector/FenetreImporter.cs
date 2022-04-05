using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace Hector
{
    internal partial class FenetreImporter : Form
    {
        /// <summary>
        /// Connexion avec la base de données
        /// </summary>
        private ConnexionBDD Connexion;

        /// <summary>
        /// DAO des Articles
        /// </summary>
        private ArticleDAO ArticleDAO;

        /// <summary>
        /// DAO des Marques
        /// </summary>
        private MarqueDAO MarqueDAO;

        /// <summary>
        /// DAO des Familles
        /// </summary>
        private FamilleDAO FamilleDAO;

        /// <summary>
        /// DAO des SousFamilles
        /// </summary>
        private SousFamilleDAO SousFamilleDAO;



        /// <summary>
        /// Dictionnaire (liste clé -> valeur) des Articles.
        /// </summary>
        public Dictionary<string, Article> Articles { get; set; }

        /// <summary>
        /// Dictionnaire (liste clé -> valeur) des Marques.
        /// </summary>
        public Dictionary<string, Marque> Marques { get; set; }

        /// <summary>
        /// Dictionnaire (liste clé -> valeur) des SousFamilles.
        /// </summary>
        public Dictionary<string, SousFamille> SousFamilles { get; set; }

        /// <summary>
        /// Dictionnaire (liste clé -> valeur) des Familles.
        /// </summary>
        public Dictionary<string, Famille> Familles { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="Connexion">Connexion à la base de données</param>
        public FenetreImporter(ConnexionBDD Connexion)
        {
            InitializeComponent();


            this.Connexion = Connexion;

            //Initialisation des DAO
            ArticleDAO = new ArticleDAO(Connexion);
            MarqueDAO = new MarqueDAO(Connexion);
            FamilleDAO = new FamilleDAO(Connexion);
            SousFamilleDAO = new SousFamilleDAO(Connexion);


            //Initialisation des dictionnaires (listes clé -> valeur)
            Articles = new Dictionary<string, Article>();
            Marques = new Dictionary<string, Marque>();
            SousFamilles = new Dictionary<string, SousFamille>();
            Familles = new Dictionary<string, Famille>();
        }


        /// <summary>
        /// Action du click sur le bouton de choix du fichier.
        /// </summary>
        /// <param name="Emetteur">L'objet appelant</param>
        /// <param name="Evenement">L'evenement</param>
        private void BoutonChoixFichier_Click(object Emetteur, EventArgs Evenement)
        {
            var DialogueFichier = new OpenFileDialog()
            {
                FileName = "",
                Title = "Importer un fichier CSV",
                InitialDirectory = Application.StartupPath,
                Filter = "Fichiers CSV (*.csv)|*.csv"
            };

            //On sort si annulation
            if (DialogueFichier.ShowDialog() != DialogResult.OK) return;


            //On récupère le contenu du fichier
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


            //On parse le fichier en tâche de fond
            BackgroundWorker BackgroundWorker = new BackgroundWorker();
            BackgroundWorker.DoWork += (s, args) => RecupererDepuisCSV(DialogueFichier.FileName);
            BackgroundWorker.RunWorkerAsync();
        }



        /// <summary>
        /// Action du click sur le bouton d'ajout
        /// </summary>
        /// <param name="Emetteur">L'objet appelant</param>
        /// <param name="Evenement">L'evenement</param>
        private void BoutonAjout_Click(object Emetteur, EventArgs Evenement)
        {
            //On insère le contenu en tâche de fond
            BackgroundWorker BackgroundWorker = new BackgroundWorker();
            BackgroundWorker.DoWork += (s, args) =>
            {
                //On insère les articles, familles, sous-familles et marques dans la base de données
                List<string> ListeErreurObjets = InsererContenuCSV();

                //S'il n'y a pas eu d'erreur, on affiche un message de succès
                if (ListeErreurObjets.Count == 0)
                {
                    MessageBox.Show(
                        "Les données ont été importées avec succès",
                        "Import réussi", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Information, 
                        MessageBoxDefaultButton.Button1, 
                        MessageBoxOptions.ServiceNotification
                    );
                }
                //Sinon on affiche la liste des erreurs
                else
                {
                    MessageBox.Show("Les données n'ont pas été importées correctement :\n\n" + string.Join("\n", ListeErreurObjets),
                        "Erreur d'import", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Error, 
                        MessageBoxDefaultButton.Button1, 
                        MessageBoxOptions.ServiceNotification
                    );
                }
            };
            BackgroundWorker.RunWorkerAsync();
        }


        /// <summary>
        /// Action du click sur le bouton d'écrasement.
        /// </summary>
        /// <param name="Emetteur">L'objet appelant</param>
        /// <param name="Evenement">L'evenement</param>
        private void BoutonEcrasement_Click(object Emetteur, EventArgs Evenement)
        {
            //On vide les tables
            ArticleDAO.ViderTable();
            MarqueDAO.ViderTable();
            FamilleDAO.ViderTable();
            SousFamilleDAO.ViderTable();
            Connexion.ViderTableSQLiteSequence();

            //On insère le contenu en tâche de fond
            BackgroundWorker BackgroundWorker = new BackgroundWorker();
            BackgroundWorker.DoWork += (s, args) =>
            {
                List<string> ListeErreurObjets = InsererContenuCSV();
                
                //S'il n'y a pas eu d'erreur, on affiche un message de succès
                if (ListeErreurObjets.Count == 0)
                {
                    MessageBox.Show(
                        "Les données ont été importées avec succès",
                        "Import réussi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.ServiceNotification
                    );
                }
                //Sinon on affiche la liste des erreurs
                else
                {
                    MessageBox.Show("Les données n'ont pas été importées correctement :\n\n" + string.Join("\n", ListeErreurObjets),
                        "Erreur d'import",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.ServiceNotification
                    );
                }
            };
            
            BackgroundWorker.RunWorkerAsync();

        }


        /// <summary>
        /// Méthode qui permet de récupérer le contenu d'un fichier .csv
        /// </summary>
        /// <param name="CheminVersFichier">Le chemin vers le fichier .csv</param>
        private void RecupererDepuisCSV(string CheminVersFichier)
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
                //Sinon on créé un nouvel objet
                this.Invoke((MethodInvoker)delegate
                {
                    Article = Articles.ContainsKey(RefArticle) ? Articles[RefArticle] : new Article();
                    Marque = Marques.ContainsKey(NomMarque) ? Marques[NomMarque] : new Marque();
                    SousFamille = SousFamilles.ContainsKey(NomSousFamille) ? SousFamilles[NomSousFamille] : new SousFamille();
                    Famille = Familles.ContainsKey(NomFamille) ? Familles[NomFamille] : new Famille();
                

                    //On définit les valeurs des objets
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


        /// <summary>
        /// Méthode qui se charge d'insérer tous les objets des dictionnaires en base de données.
        /// </summary>
        /// <returns>La liste des erreurs issues de ces insertions</returns>
        private List<string> InsererContenuCSV()
        {
            List<string> ListeErreurObjets = new List<string>();
            
            //Initialisation des compteurs (pour avancement barres progression)
            int NombreArticles = Articles.Count;
            int NombreMarques = Marques.Count;
            int NombreSousFamilles = SousFamilles.Count;
            int NombreFamilles = Familles.Count;
            int NombreEnregistrements = NombreArticles + NombreMarques + NombreSousFamilles + NombreFamilles;


            //On initialise la barre de progression et bloque les boutons
            this.Invoke((MethodInvoker)delegate
            {
                BoutonAjout.Enabled = false;
                BoutonEcrasement.Enabled = false;

                BarreProgressionTotale.Value = 0;
                BarreProgressionTotale.Maximum = NombreEnregistrements;
            });


            //Initialisation de la barre des étapes intermédiaire
            InitialiserBarreProgressionIntermediaire(NombreFamilles);

            //Insertion des Familles
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

            //Insertion des SousFamilles
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

            //Insertion des SousFamilles
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


            //Insertion des Articles
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


            //On active les boutons
            this.Invoke((MethodInvoker)delegate
            {
                BoutonAjout.Enabled = true;
                BoutonEcrasement.Enabled = true;
                BarreProgressionTotale.Enabled = false;
                BarreProgressionIntermediaire.Enabled = false;
            });
            

            //Debug erreurs (100% si insertion avec les mêmes données)
            foreach(string err in ListeErreurObjets)
            {
                Console.WriteLine(err);
            }
            Console.WriteLine(ListeErreurObjets.Count + " erreurs sur " + NombreEnregistrements + " enregistrements");


            return ListeErreurObjets;
        }



        /// <summary>
        /// Méthode qui permet d'initialiser la barre de progression des étapes intermédiaires.
        /// </summary>
        /// <param name="Maximum">La valeur maximale que peut prendre la barre de progression</param>
        private void InitialiserBarreProgressionIntermediaire(int Maximum)
        {
            this.Invoke((MethodInvoker)delegate
            {
                BarreProgressionIntermediaire.Value = 0;
                BarreProgressionIntermediaire.Maximum = Maximum;
            });
        }



        /// <summary>
        /// Méthode qui permet d'incrémenter les valeurs des barres de progressions
        /// </summary>
        private void IncrementerBarresProgression()
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
